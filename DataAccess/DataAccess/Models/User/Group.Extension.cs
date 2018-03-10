// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Partial class for group for mapping
    /// </summary>
    public partial class Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        public Group()
        {
            this.UserGroupMappings = new List<GroupUserMapping>();
            this.GroupMappingsChildGroup = new List<GroupMapping>();
            this.GroupMappingsParentGroup = new List<GroupMapping>();
            this.ResourcePermissions = new List<ResourcePermission>();
        }

        /// <summary>
        /// Gets or sets GroupMappingsChildGroup
        /// this collection will be used on child group
        /// </summary>
        public ICollection<GroupMapping> GroupMappingsChildGroup { get; set; }

        /// <summary>
        /// Gets or sets GroupMappingsParentGroup,
        /// this collection will be used on parent group
        /// </summary>
        public ICollection<GroupMapping> GroupMappingsParentGroup { get; set; }

        /// <summary>
        /// Gets or sets UserGroupMappings
        /// </summary>
        public ICollection<GroupUserMapping> UserGroupMappings { get; set; }

        /// <summary>
        /// Add user/group to this group
        /// </summary>
        /// <param name="entity">User or group to be added this group</param>
        /// <returns>A flag to indicate whether this user/group has been added to group</returns>
        public bool AddToGroup(EntityBase entity)
        {
            if (entity is User)
            {
                var user = entity as User;
                var mapping = new GroupUserMapping() { User = user, Group = this };

                if (!this.UserGroupMappings.Any(m => m.GroupId == this.Id && m.UserId == user.Id))
                {
                    this.UserGroupMappings.Add(mapping);
                    user.UserGroupMappings.Add(mapping);

                    return true;
                }
            }
            else if (entity is Group)
            {
                var group = entity as Group;
                var mapping = new GroupMapping() { ParentGroup = this, ChildGroup = group };

                if (!this.GroupMappingsParentGroup.Any(m => m.ParentGroupId == this.Id && m.ChildGroupId == group.Id))
                {
                    this.GroupMappingsParentGroup.Add(mapping);
                    group.GroupMappingsChildGroup.Add(mapping);

                    return true;
                }
            }
            else
            {
                throw new DataAccessException($"Invalid entity: unsupported type");
            }

            return false;
        }
    }
}
