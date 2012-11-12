/* LoadFileCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides support for loading the game world.  This is an admin only command.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.IO;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Provides support for loading the game world.  This is an admin only command.
    /// </summary>
    public class LoadFileCommand : ICommand
    {
        private IGame currentGame;
        private IPlayer currentPlayer;

        public LoadFileCommand(IPlayer player, IGame game)
        {
            currentGame = game;
            currentPlayer = player;
        }

        public void Execute()
        {
            //if the player has the correct role..
            if (currentPlayer != null && (currentPlayer.Role == CharacterRoles.Owner || currentPlayer.Role == CharacterRoles.Admin))
            {
                if (currentGame != null)
                {
                    //Restore the world to the state it was in prior to the last save.
                    currentGame.RestoreWorld();
                    currentPlayer.SendMessage("World restoration completed.");
                }
            }
        }
    }
}