using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Properties;

namespace MudDesigner.Scripts.Default.States.CreateCharacter
{
    public class CreationManager : IState
    {
        private IPlayer connectedPlayer;
        private ServerDirector director;

        public enum CreationState
        {
            CharacterCreation,
            GenderSelect,
            Completed,
        }
        private CreationState currentCreationState;

        public CreationManager(ServerDirector serverDirector, CreationState initialState)
        {
            currentCreationState = initialState;
            director = serverDirector;
        }

        public void Render(Engine.Mobs.IPlayer player)
        {
            connectedPlayer = player;
        }

        public Engine.Commands.ICommand GetCommand()
        {
            switch (currentCreationState)
            {
                case CreationState.CharacterCreation:
                    connectedPlayer.SwitchState(new CreateNewCharacter(director));
                    break;
                case CreationState.GenderSelect:
                    connectedPlayer.SwitchState(new GenderSelect(director));
                    break;
                case CreationState.Completed:
                    //Make sure we have a valid save path
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "saves", EngineSettings.Default.PlayerSavePath, connectedPlayer.Name + ".char");
                    var path = Path.GetDirectoryName(filePath);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Save the player using our serialization class
                    FileIO fileSave = new FileIO();
                    fileSave.Save(connectedPlayer, filePath);
                                        
                    //Broacast we are now done creating the character
                    connectedPlayer.SendMessage("You have completed the character creation process! Enjoy your stay in our world!");
                    connectedPlayer.SendMessage(string.Empty);//blank line.

                    //Display the main menu
                    //TODO - This needs to be replaced with executing a 'look' command for the current room.
                    connectedPlayer.SwitchState(new MainMenuState(director));
                    break;
            }

            return new NoOpCommand();
        }
    }
}
