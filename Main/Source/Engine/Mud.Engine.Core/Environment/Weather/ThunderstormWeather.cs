using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Weather
{
    public class ThunderstormWeather : IWeatherState
    {
        public double OccurrenceProbability
        {
            get
            {
                return 15;
            }
        }

        public string Name
        {
            get
            {
                return "Thunderstorm";
            }
        }
    }
}
