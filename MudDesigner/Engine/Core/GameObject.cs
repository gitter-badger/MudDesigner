using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

using MudDesigner.Engine.Core;
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
            if (copyTo is GameObject)
            {
                GameObject obj = (GameObject)copyTo;
                obj.Name = Name;
                obj.Description = Description;

                PropertyInfo info = obj.GetType().GetProperty("Destroyed");

                if (info != null)
                    info.SetValue(obj, bool.Parse(Destroyed.ToString()), null);
            }
        }
    }
}
