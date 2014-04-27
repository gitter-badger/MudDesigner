using System;
using System.ComponentModel;
using System.Reflection;

using Mud.Commands;

namespace Mud.Core
{
    /// <summary>
    /// Various extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Compares a supplied IMudCommand object with a string command for equality.
        /// </summary>
        /// <param name="command">The command object.</param>
        /// <param name="com">The literal command string..</param>
        /// <returns>Returns true if the command object is a command corresponding to the command string.</returns>
        public static bool Equals(this IMudCommand command, string com)
        {
            Type commandType = command.GetType();

            // Check if we have the ShorthandName attribute or DisplayName attributes
            if (Attribute.IsDefined(commandType, typeof(ShorthandNameAttribute), true))
            {
                // Compare the full command along with the short hand command against the string provided.
                ShorthandNameAttribute attribute = commandType.GetCustomAttribute<ShorthandNameAttribute>();
                if (attribute.Command == com)
                {
                    return true;
                }
                else if (attribute.Shorthand == com)
                {
                    return true;
                }
                else
                {
                    // No matches, return false.
                    return false;
                }
            }
            else if (Attribute.IsDefined(commandType, typeof(DisplayNameAttribute), true))
            {
                // Compare the display name of the command against the string provided.
                DisplayNameAttribute attribute = commandType.GetCustomAttribute<DisplayNameAttribute>();
                if (attribute.DisplayName == com)
                {
                    return true;
                }
                else
                {
                    // No match, return false.
                    return false;
                }
            }
            else
            {
                // No match, return false.
                return false;
            }
        }
    }
}
