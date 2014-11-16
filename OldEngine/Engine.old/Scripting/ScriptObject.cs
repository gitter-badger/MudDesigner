//-----------------------------------------------------------------------
// <copyright file="ScriptObject.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Reflection;
using System.Text;

namespace MudDesigner.Engine.Scripting
{
    /// <summary>
    /// Wraps an instance of any class into a Object with helper methods for invoking methods and accessing properties.
    /// </summary>
    public class ScriptObject
    {
        /// <summary>
        /// Gets or Sets the Object instance that this ScriptObject is wrapping.
        /// </summary>
        public Object Instance { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptObject"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public ScriptObject(Object instance)
        {
            Instance = instance ?? new Object();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ScriptObject"/> class.
        /// </summary>
        ~ScriptObject()
        {
            //TODO: Add ability to call a Shutdown() method within this Instance.
            Instance = null;
        }

        /// <summary>
        /// Sets the specified property for this objects Instance
        /// </summary>
        /// <param name="propertyName">The name of the property you want to change</param>
        /// <param name="propertyValue">The value for the property you want to change</param>
        /// <param name="indexArgs">Index arguments for collections.</param>
        public void SetProperty(String propertyName, object propertyValue, object[] indexArgs, bool overrideReadOnly = true)
        {
            PropertyInfo propertyInfo = Instance.GetType().GetProperty(propertyName);

            if (propertyValue != null && propertyInfo != null)
            {
                try
                {
                    if (propertyInfo.CanWrite || overrideReadOnly)
                        propertyInfo.SetValue(Instance, propertyValue, indexArgs);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        /// <summary>
        /// Gets a property for this objects Instance.
        /// </summary>
        /// <param name="propertyName">The property Name you want to get the value of.</param>
        /// <returns></returns>
        public object GetProperty(String propertyName)
        {
            String[] tokens = propertyName.Split('.');
            PropertyInfo previousProperty = Instance.GetType().GetProperty(tokens[0]);

            return previousProperty.GetValue(Instance, null);
        }

        /// <summary>
        /// Provides direct access to this objects Instance for propety modification
        /// </summary>
        /// <returns></returns>
        public dynamic GetProperty()
        {
            return Instance;
        }

        /// <summary>
        /// Gets the value of a field within this objects Instance
        /// </summary>
        /// <param name="propertyName">The field name</param>
        /// <returns></returns>
        public object GetField(String propertyName)
        {
            String[] tokens = propertyName.Split('.');
            FieldInfo previousField = Instance.GetType().GetField(tokens[0]);

            return previousField.GetValue(Instance);
        }

        /// <summary>
        /// Provides direct access to this object Instance for field modification
        /// </summary>
        /// <returns></returns>
        public dynamic GetField()
        {
            return Instance;
        }

        /// <summary>
        /// Invokes the specified method for this objects Instance
        /// </summary>
        /// <param name="methodName">The name of the method you want to call</param>
        /// <returns></returns>
        public Object InvokeMethod(String methodName)
        {
            return InvokeMethod(methodName, null);
        }

        /// <summary>
        /// Invokes the specified method for this objects Instance with optional parameters
        /// </summary>
        /// <param name="methodName">The name of the method you want to call</param>
        /// <param name="parameters">Arguments for the method</param>
        /// <returns></returns>
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
