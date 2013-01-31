/* GameObject
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: This is the base class for all Game Objects in the engine.
 */

//Microsoft .NET using statements
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Scripting;

//Newtonsoft JSon using statement
using Newtonsoft.Json;
using log4net;

namespace MudDesigner.Engine.Core
{
    //This is the base class for all Game Objects in the engine.
    public class GameObject : IGameObject
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(IGameObject));

        /// <summary>
        /// Gets or Sets the Name for this object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description for this object
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets if this object is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or Sets if this object is permanent. If true, it can never be deleted from the world
        /// </summary>
        public bool Permanent { get; set; }

        /// <summary>
        /// Gets or Sets the unique ID for this object.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets if this object has been destroyed and should no longer be used.
        /// </summary>
        [Browsable(false)]
        public bool Destroyed { get; protected set; }

        public GameObject() 
        { 
            Enabled = true;
            ID = new Guid();
        }

        /// <summary>
        /// Destroys this object, preventing it from being used anymore
        /// </summary>
        public virtual void Destroy()
        {
            Destroyed = true;
            Enabled = false;
        }

        /// <summary>
        /// Copies the current values of this object to a new object
        /// </summary>
        /// <param name="copyTo">The object that should have it's properties overwritten with the values of the calling Object</param>
        public virtual void CopyState(ref IGameObject copyFrom, bool ignoreNonNullProperties = false)
        {
            PropertyInfo[] properties = copyFrom.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (PropertyInfo prop in properties)
            {
                PropertyInfo info = this.GetType().GetProperty(prop.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                //Check if this property has State Copy disabled via the custom attribute, if so, don't copy this property.
                Attribute[] attrib = (Attribute[])info.GetCustomAttributes(typeof(DisableStateCopyAttribute), true);
                if (attrib.Length > 0)
                    continue;

                //If info is not null and we can write it without an exception
                if (info != null && info.CanWrite)
                {
                    //Check if this.Property already has a value.
                    //if it does, then check if the user wants to override it or not.
                    if (info.GetValue(this, null) != null && !ignoreNonNullProperties)
                        info.SetValue(this, prop.GetValue(copyFrom, null), null);
                    //If it does not have a value, then we set it regardless.
                    else if (info.GetValue(this, null) == null)
                        info.SetValue(this, prop.GetValue(copyFrom, null), null);
                }
                else
                {
                    Log.Error(string.Format("Failed to get property {0} from object {1} within the {2}.CopyState method", prop.Name, copyFrom.Name, this.Name));
                }
            }
        }
    }
}
