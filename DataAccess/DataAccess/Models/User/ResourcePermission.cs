namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A ResourcePermission is a mapping between resource, user/group and role
    /// </summary>
    public class ResourcePermission
    {
        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets resource type, it can be an experiment, a module
        /// </summary>
        [Required]
        [StringLength(400)]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets resource Id, it can be experiment Id, module Id, group Id
        /// </summary>
        [Required]
        public Guid ResourceId { get; set; }


        /// <summary>
        /// ForeignKey property for account
        /// </summary>
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Gets or sets which account this resource will be shared to
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Gets or sets which role Id the user/group will have against this resource
        /// </summary>
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets which role Id the user/group will have against this resource
        /// </summary>
        public virtual Role Role { get; set; }
    }
}