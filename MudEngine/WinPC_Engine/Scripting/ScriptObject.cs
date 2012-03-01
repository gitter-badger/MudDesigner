using System;
using System.Reflection;
using System.Text;

namespace sEngine.Scripting
{
    public class ScriptObject
    {
        public Object Instance { get; set; }

        public ScriptObject(Object instance)
        {
            if (instance == null)
                Instance = new Object();
            else
                Instance = instance;
        }

        ~ScriptObject()
        {
            //TODO: Add ability to call a Shutdown() method within this Instance.
            Instance = null;
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

#if WINDOWS_PC
        public dynamic GetProperty()
        {
            return Instance;
        }
#endif

        public object GetField(String propertyName)
        {
            String[] tokens = propertyName.Split('.');
            FieldInfo previousField = Instance.GetType().GetField(tokens[0]);

            return previousField.GetValue(Instance);
        }

#if WINDOWS_PC
        public dynamic GetField()
        {
            return Instance;
        }
#endif

        public Object InvokeMethod(String methodName)
        {
            return InvokeMethod(methodName, null);
        }

        public Object InvokeMethod(String methodName, params Object[] parameters)
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
                StringBuilder sb = new StringBuilder();
                sb.Append("Error invoking method. Does the method exist?");
                return sb.ToString();
            }
        }
    }
}
