// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class for defining mapping between groups
    /// https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many
    /// Many-to-many relationships without an entity class to represent the join table are not yet supported.
    /// </summary>
    public class GroupMapping
    {
        /// <summary>
        /// Gets or sets ParentGroupId
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [ForeignKey("ParentGroup")]
        public Guid ParentGroupId { get; set; }

        /// <summary>
        /// Gets or sets ParentGroup
        /// </summary>
        public Group ParentGroup { get; set; }

        /// <summary>
        /// Gets or sets ChildGroupId
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [ForeignKey("ChildGroup")]
        public Guid ChildGroupId { get; set; }

        /// <summary>
        /// Gets or sets ChildGroup
        /// </summary>
        public Group ChildGroup { get; set; }
    }
}
