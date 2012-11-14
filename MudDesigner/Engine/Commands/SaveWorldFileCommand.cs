/* SaveWorldFileCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Saves the current state of the Game world.  This is an admin only command
 */

//Microsoft .NET using statements
using System.IO;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Mobs;
using log4net;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Saves the current state of the Game World.  This is an admin only command.
    /// </summary>
    public class SaveWorldFileCommand : ICommand
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SaveWorldFileCommand)); 
        public void Execute(IPlayer player)
        {
            //if the player exists and has the proper role...
            if (player != null && (player.Role == CharacterRoles.Admin || player.Role == CharacterRoles.Owner))
            {
                if (player.Director.Server.Game != null)
                {
                    //Save the world.
                    player.Director.Server.Game.SaveWorld();
                    player.SendMessage("Save completed.");
                    Log.Info("Save completed.");

                }
            }
        }
    }
}