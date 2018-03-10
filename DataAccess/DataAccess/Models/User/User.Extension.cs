namespace Microsoft.CGC.DataAccess.Models
{
    using System.Collections.Generic;

    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.UserGroupMappings = new List<GroupUserMapping>();
            this.ResourcePermissions = new List<ResourcePermission>();
        }

        /// <summary>
        /// Gets or sets groups this user belongs to
        /// </summary>
        public virtual ICollection<GroupUserMapping> UserGroupMappings { get; set; }

        public ICollection<ResourcePermission> ResourcePermissions { get; set; }
    }
}
