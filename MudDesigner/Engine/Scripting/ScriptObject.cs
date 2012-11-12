using System;
using System.Reflection;
using System.Text;

namespace MudDesigner.Engine.Scripting
{
    public class ScriptObject
    {
        public Object Instance { get; set; }

        public ScriptObject(Object instance)
        {
            Instance = instance ?? new Object();
        }

        ~ScriptObject()
        {
            //TODO: Add ability to call a Shutdown() method within this Instance.
            Instance = null;
        }

        public void SetProperty(String propertyName, object propertyValue, object[] indexArgs)
        {
            PropertyInfo propertyInfo = Instance.GetType().GetProperty(propertyName);

            if (propertyValue != null && propertyInfo != null)
            {
                propertyInfo.SetValue(Instance, propertyValue, indexArgs);
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

        public object GetField(String propertyName)
        {
            String[] tokens = propertyName.Split('.');
            FieldInfo previousField = Instance.GetType().GetField(tokens[0]);

            return previousField.GetValue(Instance);
        }

        public dynamic GetField()
        {
            return Instance;
        }

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
                var sb = new StringBuilder();
                sb.Append("Error invoking method. Does the method exist?");
                return sb.ToString();
            }
        }
    }
}
