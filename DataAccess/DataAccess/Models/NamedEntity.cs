// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedEntity.cs" company="Microsoft">
//   Microsoft 2018
// </copyright>
// <summary>
//   A named entity is an entity with name and description
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.CGC.DataAccess.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A named entity is an entity with name and description
    /// </summary>
    public abstract class NamedEntity : Entity
    {
        /// <summary>
        /// Gets or sets entity name
        /// </summary>
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets entity description
        /// </summary>
        public string Description { get; set; }
    }
}