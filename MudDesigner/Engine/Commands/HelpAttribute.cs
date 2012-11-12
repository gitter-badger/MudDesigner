/* HelpAttributes
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Allows for attaching helpful information related to a game command. Attribute can only be attached to the class and not its members
 */

//Microsoft .NET using statements
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// HelpAttribute should be attached to all game Commands. It allows game commands to output helpful information
    /// regarding the command when the internal HelpCommand is used by the player.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HelpAttribute : Attribute
    {
        public string Description { get; set; }

        public HelpAttribute(string description)
        {
            Description = description;
        }
    }
}
