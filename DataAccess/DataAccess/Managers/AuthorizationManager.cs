// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Utility
{
    using Microsoft.CGC.DataAccess.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for managing permission related operations
    /// </summary>
    public class AuthorizationManager
    {
        private static readonly Lazy<AuthorizationManager> lazy = new Lazy<AuthorizationManager>(() => new AuthorizationManager());

        public static AuthorizationManager Instance { get { return lazy.Value; } }

        private AuthorizationManager()
        {
            bool initialized = true;
            using (var context = new EntityContext())
            {
                var adminAcc = context.Accounts.Where(a => a.Name == "admin").FirstOrDefault();
                initialized = (adminAcc != null);
            }

            if (initialized) { return; }

            // add default Read/Update/Delete permission and default Owner/Reader roles
            using (var context = new EntityContext())
            {
                var rp = new Permission() { Name = "Read" };
                var up = new Permission() { Name = "Update" };
                var dp = new Permission() { Name = "Delete" };
                context.Permissions.Add(rp);
                context.Permissions.Add(up);
                context.Permissions.Add(dp);

                var readerRole = new Role() { Name = "Reader"};
                var ownerRole = new Role() { Name = "Owner" };
                context.Roles.Add(readerRole);
                context.Roles.Add(ownerRole);

                context.RolePermissionMappings.Add(new RolePermissionMapping() { Role = readerRole, Permission = rp});
                context.RolePermissionMappings.Add(new RolePermissionMapping() { Role = ownerRole, Permission = rp });
                context.RolePermissionMappings.Add(new RolePermissionMapping() { Role = ownerRole, Permission = up });
                context.RolePermissionMappings.Add(new RolePermissionMapping() { Role = ownerRole, Permission = dp });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Check an user has the specific right on given resource
        /// </summary>
        /// <param name="userName">User alias, the unique user name</param>
        /// <param name="resourceType">Resource type, can be an experiment, a module etc.</param>
        /// <param name="resourceId">Resource Id, unique id of the specific resource</param>
        /// <param name="permission">Check whether has the right, it can be "Edit", "Read", "Delete" etc.</param>
        /// <returns>A boolean flag to indicate whether the user has this right or not</returns>
        public bool CheckPermission(string currentUser, string userName, string resourceType, Guid resourceId, string permissionName)
        {
            using (var context = new EntityContext())
            {
                var account = UserManager.Instance.GetUserInner(context, userName);

                var permission = context.Permissions.FirstOrDefault(p => p.Name == permissionName);
                if (permission == null)
                {
                    throw new DataAccessException($"{permissionName} is not defined");
                }

                var roles = context.RolePermissionMappings.Where(rpm => rpm.PermissionId == permission.Id).ToList();
                if (roles == null || roles.Count == 0)
                {
                    throw new DataAccessException($"no role has permission {permissionName}");
                }
                HashSet<Guid> rolesHasPermission = new HashSet<Guid>((from item in roles select item.Id).ToList());

                // 1. iterate through all groups this user belongs to check the permission.
                HashSet<Guid> candidateAccs = new HashSet<Guid>() { account.Id };
                HashSet<Guid> checkedAccs = new HashSet<Guid>();
                while (candidateAccs.Count != 0)
                {
                    Guid curAccId = candidateAccs.First();
                    candidateAccs.Remove(curAccId);
                    checkedAccs.Add(curAccId);
                    // get the permission of the group
                    var foundRps = (from item in context.ResourcePermissions
                                    where item.ResourceType == resourceType && item.ResourceId == resourceId &&
                                          item.AccountId == curAccId && rolesHasPermission.Any(x => x == item.RoleId)
                                    select item).ToList();
                    if (foundRps != null && foundRps.Count() != 0) { return true; }

                    // get its parent groups
                    var parents = (from item in context.GroupUserMappings
                                   where item.MemberId == curAccId && !item.Group.IsDisabled
                                   select item.GroupId).ToList();
                    foreach (var parent in parents)
                    {
                        if (!checkedAccs.Contains(parent)) { candidateAccs.Add(parent); }
                    }
                }
                return false;
            }
        }

        public void AssignRoleToAccount(string currentUser, string resourceType, Guid resourceId, string accName, string roleName)
        {
            using (var context = new EntityContext())
            {
                var currentAcc = UserManager.Instance.GetAccountInner(context, currentUser);
                var account = UserManager.Instance.GetUserInner(context, accName);

                var role = context.Roles.FirstOrDefault(p => p.Name == roleName);
                if (role == null)
                {
                    throw new DataAccessException($"{role} is not defined");
                }

                // check if there is already a permission
                var rps = (from item in context.ResourcePermissions
                         where item.ResourceType == resourceType &&
                                 item.ResourceId == resourceId &&
                                 item.AccountId == account.Id &&
                                 item.RoleId == role.Id
                         select item).ToList();
                if (rps != null && rps.Count != 0)
                {
                    return;
                }
                var resourcePermission = new ResourcePermission()
                {
                    ResourceType = resourceType,
                    ResourceId = resourceId,
                    Account = account,
                    Role = role
                };
                context.ResourcePermissions.Add(resourcePermission);
                context.SaveChanges();
            }
        }

        public void RemoveRoleFromAccount(string currentUser, string resourceType, Guid resourceId, string accName, string roleName)
        {
            using (var context = new EntityContext())
            {
                var currentAcc = UserManager.Instance.GetAccountInner(context, currentUser);
                var account = UserManager.Instance.GetUserInner(context, accName);

                var role = context.Roles.FirstOrDefault(p => p.Name == roleName);
                if (role == null)
                {
                    throw new DataAccessException($"{role} is not defined");
                }

                // check if there is already a permission
                var rps = (from item in context.ResourcePermissions
                          where item.ResourceType == resourceType &&
                                  item.ResourceId == resourceId &&
                                  item.AccountId == account.Id &&
                                  item.RoleId == role.Id
                          select item).ToArray();
                if (rps == null || rps.Length == 0)
                {
                    return;
                }
                context.ResourcePermissions.RemoveRange(rps);
                context.SaveChanges();
            }
        }
    }
}
