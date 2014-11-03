//-----------------------------------------------------------------------
// <copyright file="PersistValueAttribute.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Runtime.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Informs the data store services that a Property value must be persisted.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PersistValueAttribute : Attribute
    {
        /// <summary>
        /// Various different ways that a value can be persisted.
        /// </summary>
        public enum PersistStyle
        {
            RawValue,
            RelatedPersistedObject,
            StringRepresentation,
            CollectionRawValue,
            CollectionRelatedPersistedObject,
            CollectionStringRepresentation,
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistValueAttribute"/> class.
        /// </summary>
        /// <param name="relatedPersistedObject">The related persisted object.</param>
        public PersistValueAttribute(PersistStyle relatedPersistedObject = PersistStyle.RawValue)
        {
            this.ValuePersistanceStyle = relatedPersistedObject;
        }

        /// <summary>
        /// Gets the value determining how to handle persisting the value.
        /// </summary>
        public PersistStyle ValuePersistanceStyle { get; private set; }
    }
}
