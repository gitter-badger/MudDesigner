/* IState
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: A interface contract that provides properties and methods for creating custom character Stats
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// A interface contract that provides properties and methods for creating custom character Stats
    /// </summary>
    public interface IStat
    {
        /// <summary>
        /// Gets or Sets the name of this stat
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or Sets the maximum amount that this stat can have.
        /// </summary>
        int Amount { get; set; }
    }
}
