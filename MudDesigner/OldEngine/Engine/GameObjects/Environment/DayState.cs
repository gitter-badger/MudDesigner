//-----------------------------------------------------------------------
// <copyright file="DayState.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.GameObjects.Environment
{
    /// <summary>
    /// DayState allows a specific time of day to be given a state.
    /// </summary>
    public class DayState
    {
        /// <summary>
        /// Gets or sets the name of this state.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the time that this state takes affect in local (realm) time.
        /// </summary>
        public float StartTime { get; set; }
    }
}
