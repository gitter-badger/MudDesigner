//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//MUD Engine
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;
using MudEngine.GameManagement;

namespace MudEngine.Commands
{
    public class CommandEngine
    {
        /// <summary>
        /// Gets or Sets a Dictionary list of available commands to use.
        /// </summary>
        static internal Dictionary<string, IGameCommand> Commands { get; set; }

        public List<string> GetCommands
        {
            get
            {
                List<string> temp = new List<string>();
                foreach (string name in Commands.Keys)
                {
                    temp.Add(name);
                }

                return temp;
            }
        }

        public bool GetCommand(string Name)
        {
            if (Commands.ContainsKey(Name.ToLower()))
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
        public static CommandResults ExecuteCommand(string Name, BaseCharacter player, GameSetup project, Room room, string command)
        {
            Name = Name.Insert(0, "Command");
            foreach (string key in Commands.Keys)
            {
                if (Name.ToLower().Contains(key.ToLower()))
                {
                    return Commands[key.ToLower()].Execute(player, project, room, command);
                }
            }

            return new CommandResults();
        }
        /// <summary>
        /// Dynamically loads the specified library into memory and stores all of the
        /// classess inheriting from MudCreator.InputCommands.ICommand into the CommandEngines
        /// commands dictionary for use with the project
        /// </summary>
        /// <param name="CommandLibrary"></param>
        public static void LoadAllCommands()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Commands = new Dictionary<string, IGameCommand>();
            foreach (Type t in assembly.GetTypes())
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
                        if (Commands.ContainsKey(command.Name))
                        {
                            //Command exists, check if the command is set to override existing commands or not
                            if (command.Override)
                            {
                                Commands[command.Name] = command;
                            }
                        }
                        //Command does not exist, add it to the commands list
                        else
                            Commands.Add(command.Name, command);
                    }
                }
            }
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
    }
}
