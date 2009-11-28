using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Xml;

//MudEngine
using MUDEngine.Objects;
using MUDEngine.Objects.Environment;
using MUDEngine.FileSystem;

namespace MUDEngine
{
    [XmlInclude(typeof(StartingLocation))]
    [XmlInclude(typeof(Currency))]
    public class ProjectInformation
    {
        public enum TimeOfDayOptions
        {
            AlwaysDay,
            AlwaysNight,
            Transition,
        }

        [Category("Company Settings")]
        /// <summary>
        /// Gets or Sets the name of the company
        /// </summary>
        public string CompanyName { get; set; }

        [Category("Company Settings")]
        /// <summary>
        /// Gets or Sets the companies website for this project
        /// </summary>
        public string Website { get; set; }

        [Category("Project Settings")]
        [Description("The name of the game.")]
        public string GameTitle { get; set; }

        [Category("Project Settings")]
        /// <summary>
        /// Gets or Sets if the game autosaves when the player changes locations.
        /// </summary>
        public bool AutoSave { get; set; }

        [Category("Project Settings")]
        /// <summary>
        /// Gets or Sets if room names are hidden during console output.
        /// </summary>
        public bool HideRoomNames { get; set; }

        [Category("Day Management")]
        public TimeOfDayOptions TimeOfDay
        {
            get;
            set;
        }

        [Category("Day Management")]
        [Description("Set how long in minutes it takes to transition from day to night.")]
        public int TimeOfDayTransition
        {
            get;
            set;
        }

        [Category("Day Management")]
        [Description("Sets how long in minutes a day lasts in the game world.")]
        public int DayLength
        {
            get;
            set;
        }

        [Category("Project Settings")]
        public string Version { get; set; }

        [Category("Game Currency")]
        [Description("Sets the amount that the base currency is valued at.")]
        public uint BaseCurrencyAmount { get; set; }


        [Category("Game Currency")]
        public string BaseCurrencyName { get; set; }



        //TODO: Add Party support.
        [Browsable(false)]
        public List<Currency> CurrencyList { get; set; }

        [Browsable(false)]
        public string ProjectPath { get; set; }

        [Browsable(false)]
        public StartingLocation InitialLocation
        {
            get;
            set;
        }

        [Browsable(false)]
        public string Story
        {
            get;
            set;
        }

        public ProjectInformation()
        {
            CurrencyList = new List<Currency>();
        }
    }
}
