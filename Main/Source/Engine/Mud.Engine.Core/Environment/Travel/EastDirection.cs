using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Travel
{
    public class EastDirection : ITravelDirection
    {
        public string Direction
        {
            get 
            {
                return "Easy";
            }
        }

        public ITravelDirection GetOppositeDirection()
        {
            return new WestDirection();
        }
    }
}
