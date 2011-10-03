using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Gets or Sets a collection of help topics related to this command.
        /// </summary>
        [Browsable(false)]
        public List<string> Help { get; set; }
        
        public BaseCommand()
        {
            Help = new List<string>();
            this.Name = this.GetType().Name.Substring("Command".Length);
        }
        
        /// <summary>
        /// Executes the command for the character supplied.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="character"></param>
        public abstract void Execute(string command, ICharacter character);

        public string Name {get;set;}

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
