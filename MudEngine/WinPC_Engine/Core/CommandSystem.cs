using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MudEngine.Core;
using MudEngine.Core.Interfaces;
using MudEngine.Game.Characters;

namespace MudEngine.Core
{
    /// <summary>
    /// The command system will process string based commands and execute any class that implements ICommand.
    /// ICommand.Name must match the command string passed in CommandSystem.Execute()
    /// </summary>
    public class CommandSystem
    {
        /// <summary>
        /// A collection of all command classes and their associated Name key.
        /// </summary>
        public static Dictionary<string, ICommand> Commands
        {
            get
            {
                if (_Commands == null)
                    _Commands = new Dictionary<string, ICommand>();
                return _Commands;
            }
            private set
            {
                _Commands = value;
            }
        }
        private static Dictionary<String, ICommand> _Commands;

        /// <summary>
        /// A copy of the Static CommandSystem.Commands property that this instance will use.
        /// </summary>
        public Dictionary<String, ICommand> CommandCollection { get; private set; }

        /// <summary>
        /// Constructor that accepts a collection of Commands that have already been loaded.
        /// Use the static method CommandSystem.LoadCommands() or CommandSystem.LoadCommandLibrary()
        /// </summary>
        /// <param name="commands"></param>
        public CommandSystem(Dictionary<String, ICommand> commands)
        {
            this.CommandCollection = new Dictionary<string, ICommand>();
            this.CommandCollection = commands;
            
            //Handled by the StandardGame class now.
            //LoadCommands();
        }

        /// <summary>
        /// Returns a collection of Commands that this instance is currently using.
        /// </summary>
        /// <returns></returns>
        public List<ICommand> GetCommands()
        {
            return CommandSystem.Commands.Values.ToList();
        }

        /// <summary>
        /// Returns a command that is matching the supplied String.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommand GetCommand(string command)
        {
            foreach (ICommand c in CommandSystem.Commands.Values)
            {
                if (c.Name.ToLower() == command.ToLower())
                    return c;
            }

            return null;
        }

        /// <summary>
        /// Returns true or false if the command name supplied exists.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool IsValidCommand(string command)
        {
            if (CommandSystem.Commands.ContainsKey(command))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Takes the supplied string command and searches for a Command class that matches it.
        /// If found, it will execute the command class.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="character"></param>
        public Boolean Execute(string command, StandardCharacter character)
        {
            //All Types that implement ICommand must have their class name begin with Command.
            //We must insert the 'Command' string into the beginning of the users Command
            //If user Types "Say" we change it to "CommandSay" and then look for a Type matching "CommandSay"
            string key = command.Insert(0, "Command");

            //Loop through each Key in the Commands collection
            foreach (string k in CommandSystem.Commands.Keys)
            {
                //Check to see if the Key (Command Name) matches the Command we are looking for.
                if (key.ToLower().Contains(k.ToLower()))
                {
                    //Grab a reference to the Command
                    ICommand cmd = CommandSystem.Commands[k];
                    try
                    {
                        //Execute the command
                        return cmd.Execute(command, character);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            
            //Let the player know that it was not a valid command.
            //TODO: Implement another way of performing this.  I don't want game related classes tied to the system.
            character.SendMessage("Invalid Command Used.");
            return false;
        }

        /// <summary>
        /// Loads all of the commands found in the currently loaded assembly.
        /// </summary>
        public static void LoadCommands()
        {
            LoadCommandLibrary(Assembly.GetExecutingAssembly(), true);
        }
        
        /// <summary>
        /// Loads all of the commands found within the assembly specified.
        /// </summary>
        /// <param name="commandLibrary"></param>
        public static void LoadCommandLibrary(Assembly commandLibrary)
        {
            LoadCommandLibrary(commandLibrary, true);
        }

        /// <summary>
        /// Loads all of the commands found within the assembly specified.
        /// All existing commands will be purged.  If there are existing instances of CommandSystem being used
        /// they will need to refresh their private Command collection via the static Property CommandSystem.Commands
        /// </summary>
        /// <param name="commandLibrary"></param>
        /// <param name="purgeLoadedCommands"></param>
        public static void LoadCommandLibrary(Assembly commandLibrary, bool purgeLoadedCommands)
        {
            //Check if we need to purge all of the commands.
            if (purgeLoadedCommands)
                PurgeCommands();

            if (commandLibrary == null)
                return;

            //Loop through each Type in the assembly provided.
            foreach (Type type in commandLibrary.GetTypes())
            {
                //All commands implement the ICommand interface.
                //If that interface is not present on this Type, skip and go to the next one.
                if (type.GetInterface("ICommand") == null)
                    continue;
                else if (type.IsAbstract)
                    continue;

                //Create a instance of the Type for use.
                ICommand cmd = (ICommand)Activator.CreateInstance(type);

                //If we have a instance, lets make sure we don't already have a command
                //with that name.  If not, add it to the Commands collection.
                if (cmd != null)
                {
                    //Fail safe measures.  Ensure that we always have a name assigned to the commands.
                    if ((cmd.Name == "") || (cmd.Name == null))
                        cmd.Name = cmd.GetType().Name.ToLower();
                    else
                        cmd.Name = cmd.Name.ToLower(); //Commands are always stored in lower case.

                    if (Commands.ContainsKey(cmd.Name))
                        continue; //No overriding supported.  Skip this command.

                    //Everything checks out ok. Add the command to our collection.
                    Commands.Add(cmd.Name, cmd);
                }
            }
        }

        /// <summary>
        /// Purges the global Command collection.  This does not affect any class that is using
        /// a Instance of this Type.
        /// </summary>
        public static void PurgeCommands()
        {
            Commands.Clear();
        }
    }
}
