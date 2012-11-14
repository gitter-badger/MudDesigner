/* ICommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides the required method for implementing a engine command.
 */

//Microsoft .NET using statements
using System.Net.Sockets;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Provides the required method for implementing a engine command.
    /// </summary>
    public interface ICommand
    {
        void Execute(IPlayer player);
    }
}