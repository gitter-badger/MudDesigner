﻿//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameManagement;

namespace MudEngine.GameObjects
{
    public class BaseObject
    {
        [Category("Object Setup")]
        [Description("The Display Name assigned to this object.")]
        //Required to refresh Filename property in the editors propertygrid
        [RefreshProperties(RefreshProperties.All)]
        public string Name { get; set; }

        public Int32 ID { get; internal set; }

        [Category("Object Setup")]
        [Description("A brief description of this object. The description is displayed to users when they use a command for investigating an object")]
        public string Description { get; set; }

        /// <summary>
        /// A detailed description that treats each entry as a seperete line when outputted to the player
        /// </summary>
        public List<string> DetailedDescription { get; set; }

        [Category("Object Setup")]
        [ReadOnly(true)]
        [Description("The filename of the current object. This is assigned by the engine and not editable.")]
        public string Filename
        {
            //Returns the name of the object + the objects Type as it's extension.
            //Filenames are generated by the class itself, users can not assign it.
            get;
            set;
        }

        [Category("Environment Information")]
        [Description("If a user asks to use his/her senses to investigate an area, this is one of the results that will be displayed. Senses can be used to assist blind characters.")]
        [DefaultValue("You don't smell anything unsual.")]
        public string Smell { get; set; }

        [Category("Environment Information")]
        [Description("If a user asks to use his/her senses to investigate an area, this is one of the results that will be displayed. Senses can be used to assist blind characters.")]
        [DefaultValue("You hear nothing of interest.")]
        public string Listen { get; set; }

        [Category("Environment Information")]
        [Description("If a user asks to use his/her senses to investigate an area, this is one of the results that will be displayed. Senses can be used to assist blind characters.")]
        [DefaultValue("You feel nothing.")]
        public string Feel { get; set; }

        public Game ActiveGame { get; set; }

        /// <summary>
        /// Initializes the base object
        /// </summary>
        public BaseObject(Game game)
        {
            Name = "New " + this.GetType().Name;
            ActiveGame = game;
            DetailedDescription = new List<string>();

            this.Feel = "You feel nothing.";
            this.Listen = "You hear nothing of interest.";
            this.Smell = "You don't smell anything unsual.";
            this.Name = DefaultName();

            this.Filename = DefaultName() + "." + this.GetType().Name;

            //This must be called on instancing of the object. 
            //It is unique and will be used for saving the object.
            //Allows for multiple objects with the same Name property
            //but different Identifiers. Letting there be multiple versions
            //of the same object.

            ActiveGame.AddObject(this);
        }


        ~BaseObject()
        {
            //We must free up this ID so that it can be used by other objects being instanced.
            ActiveGame.RemoveObject(this);
        }

        private bool ShouldSerializeName()
        {
            return this.Name != DefaultName();
        }

        private void ResetName()
        {
            this.Name = DefaultName();
        }

        private string DefaultName()
        {
            return "New " + this.GetType().Name;
        }

        #region Public Methods
        public override string ToString()
        {
            return this.Name;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnCreate()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnEquip()
        {
        }

        public virtual void OnUnequip()
        {
        }

        public virtual void OnMount()
        {
        }

        public virtual void OnDismount()
        {
        }

        public virtual void Save(string filename)
        {
            string path = Path.GetDirectoryName(filename);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (File.Exists(filename))
                File.Delete(filename);

            FileManager.WriteLine(filename, this.Name, "Name");
            FileManager.WriteLine(filename, this.ID.ToString(), "Identifier");
            FileManager.WriteLine(filename, this.Description, "Description");
            FileManager.WriteLine(filename, this.Feel, "Feel");
            FileManager.WriteLine(filename, this.Listen, "Listen");
            FileManager.WriteLine(filename, this.Smell, "Smell");
        }

        public virtual void Load(string filename)
        {
            this.Name = FileManager.GetData(filename, "Name");
            this.Description = FileManager.GetData(filename, "Description");
            this.Feel = FileManager.GetData(filename, "Feel");
            this.Listen = FileManager.GetData(filename, "Listen");
            this.Smell = FileManager.GetData(filename, "Smell");
        }
        #endregion
    }
}
