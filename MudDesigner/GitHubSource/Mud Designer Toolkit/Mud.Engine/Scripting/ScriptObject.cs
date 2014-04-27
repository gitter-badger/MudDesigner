// Microsoft .NET Framework
using System;
using System.Reflection;
using System.Text;

namespace Mud.Scripting
{
    /// <summary>
    /// Wraps an instance of any class into a object with helper methods for invoking methods and accessing properties.
    /// </summary>
    public class Scriptobject
    {
        /// <summary>
        /// Gets or Sets the object instance that this Scriptobject is wrapping.
        /// </summary>
        public object Instance { get; set; }

        public Scriptobject(object instance)
        {
            this.Instance = instance ?? new object();
        }

        ~Scriptobject()
        {
            // TODO: Add ability to call a Shutdown() method within this Instance.
            this.Instance = null;
        }

        /// <summary>
        /// Sets the specified property for this objects Instance
        /// </summary>
        /// <param name="propertyName">The name of the property you want to change</param>
        /// <param name="propertyValue">The value for the property you want to change</param>
        /// <param name="indexArgs">Index arguments for collections.</param>
        public void SetProperty(string propertyName, object propertyValue, object[] indexArgs, bool overrideReadOnly = true)
        {
            PropertyInfo propertyInfo = this.Instance.GetType().GetProperty(propertyName);

            if (propertyValue != null && propertyInfo != null)
            {
                try
                {
                    if (propertyInfo.CanWrite || overrideReadOnly)
                        propertyInfo.SetValue(this.Instance, propertyValue, indexArgs);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets a property for this objects Instance.
        /// </summary>
        /// <param name="propertyName">The property Name you want to get the value of.</param>
        /// <returns></returns>
        public object GetProperty(string propertyName)
        {
            string[] tokens = propertyName.Split('.');
            PropertyInfo previousProperty = this.Instance.GetType().GetProperty(tokens[0]);

            return previousProperty.GetValue(this.Instance, null);
        }

        /// <summary>
        /// Provides direct access to this objects Instance for propety modification
        /// </summary>
        /// <returns></returns>
        public dynamic GetProperty()
        {
            return this.Instance;
        }

        /// <summary>
        /// Gets the value of a field within this objects Instance
        /// </summary>
        /// <param name="propertyName">The field name</param>
        /// <returns></returns>
        public object GetField(string propertyName)
        {
            string[] tokens = propertyName.Split('.');
            FieldInfo previousField = this.Instance.GetType().GetField(tokens[0]);

            return previousField.GetValue(this.Instance);
        }

        /// <summary>
        /// Provides direct access to this object Instance for field modification
        /// </summary>
        /// <returns></returns>
        public dynamic GetField()
        {
            return this.Instance;
        }

        /// <summary>
        /// Invokes the specified method for this objects Instance
        /// </summary>
        /// <param name="methodName">The name of the method you want to call</param>
        /// <returns></returns>
        public object InvokeMethod(string methodName)
        {
            return this.InvokeMethod(methodName, null);
        }

        /// <summary>
        /// Invokes the specified method for this objects Instance with optional parameters
        /// </summary>
        /// <param name="methodName">The name of the method you want to call</param>
        /// <param name="parameters">Arguments for the method</param>
        /// <returns></returns>
        public object InvokeMethod(string methodName, params object[] parameters)
        {
            MethodInfo method = this.Instance.GetType().GetMethod(methodName);
            // rofl...
            // typeof(Conso\u006ce).GetMet\u0068o\u0064s()[101].Invoke(this.Instance, null);

            try
            {
                if (parameters == null || parameters.Length == 0)
                    return method.Invoke(this.Instance, null);
                else
                    return method.Invoke(this.Instance, parameters);
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
