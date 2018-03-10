// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base class for entity
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class.
        /// </summary>
        protected EntityBase() => this.Id = Guid.NewGuid();

        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entity is disabled
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
