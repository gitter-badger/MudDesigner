//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;

//MUD Engine
using MudEngine.FileSystem;

namespace MudEngine.Scripting
{
    public class GameObjectCollection
    {
        internal List<GameObject> _GameObjects;
 
        public GameObjectCollection()
        {
            _GameObjects = new List<GameObject>();
        }
    }
}
