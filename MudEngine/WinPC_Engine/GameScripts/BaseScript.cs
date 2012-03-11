using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

using MudEngine.Game;
using MudEngine.DAL;
using MudEngine.Core;

namespace MudEngine.GameScripts
{
    public class BaseScript
    {
        [DefaultValue("Scripted Object")]
        public String Name { get; set; }

        public String ID { get; set; }

        public String Description { get; set; }

        public XMLData SaveData { get; protected set; }

        public StandardGame Game { get; private set; }

        public BaseScript(StandardGame game, String name, String description)
        {
            this.Name = name;
            this.Description = description;
            this.Game = game;

            this.ID = Guid.NewGuid().ToString();

            this.SaveData = new XMLData(this.GetType().Name);
        }

        public virtual Boolean Save(String filename)
        {
            return this.Save(filename, false);
        }

        public virtual Boolean Save(String filename, Boolean ignoreFileWrite)
        {
            if (File.Exists(filename))
                File.Delete(filename);

            try
            {
                this.SaveData = new XMLData(this.GetType().Name);
                this.SaveData.AddSaveData("Name", Name);
                this.SaveData.AddSaveData("ID", ID);
                this.SaveData.AddSaveData("Description", Description);

                if (!ignoreFileWrite)
                    this.SaveData.Save(filename);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public virtual void Load(String filename)
        {
            if (!File.Exists(filename))
                return;

            this.SaveData.Load(filename);

            try { this.Name = this.SaveData.GetData("Name"); }
            catch { this.LoadFailedMessage("Name"); }

            try { this.ID = this.SaveData.GetData("ID"); }
            catch { this.LoadFailedMessage("ID"); }

            try { this.Description = this.SaveData.GetData("Description"); }
            catch { this.LoadFailedMessage("Description"); }
        }

        public void LoadFailedMessage(String property)
        {
            StackTrace trace = new StackTrace();
            String callingType = trace.GetFrame(1).GetMethod().ReflectedType.Name;
            Logger.WriteLine("Load failed for " + callingType + "." + property);
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Name))
                return "{" + this.GetType().Name + "}:" + "Without Name";
            else
                return "{" + this.GetType().Name + "}:" + this.Name;
        }
    }
}
