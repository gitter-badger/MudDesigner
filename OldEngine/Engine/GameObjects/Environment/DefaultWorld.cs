//-----------------------------------------------------------------------
// <copyright file="DefaultWorld.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.GameObjects.Environment
{
    /// <summary>
    /// The default game world.
    /// </summary>
    public class DefaultWorld : IWorld
    {
        public int Id { get; set; }

        public List<IMob> Occupants { get; protected set; }

        /// <summary>
        /// Occurs after the World is loaded.
        /// </summary>
        public event EventHandler<WorldEventArgs> Loaded;

        /// <summary>
        /// Occurs after the weather changes.
        /// </summary>
        public event EventHandler<WorldEventArgs> WeatherChanged;

        /// <summary>
        /// Occurs after the day state has changed.
        /// </summary>
        public event EventHandler<WorldEventArgs> DayStateChanged;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of hours since original creation that this world has been alive.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public int HoursAlive
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the in-game hours per in-game day.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public int HoursPerDay
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the ratio that hours are compared to real-world hours. If a ratio is set to 4, then 1 in-game hour is equal to 4 real-world hours.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public float HourRatio
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the state of the current weather.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public WeatherState CurrentWeatherState
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets a collection of weather states that can be used at any time.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public List<WeatherState> WeatherStates { get; set; }

        /// <summary>
        /// Gets or sets the state of the current day.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public DayState CurrentDayState
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets a collection of day states will be used to provide feedback on the current state of the day.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public List<DayState> DayStates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is safe.
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public bool IsSafe
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Loaded" /> event.
        /// </summary>
        /// <param name="args">The <see cref="WorldEventArgs"/> instance containing the event data.</param>
        protected virtual void OnLoaded(WorldEventArgs args)
        {
            EventHandler<WorldEventArgs> handler = Loaded;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            // TODO: Restore from data access layer.
            this.DayStates = new List<DayState>();
            this.WeatherStates = new List<WeatherState>();
            this.Occupants = new List<IMob>();

            this.OnLoaded(new WorldEventArgs(this));
        }

        /// <summary>
        /// Updates this instance of the World.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
