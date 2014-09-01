using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Travel
{
    public class WestDirection : ITravelDirection
    {
        public string Direction
        {
            get 
            {
                return "West";
            }
        }

        public ITravelDirection GetOppositeDirection()
        {
            return new EastDirection();
        }
    }
}
