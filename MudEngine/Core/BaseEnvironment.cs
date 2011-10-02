using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public abstract class BaseEnvironment : BaseObject, IEnvironment
    {
        /// <summary>
        /// Gets or Sets if this environment will be safe from harmful objects
        /// </summary>
        [Category("Environment Settings")]
        [Description("Sets if the environment will be safe from harmful objects.")]
        public bool IsSafe {get;set;}

        /// <summary>
        /// Gets a collection of all occupants, either players or NPC, within this environment.
        /// </summary>
        [Browsable(false)]
        public List<string> CurrentOccupants {get; private set;}

        /// <summary>
        /// Gets or Sets the way that the environment smells.
        /// </summary>
        [Category("Environment Settings")]
        [Description("Sets how the environment will smell.")]
        public string Smell {get;set;}

        /// <summary>
        /// Gets or Sets how the environment feels.
        /// </summary>
        [Category("Environment Settings")]
        [Description("Sets how the environment feels.")]
        public string Feel {get;set;}

        /// <summary>
        /// Gets or Sets the sounds that can be heard within this environment.
        /// </summary>
        [Category("Environment Settings")]
        [Description("Sets the sounds that can be heard within this environment")]
        public string Listen { get; set; }

        protected BaseGame ActiveGame { get; private set; }

        public BaseEnvironment(BaseGame game)
        {
            this.ActiveGame = game;
            this.ID = this.ActiveGame.GetAvailableID();
        }

        /// <summary>
        /// Performs any actions needed when a character occupies the environment.
        /// </summary>
        /// <param name="character"></param>
        public virtual void OnOccupantEnter(ICharacter character)
        {
            if (!CurrentOccupants.Contains(character.Name))
                CurrentOccupants.Add(character.Name);
        }

        /// <summary>
        /// Performs needed actions when a character leaves the occupied environment.
        /// </summary>
        /// <param name="character"></param>
        public virtual void OnOccupantExit(ICharacter character)
        {
            if (!CurrentOccupants.Contains(character.Name))
                CurrentOccupants.Remove(character.Name);
        }

        bool IEnvironment.IsSafe
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

        List<string> IEnvironment.CurrentOccupants
        {
            get { throw new NotImplementedException(); }
        }

        string IEnvironment.Smell
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

        string IEnvironment.Feel
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

        string IEnvironment.Listen
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

        void IEnvironment.OnOccupantEnter(ICharacter character)
        {
            throw new NotImplementedException();
        }

        void IEnvironment.OnOccupantExit(ICharacter character)
        {
            throw new NotImplementedException();
        }

        string IObject.Name
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

        string IObject.Filename
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

        string IObject.Description
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
