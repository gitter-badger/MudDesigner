using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core;
using MudEngine.Core.Interfaces;
using MudEngine.Game.Characters;
using MudEngine.Game;
using MudEngine.GameScripts;

namespace MudEngine.Game.Environment
{
    public class Environment : BaseScript, IGameComponent, ISavable, IUpdatable
    {
        /// <summary>
        /// Gets or Sets the filename for this environment when it is saved.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or Sets if this object is enabled.  When disabled, characters can not traverse it.
        /// If a character has the Role of Builder or higher than can enter the Environment reqgardless 
        /// if it is enabled or not.  If the RequiredRole for this environment is set to Admin than
        /// only a Admin may enter.
        /// </summary>
        public Boolean Enabled { get; set; }

        /// <summary>
        /// Gets what the minimum Required Role is in order to access this environment.
        /// </summary>
        public CharacterRoles RequiredRole { get; private set; }

        public Environment(StandardGame game, String name, String description)
            : base(game, name, description)
        {
            //Default to Builder or higher when created.
            //Builders will need to set this to CharacterRoles.Player when environment construction is completed.
            this.RequiredRole = CharacterRoles.Builder;

            //Always default the environment to disabled so that players
            //can't access the environment during construction.
            this.Enabled = false;
        }

        public virtual void Initialize()
        {

        }

        public virtual void Destroy()
        {
            this.Enabled = false;
        }

        public override bool Save(String filename, Boolean ignoreFileWrite)
        {
            base.Save(filename, true);

            SaveData.AddSaveData("Filename", this.Filename);
            SaveData.AddSaveData("Enabled", this.Enabled.ToString());
            SaveData.AddSaveData("RequiredRole", this.RequiredRole.ToString());

            if (!ignoreFileWrite)
                return this.SaveData.Save(filename);
            else
                return true;
        }

        public override void Load(string filename)
        {
            base.Load(filename);

            try { this.Filename = this.SaveData.GetData("Filename"); }
            catch { LoadFailedMessage("Filename"); }

            try { this.Enabled = Convert.ToBoolean(this.SaveData.GetData("Enabled")); }
            catch { this.LoadFailedMessage("Enabled");}

            try
            {
                String role = this.SaveData.GetData("RequiredRole");

                this.RequiredRole = CharacterRole.GetRole(role);
            }
            catch { this.LoadFailedMessage("RequiredRole"); }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
