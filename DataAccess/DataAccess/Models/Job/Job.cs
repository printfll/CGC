// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Experiment.cs" company="Microsoft">
//   Microsoft 2018
// </copyright>
// <summary>
//   Enumeration of experiment states
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// An experiment is a set of jobs submitted by user
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Gets or sets entity Id, which is primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(64)]
        public string CreatedBy { get; set; }

        public string Status { get; set; }
    }
}