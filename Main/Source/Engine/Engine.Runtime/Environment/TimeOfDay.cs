//-----------------------------------------------------------------------
// <copyright file="TimeOfDay.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Runtime.Environment
{
    using Mud.Engine.Shared.Environment;
    using System;

    /// <summary>
    /// Provides a means of representing a specific time of day in hours and minutes.
    /// Methods are provided to adjust the time of day if needed.
    /// </summary>
    public class TimeOfDay : ITimeOfDay
    {
        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        public int Minute { get; set; }

        /// <summary>
        /// Gets or sets the hours per day.
        /// </summary>
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

        /// <summary>
        /// Decrements instance by the number of minutes specified.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        public void DecrementByMinute(int minutes)
        {
            if (this.Minute - minutes < 0)
            {
                // We can not reduce the number of minutes to less than 0, so we decrement an hour and restart from 59 minutes
                this.DecrementByHour(1);
                int deductedValue = Math.Abs(this.Minute - minutes);

                if (deductedValue > 0)
                {
                    this.Minute = 60;
                }
                else
                {
                    this.Minute = 59;
                }

                // Now that we have increased by an hour, lets continue to increment the minutes.
                this.DecrementByMinute(deductedValue);
            }
            else
            {
                this.Minute -= minutes;
            }
        }

        /// <summary>
        /// Decrements the instance by the number of hours specified.
        /// </summary>
        /// <param name="hours">The hour.</param>
        public void DecrementByHour(int hours)
        {
            // We can't decrement less than 0 hours. So we reset to the number of hours in a day.
            if (this.Hour - hours < 0)
            {
                int deductedValue = Math.Abs(this.Hour - hours);
                this.Hour = this.HoursPerDay;

                this.DecrementByHour(deductedValue);
            }
            else
            {
                this.Hour -= hours;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string hour = string.Empty;
            string minute = string.Empty;

            if (this.Hour < 10)
            {
                hour = string.Format("0{0}", this.Hour);
            }
            else
            {
                hour = this.Hour.ToString();
            }

            if (this.Minute < 10)
            {
                minute = string.Format("0{0}", this.Minute);
            }
            else
            {
                minute = this.Minute.ToString();
            }

            return string.Format("{0}:{1}", hour, minute);
        }
    }
}
