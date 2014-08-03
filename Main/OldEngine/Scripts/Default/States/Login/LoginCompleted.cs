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
        private static readonly ILog Log = LogManager.GetLogger(typeof(IPlayer));

        private IServerDirector director;
        private IPlayer connectedPlayer;

        //Used to manage the state of the Login in a more readable manor
        private enum CurrentState
        {
            CompletingLogin,
            SwitchingToDefaultState,
        }
        private CurrentState currentState;

        public LoginCompleted()
        {
            currentState = CurrentState.CompletingLogin;
        }

        public void Render(IPlayer player)
        {
            connectedPlayer = player;
            director = player.Director;
        }

        public ICommand GetCommand()
        {
            var File = new FileIO();
            if (connectedPlayer.Location == null)
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

                if (world == null)
                {
                    Log.Fatal("Failed to get a instance of the game world!");
                    return new NoOpCommand(); //If this is null, then we should end up in a infinite console spam
                }

                IRealm realm = world.GetRealm(locations[0]);
                if (realm == null)
                {
                    Log.Fatal(string.Format("Failed to load Realm {0}", locations[0]));
                    return new NoOpCommand();
                }

                IZone zone = realm.GetZone(locations[1]);
                if (zone == null)
                {
                    Log.Fatal(string.Format("Failed to load Zone {0}", locations[1]));
                    return new NoOpCommand();
                }

                IRoom room = zone.GetRoom(locations[2]);
                if (room == null)
                {
                    Log.Fatal(string.Format("Failed to load Room {0}", locations[2]));
                    return new NoOpCommand();
                }

                connectedPlayer.Move(room);

                File.Save(connectedPlayer, Path.Combine(EngineSettings.Default.PlayerSavePath, string.Format("{0}.char", connectedPlayer.Username)));
            }
            else if (connectedPlayer.Director.Server.Game.World.RoomExists(connectedPlayer.Location.ToString()))
            {
                File.Save(connectedPlayer, Path.Combine(EngineSettings.Default.PlayerSavePath, string.Format("{0}.char", connectedPlayer.Username)));
            }
            else
            {
                //Set as null and re-run through this state again.
                connectedPlayer.Location = null;
                return new NoOpCommand(); //Dont allow it to finish the setup
            }

            return SetupDefaultState();
        }

        private ICommand SetupDefaultState()
        {
            IState defaultState = (IState)ScriptFactory.GetScript(EngineSettings.Default.LoginCompletedState);
            if (defaultState != null)
            {
                connectedPlayer.SwitchState(defaultState);
            }

            return new NoOpCommand();
        }
    }
}
