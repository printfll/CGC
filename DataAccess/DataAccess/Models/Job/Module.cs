// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Module.cs" company="Microsoft">
//   Microsoft 2018
// </copyright>
// <summary>
//   A module is a user defined execution unit to run as a node in
//   an experiment
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.CGC.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A module is a user defined execution unit to run as a node in
    /// an experiment
    /// </summary>
    public class Module : NamedEntity
    {
        /// <summary>
        /// Family Id field
        /// </summary>
        private Guid familyId;

        /// <summary>
        /// Gets or sets module configuration
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// Gets or sets an unique identifier to indicate whether this module
        /// belongs to a given family
        /// </summary>
        public Guid FamilyId
        {
            get
            {
                if (this.familyId == Guid.Empty)
                {
                    this.familyId = Guid.NewGuid();
                }

                return this.familyId;
            }

            set
            {
                this.familyId = value;
            }
        }

        /// <summary>
        /// Gets or sets version string of this module
        /// </summary>
        public string Version { get; set; }
    }
}