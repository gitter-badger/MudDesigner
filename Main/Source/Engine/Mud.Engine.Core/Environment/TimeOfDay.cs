using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class TimeOfDay
    {
        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>
        /// The hour.
        /// </value>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>
        /// The minute.
        /// </value>
        public int Minute { get; set; }

        public int HoursPerDay { get; set; }

        /// <summary>
        /// Increments instance by the number of minutes specified.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        public void IncrementByMinute(int minutes)
        {
            if (this.Minute + minutes > 59)
            {
                // We have to many minutes provided, so we must increase by an hour
                this.IncrementByHour(1);
                int deductedValue = Math.Abs(this.Minute - 59);
                this.Minute = 0;

                // Now that we have increased by an hour, lets continue to increment the minutes.
                if (deductedValue > 0)
                {
                    this.IncrementByMinute(minutes - deductedValue);
                }
            }
            else
            {
                this.Minute += minutes;
            }
        }

        /// <summary>
        /// Increments the instance by the number of hours specified.
        /// </summary>
        /// <param name="hours">The hour.</param>
        public void IncrementByHour(int hours)
        {
            // We can't increment more than 23 hours. Next hour is rolled over to 0.
            if (this.Hour + hours > this.HoursPerDay)
            {
                int deductedValue = Math.Abs(this.Hour - this.HoursPerDay);
                this.Hour = 0;

                if (deductedValue > 0)
                {
                    this.IncrementByHour(hours - deductedValue);
                }
            }
            else
            {
                this.Hour += hours;
            }
        }
    }
}
