//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

//MudEngine
using MudEngine.FileSystem;
using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;
using MudEngine.Core;
using MudEngine.Scripting;

namespace MudEngine.GameManagement
{
    public enum ObjectTypes
    {
        Realm,
        Zone,
        Room,
        Character,
        Item,
        Standard,
        //Any
    }

    public class GameWorld
    {
        /// <summary>
        /// Gets the collection of Objects that do not belong to any other Type of categories.
        /// </summary>
        public List<BaseObject> Objects { get; private set; }
        /// <summary>
        /// Gets the collection of Items that are available in the game world.
        /// </summary>
        public List<BaseItem> Items { get; private set; }

        /// <summary>
        /// Gets the collection of Characters (NPC/Monster) that are available in the Game world
        /// </summary>
        //TODO: This should be BaseAI
        public List<BaseCharacter> Characters { get; private set; }

        /// <summary>
        /// Gets the collection of Realms currently available in the game world
        /// </summary>
        public List<Realm> RealmCollection { get; private set; }

        private Game _Game;

        public GameWorld(Game game)
        {
            _Game = game;

            Objects = new List<BaseObject>();
            Items = new List<BaseItem>();
            Characters = new List<BaseCharacter>();
            RealmCollection = new List<Realm>();
        }

        public void Start()
        {
            //See if we have an Initial Realm set
            //TODO: Check for saved Realm files and load
            Log.Write("Initializing World...");
            foreach (Realm r in RealmCollection)
            {
                if (r.IsInitialRealm)
                {
                    _Game.InitialRealm = r;
                    break;
                }
            }
        }

        public void Save()
        {
            //Save all of the Environments
            for (Int32 x = 0; x <= RealmCollection.Count - 1; x++)
            {
                RealmCollection[x].Save();
            }
        }

        public void Load()
        {
            String filename = _Game.GameTitle + ".ini";

            //Get the Initial Realm information from saved file.
            String realmFile = FileManager.GetData(filename, "InitialRealm");
            String realmFolder = Path.GetFileNameWithoutExtension(realmFile);
            String realmPath = Path.Combine(_Game.DataPaths.Environment, realmFolder, realmFile);

            Log.Write("Restoring Game World...");

            //Loop through each RealmCollection data entry stored in the _Game saved file.
            foreach (String realm in FileManager.GetCollectionData(filename, "RealmCollection"))
            {
                Log.Write("Restoring Realm " + realm);

                Realm r = new Realm(_Game);

                //Restore the Realm objects properties from file.
                r.Load(Path.Combine(_Game.DataPaths.Environment, Path.GetFileNameWithoutExtension(realm), realm));

                Boolean isFound = false;

                //Loop through each of the Realm objects instanced during startup and find one matching the loaded filename
                for (int x = 0; x != RealmCollection.Count; x++)
                {
                    //If the filenames match, then overwrite the pre-loaded Realm with the restored Realm with the saved data.
                    if (RealmCollection[x].Filename == r.Filename)
                    {
                        RealmCollection[x] = r;
                        isFound = true;
                        break;
                    }
                    else
                    {
                        RealmCollection.Add(r);
                    }
                }

                if (!isFound)
                    RealmCollection.Add(r);
            }


            foreach (Realm r in RealmCollection)
            {
                if (r.IsInitialRealm)
                {
                    _Game.InitialRealm = r;
                }

                foreach (Zone z in r.ZoneCollection)
                {
                    Log.Write("Restoring Zone: " + z.Filename);
                    z.RestoreLinkedRooms();
                }
            }
        }

        /// <summary>
        /// Adds a Realm to the Games current list of Realms.
        /// </summary>
        /// <param name="realm"></param>
        public void AddRealm(Realm realm)
        {
            //If this Realm is set as Initial then we need to disable any previously
            //set Realms to avoid conflict.
            if (realm.IsInitialRealm)
            {
                foreach (Realm r in RealmCollection)
                {
                    if (r.IsInitialRealm)
                    {
                        r.IsInitialRealm = false;
                        break;
                    }
                }
            }

            //Set this Realm as the Games initial Realm
            if (realm.IsInitialRealm)
                _Game.InitialRealm = realm;

            //TODO: Check for duplicate Realms.
            RealmCollection.Add(realm);
        }

        /// <summary>
        /// Returns a reference to the Realm contained within the Realms collection if it exists.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Realm GetRealm(String filename)
        {
            if (!filename.ToLower().EndsWith(".realm"))
                filename += ".realm";

            foreach (Realm r in RealmCollection)
            {
                if (r.Filename.ToLower() == filename.ToLower())
                    return r;
            }

            return null;
        }

        public virtual void Update()
        {
            foreach (BaseCharacter character in Characters)
            {
                character.Update();
            }
        }
    }
}
