using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MudEngine.Core;

namespace MudEngine.Runtime
{
    public class CommandSystem
    {
        public Dictionary<string, ICommand> Commands { get; private set; }

        public CommandSystem()
        {
            Commands = new Dictionary<string, ICommand>();
            LoadCommands();
        }

        public List<ICommand> GetCommands()
        {
            List<ICommand> collection = new List<ICommand>();

            foreach (ICommand c in this.Commands.Values)
                collection.Add(c);

            return collection;
        }

        public ICommand GetCommand(string command)
        {
            foreach (ICommand c in this.Commands.Values)
            {
                if (c.Name.ToLower() == command.ToLower())
                    return c;
            }

            return null;
        }

        public bool IsValidCommand(string command)
        {
            if (this.Commands.ContainsKey(command))
                return true;
            else
                return false;
        }

        public void Execute(string command, ICharacter character)
        {
            string key = command.Insert(0, "Command");

            foreach (string k in this.Commands.Keys)
            {
                if (key.ToLower().Contains(k.ToLower()))
                {
                    ICommand cmd = this.Commands[k];
                    try
                    {
                        cmd.Execute(command, character);
                    }
                    catch (Exception ex)
                    {
                        throw new NotImplementedException();
                    }

                    return;
                }
            }

            //TODO: Inform player that this was not a valid command.
        }

        private void LoadCommands()
        {
            this.LoadCommandLibrary(Assembly.GetExecutingAssembly(), true);
        }
        
        public void LoadCommandLibrary(Assembly commandLibrary)
        {
            LoadCommandLibrary(commandLibrary, true);
        }

        public void LoadCommandLibrary(Assembly commandLibrary, bool purgeLoadedCommands)
        {
            if (purgeLoadedCommands)
                PurgeCommands();

            if (commandLibrary == null)
                return;

            foreach (Type type in commandLibrary.GetTypes())
            {
                //All commands implement the ICommand interface.
                //If that interface is not present on this Type, skip and go to the next one.
                if (!type.IsInterface)
                    continue;

                if (type.GetInterface("ICommand") == null)
                    continue;

                ICommand cmd = (ICommand)Activator.CreateInstance(type);

                if (cmd != null)
                {
                    //Fail safe measures.  Ensure that we always have a name assigned to the commands.
                    if ((cmd.Name == "") || (cmd.Name == null))
                        cmd.Name = cmd.GetType().Name.ToLower();
                    else
                        cmd.Name = cmd.Name.ToLower(); //Commands are always stored in lower case.

                    if (this.Commands.ContainsKey(cmd.Name))
                        continue; //No overriding supported.  Skip this command.

                    //Everything checks out ok. Add the command to our collection.
                    this.Commands.Add(cmd.Name, cmd);
                }
            }
        }

        public void PurgeCommands()
        {
            this.Commands.Clear();
        }
    }
}
