using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Scripting;

using MudDesigner.Scripts.Default.Commands;
using MudDesigner.Scripts.Default.Game;
using MudDesigner.Scripts.Default.States.CreateCharacter;
using MudDesigner.Scripts.Default.Environment;
using log4net;

namespace MudDesigner.Scripts.Default.States.Login
{
    public class LoginCompleted : IState
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BasePlayer)); 

        private ServerDirector director;
        private IPlayer connectedPlayer;

        //Used to manage the state of the Login in a more readable manor
        private enum CurrentState
        {
            EnteringName,
            EnteringPassword,
            CharacterSelection,
        }
        private CurrentState currentState;

        public LoginCompleted(ServerDirector serverDirector)
        {
            director = serverDirector;
            currentState = CurrentState.EnteringName;
        }

        public void Render(IPlayer player)
        {
        }

        public ICommand GetCommand()
        {
            

            var File = new FileIO();
            if(connectedPlayer.Location == null)
            {
                string startRoom = EngineSettings.Default.InitialRoom;
                string[] locations = startRoom.Split('>');

                if (locations.Length < 3)
                {
                    Log.Error("The Server does not have a starting room set!");
                    connectedPlayer.SendMessage(
                        "The server does not have a starting room set! Please contact the server administrator.");
                    return new NoOpCommand();
                }

                IWorld world = director.Server.Game.World;
                IRealm realm = world.GetRealm(locations[0]);
                IZone zone = realm.GetZone(locations[1]);
                IRoom room = zone.GetRoom(locations[2]);
                
                File.Save(connectedPlayer, Path.Combine("saves", EngineSettings.Default.PlayerSavePath, string.Format("{0}.char", connectedPlayer.Username)));
                
                connectedPlayer.Move(room);
            }
            else
            {
                
                File.Save(connectedPlayer, Path.Combine("saves", EngineSettings.Default.PlayerSavePath, string.Format("{0}.char", connectedPlayer.Username)));
                connectedPlayer.Move(connectedPlayer.Location);
                
                


            }

            return new LookCommand();
        }
    }
}
