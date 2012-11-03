using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.States
{
    public class MainMenuState : IState
    {

        public ServerDirector Director { get; private set; }
        private IPlayer connectedPlayer { get; set; }

        public MainMenuState(ServerDirector director)
        {
            Director = director;

        }
        public void Render(IPlayer player)
        {
            connectedPlayer = player;
            if(player != null)
            {
                player.SendMessage(string.Format("Welcome {0}, what do you want to do?", player.CharacterName));
            }


            // Some Fancy Menu
            player.SendMessage("");
            player.SendMessage("-----------------------------------------");
            player.SendMessage(string.Format("|{0}|", Director.Server.Game.Name)); // @ToDo: I'll look into Text Centering stuff.
            player.SendMessage("-----------------------------------------");
            //player.SendMessage("| [Enter] a town                        |");
            //player.SendMessage("| [Join] a chat channel                 |");
            //player.SendMessage("| [Save] my current player              |");
            //player.SendMessage("| Save [World] (Debug)                  |");
            //player.SendMessage("| Change some game [Options]            |");
            player.SendMessage("| [Quit] the game                       |");
            player.SendMessage("-----------------------------------------"); 



        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(connectedPlayer);
            switch (input.ToLower())
            {
                    //TODO This needs to use the command SwitchState to switch to room state.
                case "enter":
                    IRoom startRoom = (IRoom)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.LoginRoom, null);
                    connectedPlayer.Move(startRoom);
                    break;
                case "world":
                    var game = Director.Server.Game as Game.Game;
                    if (game != null)
                    {
                        connectedPlayer.SendMessage("Save Success!");
                        return new SaveWorldFileCommand(game);
                    }
                    break;
                case "quit":
                    connectedPlayer.Disconnect();
                    break;
            }

            // We Don't have any commands here yet... but we will! (EnterCommand, JoinCommand, SaveCommand, OptionsCommand, QuitCommand etc)
            return new InvalidCommand(connectedPlayer);
        }
    }
}