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
        public static Dictionary<String, IGameCommand> CommandCollection { get; set; }

        internal Dictionary<String, IGameCommand> __Commands { get; set; }

        public CommandEngine()
        {
            if ((CommandEngine.CommandCollection == null) || (CommandEngine.CommandCollection.Count == 0))
                CommandEngine.LoadBaseCommands();

            //_Commands = CommandEngine.CommandCollection;
        }

        public static List<String> GetCommands()
        {
            List<String> temp = new List<String>();

            foreach (String name in CommandEngine.CommandCollection.Keys)
            {
                temp.Add(name);
            }

            return temp;
        }

        public static IGameCommand GetCommand(String command)
        {
            if (IsValidCommand(command))
            {
                foreach (IGameCommand cmd in CommandCollection.Values)
                {
                    if (cmd.Name.ToLower() == command.ToLower())
                        return cmd;
                }
            }

            return null;
        }

        public static String GetCommandName(IGameCommand command)
        {
            return command.Name.Substring("Command".Length);
        }

        public static Boolean IsValidCommand(String Name)
        {
            if (CommandCollection.ContainsKey(Name.ToLower()))
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
        public void ExecuteCommand(String command, BaseCharacter player)
        {
            String commandKey = command.Insert(0, "Command");

            foreach (String key in CommandEngine.CommandCollection.Keys)
            {
                if (commandKey.ToLower().Contains(key.ToLower()))
                {
                    IGameCommand cmd = CommandEngine.CommandCollection[key];
                    try
                    {
                        cmd.Execute(command, player);
                    }
                    catch(Exception ex)
                    {
                        Log.Write("Fatal Error occured while attempting to execute that command " + command.ToUpper());
                        Log.Write("Command Error: " + ex.Source + "." + ex.TargetSite.Name + ": " + ex.Message);
                    }
                    return;
                }
            }

            player.Send("Invalid Command.");
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

        public static void LoadCommandLibrary(String libraryFilename)
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

        public static void LoadCommandLibrary(Assembly commandLibrary, Boolean purgeOldCommands)
        {
            //no assembly passed for whatever reason, don't attempt to enumerate through it.
            if (commandLibrary == null)
                return;

            Log.Write("Loading commands within " + Path.GetFileName(commandLibrary.Location));

            if (purgeOldCommands)
                ClearCommands();

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
                        if (CommandCollection.ContainsKey(command.Name))
                        {
                            //Command exists, check if the command is set to override existing commands or not
                            if (command.Override)
                            {
                                CommandCollection[command.Name] = command;
                            }
                        }
                        //Command does not exist, add it to the commands list
                        else
                            CommandCollection.Add(command.Name, command);
                    }
                }
            }
        }

        public static void ClearCommands()
        {
            CommandCollection = new Dictionary<String, IGameCommand>();
        }
    }
}
