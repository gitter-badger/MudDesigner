using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.MyGame.Environment
{
    public class MyGameRoom : BaseRoom
    {
        public MyGameRoom()
        {
            Name = this.GetType().Name;
        }
    }
}
