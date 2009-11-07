using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace MUDEngine
{
    [XmlInclude(typeof(StartingLocation))]
    public class ProjectInformation
    {

        public struct StartingLocation
        {
            public Environment.Room Room;
            public Environment.Zone Zone;
            public Environment.Realm Realm;
        }

        public struct CurrencyInfo
        {
            public uint Amount;
            public string Name;
            public string Description;
            public uint BaseValue;
        }

        public enum TimeOfDayOptions
        {
            AlwaysDay,
            AlwaysNight,
            Transition,
        }

        [Category("Company Information")]
        /// <summary>
        /// Gets or Sets the name of the company
        /// </summary>
        public string CompanyName { get; set; }

        [Category("Company Information")]
        /// <summary>
        /// Gets or Sets the companies website for this project
        /// </summary>
        public string Website { get; set; }

        [Category("Project Settings")]
        [Description("The name of the project.")]
        public string ProjectName { get; set; }

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

        [Category("Project Settings")]
        public TimeOfDayOptions TimeOfDay
        {
            get;
            set;
        }

        [Category("Project Settings")]
        [Description("Set how long in minutes it takes to transition from day to night.")]
        public int TimeOfDayTransition
        {
            get;
            set;
        }

        [Category("Project Settings")]
        [Description("Sets how long in minutes a day lasts in the game world.")]
        public int DayLength
        {
            get;
            set;
        }

        [Category("Project Information")]
        public string Version { get; set; }

        [Category("Project Information")]
        [Description("Sets the amount that the base currency is valued at.")]
        public uint BaseCurrencyAmount { get; set; }


        [Category("Project Information")]
        public string BaseCurrencyName { get; set; }



        //TODO: Add Party support.
        [Browsable(false)]
        public List<CurrencyInfo> CurrencyList { get; set; }

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
    }
}
