/* InventoryBounds
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: An enumerator for determining if items can be moved from character to character or not.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Objects
{
    /// <summary>
    /// An enumerator for determining if items can be moved from character to character or not.
    /// </summary>
    public enum InventoryBounds
    {
        None,
        AccountBound,
        CharacterBound
    }
}
