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
    public class JobManager
    {
        private static readonly Lazy<JobManager> lazy = new Lazy<JobManager>(() => new JobManager());

        public static JobManager Instance { get { return lazy.Value; } }

        private JobManager()
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
        public Guid AddJob(string op, string jobSerialized)
        {
            using (var context = new EntityContext())
            {
                var opAcc = UserManager.Instance.GetUserInner(context, op);
                var job = new Job()
                {
                    CreatedBy = op,
                    CreatedOn = DateTime.UtcNow,
                    Status = "NotStarted"
                };
                context.Jobs.Add(job);
                context.SaveChanges();
                return job.Id;
            }
        }

        public void AssignRoleToAccount(string op, Guid jobId, string roleName)
        {
            using (var context = new EntityContext())
            {
                var job = context.Jobs.FirstOrDefault(x => x.Id == jobId);
                if (job == null)
                {
                    throw new DataAccessException($"job {jobId} not found");
                }

                // check if op is the owner
                if (job.CreatedBy != op)
                {
                    throw new DataAccessException($"permission denied");
                }
            }
            AuthorizationManager.Instance.AssignRoleToAccount(op, "Job", jobId, op, roleName);
        }

        public void RemoveRoleFromAccount(string op, Guid jobId, string roleName)
        {
            using (var context = new EntityContext())
            {
                var job = context.Jobs.FirstOrDefault(x => x.Id == jobId);
                if (job == null)
                {
                    throw new DataAccessException($"job {jobId} not found");
                }

                // check if op is the owner
                if (job.CreatedBy != op)
                {
                    throw new DataAccessException($"permission denied");
                }
            }
            AuthorizationManager.Instance.RemoveRoleFromAccount(op, "Job", jobId, op, roleName);
        }

        public bool CheckPermission(string op, string userName, Guid jobId, string permissionName)
        {
            return AuthorizationManager.Instance.CheckPermission(op, userName, "Job", jobId, permissionName);
        }
    }
}
