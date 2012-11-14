/* NoOpCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: This command performs nothing. It is used when the engine needs to continue a current state but perform no commands
 */

//Microsoft .NET using statements
using System.Net.Sockets;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// This command performs nothing. It is used when the engine needs to continue a current state but perform no commands
    /// </summary>
    public class NoOpCommand : ICommand
    {
        public void Execute(IPlayer player)
        {
            // We are doing nothing on purpose.
            // This is a No operation command, aka do nothing.
            // good for silently changing states or modes.
        }
    }
}