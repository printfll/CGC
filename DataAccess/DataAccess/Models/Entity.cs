// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity is the base object with common properties
    /// </summary>
    public abstract class Entity : EntityBase
    {
        /// <summary>
        /// Gets or sets entity created by user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets entity created time
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets entity modified by user
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets entity modified time
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}