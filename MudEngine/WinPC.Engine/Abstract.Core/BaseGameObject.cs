using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
namespace MudDesigner.Engine.Abstract.Core
{
    public class BaseGameObject
    {
        Guid ID;

        public BaseGameObject(Guid id)
        {
            ID = id;
        }

        public BaseGameObject() : this(Guid.NewGuid())
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
