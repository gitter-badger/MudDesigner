using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Travel
{
    public class SouthDirection : ITravelDirection
    {
        public string Direction 
        {
            get
            {
                return "South";
            }
        }

        public ITravelDirection GetOppositeDirection()
        {
            return new NorthDirection();
        }
    }
}
