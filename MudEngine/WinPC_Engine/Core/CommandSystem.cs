using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MudEngine.Core.Interface;
using MudEngine.Game.Characters;

namespace MudEngine.Core
{
    public class CommandSystem
    {
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

        public Dictionary<String, ICommand> CommandCollection { get; private set; }

        public CommandSystem(Dictionary<String, ICommand> commands)
        {
            this.CommandCollection = new Dictionary<string, ICommand>();
            this.CommandCollection = commands;
            
            //Handled by the StandardGame class now.
            //LoadCommands();
        }

        public List<ICommand> GetCommands()
        {
            List<ICommand> collection = new List<ICommand>();

            foreach (ICommand c in CommandSystem.Commands.Values)
                collection.Add(c);

            return collection;
        }

        public ICommand GetCommand(string command)
        {
            foreach (ICommand c in CommandSystem.Commands.Values)
            {
                if (c.Name.ToLower() == command.ToLower())
                    return c;
            }

            return null;
        }

        public bool IsValidCommand(string command)
        {
            if (CommandSystem.Commands.ContainsKey(command))
                return true;
            else
                return false;
        }

        public void Execute(string command, StandardCharacter character)
        {
            string key = command.Insert(0, "Command");

            foreach (string k in CommandSystem.Commands.Keys)
            {
                if (key.ToLower().Contains(k.ToLower()))
                {
                    ICommand cmd = CommandSystem.Commands[k];
                    try
                    {
                        cmd.Execute(command, character);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    return;
                }
            }

            //TODO: Inform player that this was not a valid command.
        }

        public static void LoadCommands()
        {
            LoadCommandLibrary(Assembly.GetExecutingAssembly(), true);
        }
        
        public static void LoadCommandLibrary(Assembly commandLibrary)
        {
            LoadCommandLibrary(commandLibrary, true);
        }

        public static void LoadCommandLibrary(Assembly commandLibrary, bool purgeLoadedCommands)
        {
            if (purgeLoadedCommands)
                PurgeCommands();

            if (commandLibrary == null)
                return;

            foreach (Type type in commandLibrary.GetTypes())
            {
                //All commands implement the ICommand interface.
                //If that interface is not present on this Type, skip and go to the next one.
                if (type.GetInterface("ICommand") == null)
                    continue;
                else if (type.IsAbstract)
                    continue;

                ICommand cmd = (ICommand)Activator.CreateInstance(type);

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

        public static void PurgeCommands()
        {
            Commands.Clear();
        }
    }
}
