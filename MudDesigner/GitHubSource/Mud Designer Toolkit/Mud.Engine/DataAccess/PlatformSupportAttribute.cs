// Microsoft .NET Framework
using System;

namespace Mud.DataAccess
{
    /// <summary>
    /// The PlatformSupport attribute can be used to specify what platform a specific class is officially supported on.
    /// For example: Limit a IDataContext storage container to a specific platform due to container limitations. SQLite on a tablet or SQLServer on Windows.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PlatformSupportAttribute : Attribute
    {
        /// <summary>
        /// Gets or Sets the name of the OS. This is just for information; it should not be used for OS support checks.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Major version of the OS that this attribute is targeted for.
        /// </summary>
        public int MajorVersion { get; set; }

        /// <summary>
        /// Gets or Sets the minor version of the OS that this attribute is targeted for.
        /// </summary>
        public int MinorVersion { get; set; }

        /// <summary>
        /// Constructs the attribute with information meant to specify which OS the class is being targeted for.
        /// </summary>
        /// <param name="name">Gets or Sets the name of the OS. This is just for information; it should not be used for OS support checks.</param>
        /// <param name="majorVersion">Gets or Sets the Major version of the OS that this attribute is targeted for.</param>
        /// <param name="minorVersion"> Gets or Sets the minor version of the OS that this attribute is targeted for.</param>
        public PlatformSupportAttribute(string name, int majorVersion, int minorVersion)
        {
            this.Name = name;
            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;
        }

    }
}
