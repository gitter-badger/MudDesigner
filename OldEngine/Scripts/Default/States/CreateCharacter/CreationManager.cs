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
using MudDesigner.Engine.Environment;
using MudDesigner.Scripts.Default.Commands;

using log4net;

namespace MudDesigner.Scripts.Default.States.CreateCharacter
{
    public class CreationManager : IState
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CreationManager)); 
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
                    //Broacast we are now done creating the character
                    connectedPlayer.SendMessage("You have completed the character creation process! Enjoy your stay in our world!");
                    connectedPlayer.SendMessage(string.Empty);//blank line.

                    //Trace back up through the environment path to get the World
                    IWorld world = director.Server.Game.World;

                    //Get the initial Room location, and split it up into an array so we can parse it
                    string[] roomPath = EngineSettings.Default.InitialRoom.Split('>');

                    //Make sure we have three entries, Realm, Zone and Room
                    if (roomPath.Length != 3)
                        return new NoOpCommand();

                    //Get the Realm
                    IRealm realm = world.GetRealm(roomPath[0]);
                    if (realm == null)
                        return new NoOpCommand();

                    //Get our Zone
                    IZone zone = realm.GetZone(roomPath[1]);
                    if (zone == null)
                        return new NoOpCommand();

                    //Get the initial Room
                    IRoom room = zone.GetRoom(roomPath[2]);
                    if (room == null)
                        return new NoOpCommand();
                   

                    connectedPlayer.Move(room);

                    //Make sure we have a valid save path
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), EngineSettings.Default.PlayerSavePath, connectedPlayer.Username + ".char");
                    var path = Path.GetDirectoryName(filePath);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Save the player using our serialization class
                    FileIO fileSave = new FileIO();
                    fileSave.Save(connectedPlayer, filePath);

                    connectedPlayer.SwitchState(new EnteringCommandState());
                    Log.Info(string.Format("{0} has just logged in.", connectedPlayer.Name));
                    return new LookCommand();
            }

            return new NoOpCommand();
        }
    }
}
