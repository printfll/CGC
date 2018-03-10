// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class for defining mapping between user and group
    /// https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many
    /// Many-to-many relationships without an entity class to represent the join table are not yet supported.
    /// </summary>
    public class GroupUserMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Group")]
        public Guid GroupId { get; set; }

        [Required]
        public virtual Account Group { get; set; }

        [ForeignKey("Member")]
        public Guid MemberId { get; set; }

        [Required]
        public virtual Account Member { get; set; }

        public bool IsOwner { get; set; }

    }
}
