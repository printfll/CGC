// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A user can be assigned a role to grant rights
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets user Email address
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets user Email address
        /// </summary>
        public string Email { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
