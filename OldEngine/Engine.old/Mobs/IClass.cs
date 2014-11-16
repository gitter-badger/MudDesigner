//-----------------------------------------------------------------------
// <copyright file="IAppearanceAttribute.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// A interface contract that provides properties and methods for creating custom character classes
    /// </summary>
    public interface IClass : IGameObject
    {
        /// <summary>
        /// Gets or Sets a collection of Races that can not have this character class associated with them.
        /// </summary>
        List<IRace> RaceRestrictions { get; set; }

        // TODO: Create a IEquipmentType interface, allowing class restrictions on Equipment.
    }
}
