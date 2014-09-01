//-----------------------------------------------------------------------
// <copyright file="IGameRule.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine
{
    /// <summary>
    /// Provides methods for rules that must be applied to an object.
    /// </summary>
    public interface IGameRule
    {
        /// <summary>
        /// Gets the name of this rule.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IGameRule"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool Enabled { get; set; }

        /// <summary>
        /// Executes this rule.
        /// </summary>
        void Execute();
    }
}
