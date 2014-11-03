//-----------------------------------------------------------------------
// <copyright file="IPersistedObject.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract to objects that need to be persisted to a data store.
    /// </summary>
    public interface IPersistedObject
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        int Id { get; set; }
    }
}
