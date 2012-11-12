/* CharacterRoles
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: An enumeration of Roles that the player can have on the server.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// An enumeration of Roles that the player can have on the server
    /// </summary>
    public enum CharacterRoles
    {
        Standard,
        Builder,
        Admin,
        Owner
    }
}
