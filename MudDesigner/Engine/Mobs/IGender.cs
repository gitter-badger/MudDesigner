/* IGender
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: A interface contract that provides properties and methods for creating custom character Genders
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// A interface contract that provides properties and methods for creating custom character Genders
    /// </summary>
    public interface IGender
    {
        /// <summary>
        /// Gets or Sets the name of this Gender
        /// </summary>
        string Name { get; set; }
    }
}
