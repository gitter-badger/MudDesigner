using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.FileSystem
{
        public struct SaveDataPaths
        {
            public string Players 
            {
                get
                {
                    return _Players;
                }
                set
                {
                    _Players = value;
                }
            }

            public string Environment
            {
                get
                {
                    return _Environment;
                }
                set
                {
                    _Environment = value;
                }
            }
            private string _Players;
            private string _Environment;

            public SaveDataPaths(string environment, string players)
            {
                _Players = players;
                _Environment = environment;
            }
        }
}
