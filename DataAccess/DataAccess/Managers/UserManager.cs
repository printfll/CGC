// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Utility
{
    using Microsoft.CGC.DataAccess;
    using Microsoft.CGC.DataAccess.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for managing permission related operations
    /// </summary>   
    public class UserManager
    {
        private static readonly Lazy<UserManager> lazy = new Lazy<UserManager>(() => new UserManager());

        public static UserManager Instance { get { return lazy.Value; } }

        private UserManager()
        {
            bool initialized = true;
            using (var context = new EntityContext())
            {
                var adminAcc = context.Accounts.Where(a => a.Name == "admin").FirstOrDefault();
                initialized = (adminAcc != null);
            }

            if (initialized) { return; }

            // create init seed user/group in database
            using (var context = new EntityContext())
            {
                var account = new Account() { Name = "admin", Type = "User", IsDisabled = false };
                var newUser = new User() { Account = account, Name = "admin", Email = null };
                context.Users.Add(newUser);
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            AddGroup("admin", "AllUsers");
        }

        public void AddUser(string currentUser, string userName, string email)
        {
            if (currentUser != "admin")
            {
                throw new DataAccessException($"permission denied");
            }
            using (var context = new EntityContext())
            {
                GetUserInner(context, currentUser);
                // check if a user with the same name already exists
                var existingAcc = context.Accounts.Where(a => a.Name == userName).FirstOrDefault();
                if (existingAcc != null)
                {
                    throw new DataAccessException($"invalid user name {userName}");
                }

                var account = new Account() { Name = userName, Type = "User", IsDisabled = false };
                var newUser = new User() { Account = account, Name = userName, Email = email};
                context.Users.Add(newUser);
                context.Accounts.Add(account);
                context.SaveChanges();
            }

            // add user to default group AllUsers
            AddAccountToGroup("admin", userName, "AllUsers");
        }

        public void RemoveUser(string currentUser, string username)
        {
            if (currentUser != "admin")
            {
                throw new DataAccessException($"permission denied");
            }

            using (var context = new EntityContext())
            {
                var acc = GetUserInner(context, username);
                acc.IsDisabled = true;
                context.SaveChanges();
            }
        }

        public void ResumeUser(string currentUser, string userName)
        {
            if (currentUser != "admin")
            {
                throw new DataAccessException($"permission denied");
            }

            using (var context = new EntityContext())
            {
                var userAcc = context.Accounts.Where(x => x.Name == userName && x.Type == "User" && x.IsDisabled).FirstOrDefault();
                if (userAcc == null)
                {
                    throw new DataAccessException($"user {userName} not found or not disabled");
                }
                userAcc.IsDisabled = false;
                context.SaveChanges();
            }
        }

        public void AddGroup(string currentUser, string groupName)
        {
            using (var context = new EntityContext())
            {
                var currentAcc = GetUserInner(context, currentUser);

                // check existing group
                var existingAcc = context.Accounts.Where(a => a.Name == groupName).FirstOrDefault();
                if (existingAcc != null)
                {
                    throw new DataAccessException($"invalid group name {groupName}");
                }

                var groupAcc = new Account() { Name = groupName, Type = "Group", IsDisabled = false };
                var newGroup = new Group() { Account = groupAcc, Owner = currentUser, Name = groupName};
                context.Groups.Add(newGroup);               
                context.Accounts.Add(groupAcc);
                context.GroupUserMappings.Add(new GroupUserMapping() { Group = groupAcc, Member = currentAcc, IsOwner = true});
                context.SaveChanges();
            }
        }

        public void RemoveGroup(string currentUser, string groupName)
        {
            if (groupName == "AllUsers")
            {
                throw new DataAccessException($"permission denied");
            }

            using (var context = new EntityContext())
            {
                var checkRes = EnsureCanOperateGroup(context, currentUser, groupName);
                var groupAcc = checkRes.Item2;
                groupAcc.IsDisabled = true;
                context.SaveChanges();
            }
        }

        public void ResumeGroup(string currentUser, string groupName)
        {
            if (currentUser != "admin")
            {
                throw new DataAccessException($"permission denied");
            }

            using (var context = new EntityContext())
            {
                var groupAcc = context.Accounts.Where(x => x.Name == groupName && x.Type == "Group" && x.IsDisabled).FirstOrDefault();
                if (groupAcc == null)
                {
                    throw new DataAccessException($"group {groupName} not found or not disabled");
                }
                groupAcc.IsDisabled = false;
                context.SaveChanges();
            }
        }

        public void AddAccountToGroup(string currentUser, string accountName, string groupName)
        {
            using (var context = new EntityContext())
            {
                var checkRes = EnsureCanOperateGroup(context, currentUser, groupName);
                var groupAcc = checkRes.Item2;

                // get existing user
                var accToAdd = context.Accounts.Where(a => a.Name == accountName).FirstOrDefault();
                if (accToAdd == null)
                {
                    throw new DataAccessException($"account {accountName} not found");
                }

                // check if already in the group
                var gAccMapping = (from item in context.GroupUserMappings
                                   where item.GroupId == groupAcc.Id && item.MemberId == accToAdd.Id
                                   select item).FirstOrDefault();
                if (gAccMapping != null)
                {
                    return;
                }

                context.GroupUserMappings.Add(new GroupUserMapping() { Group = groupAcc, Member = accToAdd, IsOwner = false });
                context.SaveChanges();
            }
        }

        public void RemoveAccountFromGroup(string currentUser, string accountName, string groupName)
        {
            using (var context = new EntityContext())
            {
                var checkRes = EnsureCanOperateGroup(context, currentUser, groupName);
                var groupAcc = checkRes.Item2;

                // get existing account
                var accToRemove = context.Accounts.Where(a => a.Name == accountName).FirstOrDefault();
                if (accToRemove == null)
                {
                    throw new DataAccessException($"account {accountName} not found");
                }

                // check if use exists in the group or to remove is the admin, stop
                var gAccMapping = (from item in context.GroupUserMappings
                                   where item.GroupId == groupAcc.Id && item.MemberId == accToRemove.Id
                                   select item).FirstOrDefault();
                if (gAccMapping == null || gAccMapping.IsOwner)
                {
                    return;
                }

                context.GroupUserMappings.Remove(gAccMapping);
                context.SaveChanges();
            }
        }

        public User GetUser(string currentUser, string userName)
        {
            using (var context = new EntityContext())
            {
                var currentAcc = GetUserInner(context, currentUser);
                if (currentUser != "admin" && currentUser != userName)
                {
                    throw new DataAccessException("permission denied");
                }
                var acc = GetUserInner(context, userName);
                var user = context.Users.Where(x => x.AccountId == acc.Id).FirstOrDefault();
                return new User() { Name = user.Name, Email = user.Email };
            }
        }

        public Group GetGroup(string currentUser, string groupName)
        {
            using (var context = new EntityContext())
            {
                var currentAcc = GetUserInner(context, currentUser);
                var groupAcc = GetGroupInner(context, groupName);
                var group = (from item in context.Groups
                            where item.AccountId == groupAcc.Id
                            select item).FirstOrDefault();
                return new Group() { Name = group.Name, Owner = group.Owner };
            }           
        }

        public List<Group> GetOwnedGroups(string currentUser, string userName)
        {
            List<Group> res = new List<Group>();
            using (var context = new EntityContext())
            {
                var currentAcc = GetUserInner(context, currentUser);
                var groups = from item in context.Groups
                             where item.Owner == userName
                             select item;
                foreach (var group in groups)
                {
                    var groupAcc = context.Accounts.Where(x => !x.IsDisabled && x.Id == group.AccountId && x.Type == "Group").FirstOrDefault();
                    if (groupAcc != null)
                    {
                        res.Add(new Group() { Name = group.Name, Owner = userName });
                    }
                }
            }
            return res;
        }

        public List<Account> GetMembersOfGroup(string currentUser, string groupName)
        {
            List<Account> res = new List<Account>();
            using (var context = new EntityContext())
            {
                var currentAcc = GetUserInner(context, currentUser);
                var groupAcc = GetGroupInner(context, groupName);
                var memberAccs = from item in context.GroupUserMappings
                                 where item.GroupId == groupAcc.Id && !item.Member.IsDisabled
                                 select item.Member;
                foreach (var memberAcc in memberAccs)
                {
                    res.Add(new Account() { Name = memberAcc.Name, Type = memberAcc.Type});
                }
            }
            return res;
        }

        public List<Group> GetMembershipsOfAccount(string currentUser, string accName)
        {
            List<Group> res = new List<Group>();
            using (var context = new EntityContext())
            {
                var currentAcc = GetUserInner(context, currentUser);
                var acc = GetAccountInner(context, accName);
                var accMemberships = from item in context.GroupUserMappings
                                     where item.MemberId == acc.Id && !item.Group.IsDisabled
                                     select item.Group;
                foreach (var accMembership in accMemberships)
                {
                    var group = context.Groups.Where(x => x.AccountId == accMembership.Id).FirstOrDefault();
                    res.Add(new Group() { Name = group.Name, Owner = group.Owner });
                }
            }
            return res;
        }

        private Tuple<Account, Account> EnsureCanOperateGroup(EntityContext context, string currentUser, string groupName)
        {
            // find who is requesting the function
            var currentAcc = context.Accounts.Where(a => a.Name == currentUser).FirstOrDefault();
            if (currentAcc == null || currentAcc.Type != "User")
            {
                throw new DataAccessException($"user {currentUser} not found");
            }

            // find group
            var groupAcc = context.Accounts.Where(a => a.Name == groupName).FirstOrDefault();
            if (groupAcc == null || groupAcc.Type != "Group")
            {
                throw new DataAccessException($"group {groupName} not found");
            }

            // check if op is the owner
            var gum = (from item in context.GroupUserMappings
                       where item.GroupId == groupAcc.Id && item.MemberId == currentAcc.Id
                       select item).FirstOrDefault();
            if (gum == null || !gum.IsOwner)
            {
                throw new DataAccessException($"permission denied");
            }

            return new Tuple<Account, Account>(currentAcc, groupAcc);
        }

        internal Account GetAccountInner(EntityContext context, string name)
        {
            var acc = context.Accounts.Where(a => a.Name == name && !a.IsDisabled).FirstOrDefault();
            if (acc == null)
            {
                throw new DataAccessException($"account {name} not found");
            }
            return acc;
        }

        internal Account GetUserInner(EntityContext context, string name)
        {
            var acc = context.Accounts.Where(a => a.Name == name && !a.IsDisabled).FirstOrDefault();
            if (acc == null || acc.Type != "User")
            {
                throw new DataAccessException($"user {name} not found");
            }
            return acc;
        }

        internal Account GetGroupInner(EntityContext context, string name)
        {
            var acc = context.Accounts.Where(a => a.Name == name && !a.IsDisabled).FirstOrDefault();
            if (acc == null || acc.Type != "Group")
            {
                throw new DataAccessException($"group {name} not found");
            }
            return acc;
        }
    }
}
