
//------------------------------------------------------------------------------
// This code was generated by a tool.
//
//   Tool : Bond Compiler 0.10.1.0
//   File : common_types.cs
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// <auto-generated />
//------------------------------------------------------------------------------


// suppress "Missing XML comment for publicly visible type or member"
#pragma warning disable 1591


#region ReSharper warnings
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameter.Local
// ReSharper disable RedundantUsingDirective
#endregion

namespace Microsoft.CGC.Comm
{
    using System.Collections.Generic;

    [global::Bond.Schema]
    [System.CodeDom.Compiler.GeneratedCode("gbc", "0.10.1.0")]
    public partial class EntityBase
    {
        [global::Bond.Id(0), global::Bond.Type(typeof(string))]
        public System.Guid Id { get; set; }

        public EntityBase()
            : this("Microsoft.CGC.Comm.EntityBase", "EntityBase")
        {}

        protected EntityBase(string fullName, string name)
        {
            Id = new System.Guid();
        }
    }

    [global::Bond.Schema]
    [System.CodeDom.Compiler.GeneratedCode("gbc", "0.10.1.0")]
    public partial class Entity
    {
        [global::Bond.Id(1)]
        public string Name { get; set; }

        [global::Bond.Id(2), global::Bond.Type(typeof(global::Bond.Tag.nullable<long>))]
        public System.DateTime? CreatedOn { get; set; }

        [global::Bond.Id(3), global::Bond.Type(typeof(global::Bond.Tag.nullable<string>))]
        public string CreatedBy { get; set; }

        [global::Bond.Id(4), global::Bond.Type(typeof(global::Bond.Tag.nullable<long>))]
        public System.DateTime? ModifiedOn { get; set; }

        [global::Bond.Id(5), global::Bond.Type(typeof(global::Bond.Tag.nullable<string>))]
        public string ModifiedBy { get; set; }

        public Entity()
            : this("Microsoft.CGC.Comm.Entity", "Entity")
        {}

        protected Entity(string fullName, string name)
        {
            Name = "";
        }
    }
} // Microsoft.CGC.Comm
