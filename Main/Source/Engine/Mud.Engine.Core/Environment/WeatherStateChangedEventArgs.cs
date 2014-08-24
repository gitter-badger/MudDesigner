using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class WeatherStateChangedEventArgs : EventArgs
    {
        public WeatherStateChangedEventArgs(IWeatherState previousState, IWeatherState newState)
        {
            this.PreviousState = previousState;
            this.CurrentState = newState;
        }

        public IWeatherState PreviousState { get; private set; }

        public IWeatherState CurrentState {get; private set;}
    }
}
