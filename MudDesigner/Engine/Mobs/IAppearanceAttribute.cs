/* IAppearanceAttribute
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Appearance information related to the character that can be displayed to the players
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// Appearance information related to the character that can be displayed to the players
    /// </summary>
    public interface IAppearanceAttribute
    {
        //Name of this appearance attribute. Example: Hair
        string Name { get; set; }

        //Description of this appearance attribute.  Example: With long flowing hair that shines brightly in the sunlight.
        string Description { get; set; }

        //Value that overrides the default description. Used for custom values that the player sets.
        string Value { get; set; }
    }   
}
