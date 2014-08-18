using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Environment
{
    public class DefaultWorld : IWorld
    {
        public DefaultWorld()
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public double TimeFromCreation 
        { 
            get 
            { 
                return this.CreationDate.Subtract(DateTime.Now).TotalSeconds; 
            } 
        }

        public DateTime CreationDate { get; protected set; }

        public int HoursPerDay { get; set; }

        public float HoursRatio { get; set; }

        public IEnumerable<IRealm> Realms { get; set; }

        public string Name { get; set; }

        public IWeatherState CurrentWeather { get; set; }

        public IEnumerable<IWeatherState> WeatherStates { get; set; }

        public ITimeOfDayState CurrentTimeOfDay { get; set; }

        public IEnumerable<ITimeOfDayState> TimeOfDayStates { get; set; }

        public void Initialize()
        {
            this.TimeOfDayStates = new List<ITimeOfDayState>();
            this.WeatherStates = new List<IWeatherState>();
            this.Realms = new List<IRealm>();
        }
    }
}
