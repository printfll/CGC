// <copyright file="Group.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A group is a collection of user/group
    /// </summary>
    public partial class Group
    {
        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Owner { get; set; }

        public string Name { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
