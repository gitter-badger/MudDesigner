using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Scripting;
using Newtonsoft.Json;
namespace MudDesigner.Engine.Core
{
//    [JsonObject(IsReference = true)]
    public class GameObject : IGameObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public bool Permanent { get; set; }

        [Browsable(false)]
        public bool Destroyed { get; protected set; }

        public GameObject() 
        { 
            Enabled = true; 
        }

        public virtual void Destroy()
        {
            Destroyed = true;
            Enabled = false;
        }

        public virtual void CopyState(ref dynamic copyTo)
        {
            //Make sure we are dealing with an object that inherits from GameObject
            if (copyTo is IGameObject)
            {
                //Wrap the object in a ScriptObject for easy managing
                ScriptObject newObject = new ScriptObject(copyTo);

                //Set each of the new objects properties to match this object.
                newObject.SetProperty("Name", Name, null);
                newObject.SetProperty("Description", Description, null);
                newObject.SetProperty("Destroyed", Destroyed, null);
                newObject.SetProperty("Enabled", Enabled, null);
                newObject.SetProperty("Permanent", Permanent, null);
            }
        }
    }
}
