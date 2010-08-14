using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.GameManagement
{
    public class GameTime
    {
        public struct Time
        {
            public Int32 Year { get; set; }
            public Int32 Month { get; set; }
            public Int32 Day { get; set; }
            public Int32 Hour { get; set; }
            public Int32 Minute { get; set; }
            public Int32 Second { get; set; }
            private GameTime gameTime;
        }
        
        public enum TimeOfDayOptions
        {
            AlwaysDay,
            AlwaysNight,
            Transition,
        }

        internal Game ActiveGame { get; private set; }

        /// <summary>
        /// The time of day that the server actually started up.
        /// </summary>
        internal DateTime ServerStartTime { get; private set; }

        /// <summary>
        /// Gets the current World Time.
        /// </summary>
        public Time CurrentWorldTime { get; internal set; }

        /// <summary>
        /// Gets or Sets the current Time of the System
        /// </summary>
        private DateTime CurrentTime { get; set; }

        /// <summary>
        /// Gets or Sets how many Hours it takes to make a full day in the World
        /// </summary>
        public Int32 HoursPerDay { get; set; }

        /// <summary>
        /// Gets or Sets how many minutes it takes to make a full Hour
        /// </summary>
        public Int32 MinutesPerHour { get; set; }

        /// <summary>
        /// Gets or Sets how many seconds it takes to make a full minute
        /// </summary>
        public Int32 SecondsPerMinute { get; set; }

        /// <summary>
        /// Gets or Sets how many Days it takes to make a full month in the world
        /// </summary>
        public Int32 DaysPerMonth { get; set; }

        /// <summary>
        /// Gets or Sets how many Months it takes to make a full Year in the world
        /// </summary>
        public Int32 MonthsPerYear { get; set; }

        /// <summary>
        /// Gets or Sets the name of each Day in a Week.
        /// </summary>
        public List<String> DayNames { get; set; }

        /// <summary>
        /// Gets or Sets the name of each Month in a Year.
        /// </summary>
        public List<String> MonthNames { get; set; }

        /// <summary>
        /// Gets or Sets what time of day the world is currently in.
        /// </summary>
        public TimeOfDayOptions DayTransitions { get; set; }

        /// <summary>
        /// Gets or Sets what time of day that it begins to transition to night.
        /// </summary>
        public Int32 DawnTime { get; set; }

        /// <summary>
        /// /Gets or Sets what time of day that it begins to transition into day time.
        /// </summary>
        public Int32 SunriseTime { get; set; }

        /// <summary>
        /// Gets or Sets the initial Time that the world starts in.
        /// </summary>
        public Time InitialGameTime { get; set; }

        public GameTime(Game game)
        {
            ActiveGame = game;

            ServerStartTime = DateTime.Now;

            DayNames = new List<String>();
            MonthNames = new List<String>();

            DayNames.Add("Monday");
            DayNames.Add("Tuesday");
            DayNames.Add("Wednesday");
            DayNames.Add("Thursday");
            DayNames.Add("Friday");
            DayNames.Add("Saturday");
            DayNames.Add("Sunday");

            MonthNames.Add("January");
            MonthNames.Add("February");
            MonthNames.Add("March");
            MonthNames.Add("April");
            MonthNames.Add("May");
            MonthNames.Add("June");
            MonthNames.Add("July");
            MonthNames.Add("August");
            MonthNames.Add("September");
            MonthNames.Add("October");
            MonthNames.Add("November");
            MonthNames.Add("December");
        }

        public void Initialize()
        {
            Time t = InitialGameTime;
            CurrentWorldTime = t;
        }

        public virtual void Update()
        {
            TimeSpan ts = CurrentTime - DateTime.Now;

            //If the seconds that has passed inbetween the last Update call is greater than 0
            //Then we need to increment a Second, which will start a domino effect if it needs to
            //in order to increment minute/hours/days/months and years.
            if (ts.Seconds != 0)
            {
                IncrementSecond();
            }

            CurrentTime = DateTime.Now;

        }

        public void IncrementSecond()
        {
            Time t = new Time();
            t = CurrentWorldTime;

            if (CurrentWorldTime.Second == SecondsPerMinute)
            {
                t.Second = 0;
                IncrementMinute();
            }
            else
                t.Second++;

            CurrentWorldTime = t;
        }

        public void IncrementMinute()
        {
            Time t = new Time();
            t = CurrentWorldTime;

            if (CurrentWorldTime.Minute == MinutesPerHour)
            {
                t.Minute = 0;
                IncrementHour();
            }
            else
                t.Minute++;

            CurrentWorldTime = t;
        }

        public void IncrementHour()
        {
            Time t = new Time();
            t = CurrentWorldTime;

            if (CurrentWorldTime.Hour == HoursPerDay)
            {
                t.Hour = 0;
                IncrementDay();
            }
            else
                t.Hour++;

            CurrentWorldTime = t;
        }

        public void IncrementDay()
        {
            Time t = new Time();
            t = CurrentWorldTime;

            if (CurrentWorldTime.Day == DaysPerMonth)
            {
                t.Day = 1;
                IncrementMonth();
            }
            else
                t.Day++;

            CurrentWorldTime = t;
        }

        public void IncrementMonth()
        {
            Time t = new Time();
            t = CurrentWorldTime;

            if (CurrentWorldTime.Month == MonthsPerYear)
            {
                t.Month = 1;
                IncrementYear();
            }
            else
                t.Month++;

            CurrentWorldTime = t;
        }

        public void IncrementYear()
        {
            Time t = new Time();
            t = CurrentWorldTime;

            t.Year++;

            CurrentWorldTime = t;
        }

        public String GetCurrentWorldTime()
        {
            if (DayNames.Count < CurrentWorldTime.Day)
            {
                return "Not enough Day Names specified to match up with DaysPerMonth property.";
            }
            else if (MonthNames.Count < CurrentWorldTime.Month)
            {
                return "Not enough Month names specified to match up with MonthsPerYear property.";
            }

            String day = DayNames[CurrentWorldTime.Day - 1];
            String month = MonthNames[CurrentWorldTime.Month - 1];

            return day + ", " + month + " " + CurrentWorldTime.Day + ", " + CurrentWorldTime.Year + ": " + CurrentWorldTime.Hour + ":" + CurrentWorldTime.Minute + ":" + CurrentWorldTime.Second;
        }
    }
}
