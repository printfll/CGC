namespace Microsoft.CGC.DataAccess.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Partial class for role, which contains mapping collections
    /// </summary>
    public partial class Role
    {
        public Role()
        {
            this.ResourcePermissions = new List<ResourcePermission>();
            this.RoleRightMappings = new List<RolePermissionMapping>();
        }

        public ICollection<ResourcePermission> ResourcePermissions { get; set; }

        public ICollection<RolePermissionMapping> RoleRightMappings { get; set; }

        public void AddRight(Permission right)
        {
            if (!this.RoleRightMappings.Any(r => r.RightId == right.Id))
            {
                this.RoleRightMappings.Add(new RolePermissionMapping() { Right = right, Role = this });
            }
        }

        public void AddRights(ICollection<Permission> rights)
        {
            foreach (var right in rights)
            {
                this.AddRight(right);
            }
        }
    }
}
