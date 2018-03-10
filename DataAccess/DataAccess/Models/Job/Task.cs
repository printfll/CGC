// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Job.cs" company="Microsoft">
//   Microsoft 2018
// </copyright>
// <summary>
//   Class definition for job and job stats
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A job is an instance of Module in experiment
    /// </summary>
    public class Task : Entity
    {
        /// <summary>
        /// Gets or sets job state
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Gets or sets module id which this job is created based on
        /// </summary>
        [ForeignKey("Module")]
        public Guid ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the module which this job is created based on
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// Gets or sets experiment id which this job belongs to
        /// </summary>
        [ForeignKey("Experiment")]
        public Guid ExperimentId { get; set; }

        /// <summary>
        /// Gets or sets experiment which this job belongs to
        /// </summary>
        public virtual Job Experiment { get; set; }
    }
}
