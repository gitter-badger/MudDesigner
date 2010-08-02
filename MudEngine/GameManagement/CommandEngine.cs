//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

//MUD Engine
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;
using MudEngine.GameManagement;

namespace MudEngine.GameManagement
{
    public class CommandEngine
    {
        /// <summary>
        /// Gets or Sets a Dictionary list of available commands to use.
        /// </summary>
        public static Dictionary<string, IGameCommand> CommandCollection { get; set; }

        internal Dictionary<string, IGameCommand> _Commands { get; set; }

        public CommandEngine()
        {
            if ((CommandCollection == null) || (CommandCollection.Count == 0))
                CommandEngine.LoadBaseCommands();

            _Commands = CommandCollection;
        }

        public static List<string> GetCommands()
        {
            List<string> temp = new List<string>();

            foreach (string name in CommandEngine.CommandCollection.Keys)
            {
                temp.Add(name);
            }

            return temp;
        }

        public static string GetCommand(object Parameter)
        {
            List<object> objectList = (List<object>)Parameter;

            foreach (object obj in objectList)
            {
                if (obj is string)
                    return (string)obj;
            }

            return null;
        }

        public static bool IsValidCommand(string Name)
        {
            if (CommandEngine.CommandCollection.ContainsKey(Name.ToLower()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Executes the specified command name if it exists in the Commands Dictionary.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public CommandResults ExecuteCommand(string command, BaseCharacter player)
        {
            string commandKey = command.Insert(0, "Command");
            if (Game.IsDebug)
                Log.Write("Executing command: " + command);

            foreach (string key in player.CommandSystem._Commands.Keys)
            {
                if (commandKey.ToLower().Contains(key.ToLower()))
                {
                    return player.CommandSystem._Commands[key.ToLower()].Execute(command, player);
                    //return player.Commands.ExecuteCommand[key.ToLower()]Execute(command, player);
                }
            }

            return new CommandResults();
        }

        public static void LoadBaseCommands()
        {
            LoadCommandLibrary(Assembly.GetExecutingAssembly(), true);
        }

        /// <summary>
        /// Dynamically loads the specified library into memory and stores all of the
        /// classess inheriting from MudCreator.InputCommands.ICommand into the CommandEngines
        /// commands dictionary for use with the project
        /// </summary>
        /// <param name="CommandLibrary"></param>
        public static void LoadCommandLibrary()
        {
            LoadCommandLibrary(Assembly.GetExecutingAssembly());
        }

        public static void LoadCommandLibrary(string libraryFilename)
        {
            if (System.IO.File.Exists(libraryFilename))
            {
                Assembly assem = Assembly.LoadFile(libraryFilename);
                LoadCommandLibrary(assem);
            }
        }

        public static void LoadCommandLibrary(List<Assembly> commandLibraries)
        {
            foreach (Assembly lib in commandLibraries)
                LoadCommandLibrary(lib);
        }

        public static void LoadCommandLibrary(Assembly commandLibrary)
        {
            LoadCommandLibrary(commandLibrary, false);
        }

        public static void LoadCommandLibrary(Assembly commandLibrary, bool purgeOldCommands)
        {
            //no assembly passed for whatever reason, don't attempt to enumerate through it.
            if (commandLibrary == null)
                return;

            Log.Write("Loading commands within " + Path.GetFileName(commandLibrary.Location));

            if (purgeOldCommands)
                CommandEngine.ClearCommands();

            foreach (Type t in commandLibrary.GetTypes())
            {
                if (t.GetInterface(typeof(IGameCommand).FullName) != null)
                {
                    //Use activator to create an instance
                    IGameCommand command = (IGameCommand)Activator.CreateInstance(t);

                    if (command != null)
                    {
                        if (command.Name == null)
                            command.Name = t.Name.ToLower();
                        else //Make sure the command is always in lower case.
                            command.Name = command.Name.ToLower();

                        //Add the command to the commands list if it does not already exist
                        if (CommandEngine.CommandCollection.ContainsKey(command.Name))
                        {
                            //Command exists, check if the command is set to override existing commands or not
                            if (command.Override)
                            {
                                CommandEngine.CommandCollection[command.Name] = command;
                            }
                        }
                        //Command does not exist, add it to the commands list
                        else
                            CommandEngine.CommandCollection.Add(command.Name, command);
                    }
                }
            }
        }

        public static void ClearCommands()
        {
            CommandEngine.CommandCollection = new Dictionary<string, IGameCommand>();
        }
    }
}
