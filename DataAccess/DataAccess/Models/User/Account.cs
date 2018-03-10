// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// A role is a collection of rights
    /// </summary>
    public partial class Account
    {
        public Account()
        {
            IsDisabled = false;
        }

        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets account name, which is unique
        /// </summary>
        [StringLength(64)]
        public string Name { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entity is disabled
        /// </summary>
        public bool IsDisabled { get; set; }

    }
}