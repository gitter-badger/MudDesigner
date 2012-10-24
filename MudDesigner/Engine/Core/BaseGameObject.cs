using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Core
{
    public abstract class BaseGameObject : IGameObject
    {

        /// <summary>
        /// Gets a reference to the ID of this room.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or Sets the name associated with this object.
        /// </summary>
        public string Name { get; set; }

        public BaseGameObject(string name) : this(name, Guid.NewGuid())
        {
            //Stub
        }

        public BaseGameObject(string name, Guid id)
        {
            Id = id;
            Name = name;

            //Realms = new Dictionary<string, IRealm>();
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                Type t = property.PropertyType;

                if (!property.CanWrite)
                    continue;

                if (property.GetType().IsInterface)
                    continue;

                //Wrap in a Try{} in the event that SetValue fails with special Types
                try
                {
                    if (property.PropertyType == typeof(string))
                        property.SetValue(this, string.Empty, null);

                    else if (property.GetValue(this, null) == null)
                        property.SetValue(this, Activator.CreateInstance(property.PropertyType), null);
                }
                catch
                {
                    //Swallow it.
                }
            }
        }

        public BaseGameObject() : this(string.Empty, Guid.NewGuid())
        {
            //Stub
        }

        public void Save(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
