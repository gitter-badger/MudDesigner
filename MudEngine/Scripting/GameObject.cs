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
        public string Name { get; set; }

        /// <summary>
        /// Determins if this object will recieve Update/Draw calls from the ScriptEngine
        /// </summary>
        public bool IsActive { get; set; }

        public object CreateObject()
        {
            return Instance;
        }

        public bool DeleteObject()
        {
            return true;
        }

        public void SetProperty(string propertyName, object propertyValue)
        {
            PropertyInfo propertyInfo = Instance.GetType().GetProperty(propertyName);

            if (propertyValue is string)
            {
                if (propertyInfo.PropertyType.Name is string)
                {
                    propertyInfo.SetValue(Instance, propertyValue, null);
                }
            }
        }
        
        public object GetProperty(string propertyName)
        {
            string[] tokens = propertyName.Split('.');
            PropertyInfo previousProperty = Instance.GetType().GetProperty(tokens[0]);

            return previousProperty.GetValue(Instance, null);
        }
        
        public dynamic GetProperty()
        {
            return Instance;
        }
        
        public object InvokeMethod(string methodName, params object[] parameters)
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
