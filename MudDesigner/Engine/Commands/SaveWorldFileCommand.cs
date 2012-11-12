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

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Saves the current state of the Game World.  This is an admin only command.
    /// </summary>
    public class SaveWorldFileCommand : ICommand
    {
        private IGame currentGame;
        private IPlayer currentPlayer;
        
        public SaveWorldFileCommand(IPlayer player, IGame game)
        {
            currentGame = game;
            currentPlayer = player;
        }

        public void Execute()
        {
            //if the player exists and has the proper role...
            if (currentPlayer != null && (currentPlayer.Role == CharacterRoles.Admin || currentPlayer.Role == CharacterRoles.Owner))
            {
                if (currentGame != null)
                {
                    //Save the world.
                    currentGame.SaveWorld();
                    currentPlayer.SendMessage("Save completed.");
                }
            }
        }
    }
}