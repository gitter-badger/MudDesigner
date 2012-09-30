using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MudDesigner.Engine.Abstract.Objects
{
    public abstract class BaseGameObject
    {

        /// <summary>
        /// Gets a reference to the ID of this room.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Returns a string value of the Type that the object belongs for this room.
        /// </summary>
        public string Type
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Gets or Sets the name associated with this object.
        /// </summary>
        public string Name { get; set; }

        public BaseGameObject(string name, Guid id)
        {
            Id = id;
            Name = name;
        }

        public BaseGameObject() : this(string.Empty, Guid.NewGuid())
        {

            //Realms = new Dictionary<string, IRealm>();
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                Type t = property.PropertyType;

                if (!property.CanWrite)
                    continue;

                if (property.PropertyType == typeof(string))
                    property.SetValue(this, string.Empty, null);

                else if (property.GetValue(this, null) == null)
                    property.SetValue(this, Activator.CreateInstance(property.PropertyType), null);
            }
        }
    }
}
