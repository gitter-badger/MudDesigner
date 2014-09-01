using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Weather
{
    public class ClearWeather : IWeatherState
    {
        public double OccurrenceProbability 
        { 
            get 
            {
                return 80; 
            } 
        }

        public string Name 
        { 
            get 
            { 
                return "Clear"; 
            } 
        }
    }
}
