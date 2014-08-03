/* Editor
 * Product: Mud Designer Editor
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides global access to various Game Objects across the entire Editor toolkit.
 */

//Microsoft .NET Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AllocateThis! Mud Designer Using Statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;

namespace MudDesigner.Editor
{
    /// <summary>
    /// Provides global access to the current Game and various currently selected Game Objects.
    /// </summary>
    public static class Editor
    {
        /// <summary>
        /// Current Game loaded and running
        /// </summary>
        public static IGame Game { get; set; }

        /// <summary>
        /// Current Realm selected by the editor
        /// </summary>
        public static IRealm CurrentRealm { get; set; }

        /// <summary>
        /// Current Zone selected by the editor
        /// </summary>
        public static IZone CurrentZone { get; set; }

        /// <summary>
        /// Current Room selected by the editor.
        /// </summary>
        public static IRoom CurrentRoom { get; set; }
    }
}
