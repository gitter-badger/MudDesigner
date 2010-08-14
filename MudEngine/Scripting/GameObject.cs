using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Scripting;

namespace MudEngine.Scripting
{
    public class GameObject
    {
        /// <summary>
        /// The script instance for this game object
        /// </summary>
        public object Instance { get; set; }
        
        /// <summary>
        /// The Type name for this object
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Determins if this object will recieve Update/Draw calls from the ScriptEngine
        /// </summary>
        public Boolean IsActive { get; set; }

        public GameObject(object instance, String name)
        {
            Instance = instance;
            Name = name;
        }

        public object CreateObject()
        {
            return Instance;
        }

        public Boolean DeleteObject()
        {
            return true;
        }

        public void SetProperty(String propertyName, object propertyValue)
        {
            PropertyInfo propertyInfo = Instance.GetType().GetProperty(propertyName);

            if (propertyValue is String)
            {
                if (propertyInfo.PropertyType.Name is String)
                {
                    propertyInfo.SetValue(Instance, propertyValue, null);
                }
            }
        }
        
        public object GetProperty(String propertyName)
        {
            String[] tokens = propertyName.Split('.');
            PropertyInfo previousProperty = Instance.GetType().GetProperty(tokens[0]);

            return previousProperty.GetValue(Instance, null);
        }
        
        public dynamic GetProperty()
        {
            return Instance;
        }
        
        public object InvokeMethod(String methodName, params object[] parameters)
        {
            MethodInfo method = Instance.GetType().GetMethod(methodName);

            try
            {
                if (parameters == null || parameters.Length == 0)
                    return method.Invoke(Instance, null);
                else
                    return method.Invoke(Instance, parameters);
            }
            catch
            {
                throw;
            }
        }
    }
}
