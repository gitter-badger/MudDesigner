using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class ClearWeather : IWeatherState
    {
        public double OccurrenceProbability { get { return 80; } }

        public string Name { get { return "Clear"; } }
    }

    public class RainyWeather : IWeatherState
    {
        public double OccurrenceProbability { get { return 30; } }

        public string Name { get { return "Rainy"; } }
    }

    public class ThunderstormWeather : IWeatherState
    {
        public double OccurrenceProbability { get { return 15; } }

        public string Name { get { return "Thunderstorm"; } }
    }
}
