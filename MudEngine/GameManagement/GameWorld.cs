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
using MudEngine.GameObjects.Items;
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
        public List<Realm> Realms { get; private set; }

        private Game _Game;

        public GameWorld(Game game)
        {
            _Game = game;

            Objects = new List<BaseObject>();
            Items = new List<BaseItem>();
            Characters = new List<BaseCharacter>();
            Realms = new List<Realm>();
        }

        public void Start()
        {
            //See if we have an Initial Realm set
            //TODO: Check for saved Realm files and load
            Log.Write("Initializing World...");
            foreach (Realm r in Realms)
            {
                if (r.IsInitialRealm)
                {
                    _Game.InitialRealm = r;
                    break;
                }
            }

            //Check if any the initial room exists or not.
            if ((_Game.InitialRealm == null) || (_Game.InitialRealm.InitialZone == null) || (_Game.InitialRealm.InitialZone.InitialRoom == null))
            {
                Log.Write("ERROR: No initial location defined. Game startup failed!");
                Log.Write("Players will start in the Abyss. Each player will contain their own instance of this room.");
                //return false;
            }
            else
                Log.Write("Initial Location loaded-> " + _Game.InitialRealm.Name + "." + _Game.InitialRealm.InitialZone.Name + "." + _Game.InitialRealm.InitialZone.InitialRoom.Name);

        }

        public void Save()
        {
            //Save all of the Environments
            for (Int32 x = 0; x <= Realms.Count - 1; x++)
            {
                Realms[x].Save(_Game.DataPaths.Environment);
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
                foreach (Realm r in Realms)
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
            Realms.Add(realm);
        }

        /// <summary>
        /// Returs a reference to an object that matches the supplied Type and filename.
        /// Object MUST inherit from BaseObject or one of its child classes in order to be found.
        /// </summary>
        /// <param name="objectType">Determins the Type of object to perform the search for. Using Standard will search for objects that inherit from BaseObject, but none of BaseObjects child Types.</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public BaseObject GetObject(ObjectTypes objectType, String filename)
        {
            BaseObject obj = new BaseObject(_Game);

            switch (objectType)
            {
                case ObjectTypes.Standard:

                    break;
                case ObjectTypes.Character:

                    break;
                case ObjectTypes.Item:

                    break;
                case ObjectTypes.Realm:
                    obj = GetRealm(filename);
                    break;
                case ObjectTypes.Zone:

                    break;
                case ObjectTypes.Room:
                    
                    break;
            }

            return obj;
        }

        /// <summary>
        /// Returns a reference to the Realm contained within the Realms collection if it exists.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Realm GetRealm(String filename)
        {
            foreach (Realm r in Realms)
            {
                if (r.Filename == filename)
                    return r;
            }

            return null;
        }
    }
}
