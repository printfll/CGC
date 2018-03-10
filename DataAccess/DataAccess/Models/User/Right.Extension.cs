namespace Microsoft.CGC.DataAccess.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension class for Right, which contains RoleRightMappings collection for 
    /// defining mapping between role and right in .Net core
    /// </summary>
    public partial class Permission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
            this.RoleRightMappings = new List<RolePermissionMapping>();
        }

        /// <summary>
        /// Gets or sets RoleRightMappings
        /// </summary>
        public ICollection<RolePermissionMapping> RoleRightMappings { get; set; }
    }
}
