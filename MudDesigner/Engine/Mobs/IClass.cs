/* IClass
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: A interface contract that provides properties and methods for creating custom character classes
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
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

        //TODO: Create a IEquipmentType interface, allowing class restrictions on Equipment.
    }
}
