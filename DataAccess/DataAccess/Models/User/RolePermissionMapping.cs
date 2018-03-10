// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A class to define role and right mapping
    /// </summary>
    public class RolePermissionMapping
    {
        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets RoleId
        /// </summary>
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets Role
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets RightId
        /// </summary>
        [ForeignKey("Permission")]
        public Guid PermissionId { get; set; }

        /// <summary>
        /// Gets or sets Right
        /// </summary>
        public Permission Permission { get; set; }

        
    }
}
