//-----------------------------------------------------------------------
// <copyright file="PlatformSupportAttribute.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// The PlatformSupport attribute can be used to specify what platform a specific class is officially supported on.
    /// For example: Limit an IPersistedStorage container to a specific platform due to container limitations. 
    /// SQLite on a tablet or SQLServer on Windows.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PlatformSupportAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSupportAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        public PlatformSupportAttribute(string name, int majorVersion, int minorVersion)
        {
            this.Name = name;
            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;
        }

        /// <summary>
        /// Gets or sets the name of the OS. This is just for information; it should not be used for OS support checks.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Major version of the OS that this attribute is targeted for.
        /// </summary>
        public int MajorVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor version of the OS that this attribute is targeted for.
        /// </summary>
        public int MinorVersion { get; set; }
    }
}
