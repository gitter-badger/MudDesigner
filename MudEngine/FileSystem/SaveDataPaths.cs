using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.FileSystem
{
        public struct SaveDataPaths
        {
            public String Players 
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

            public String Environment
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
            private String _Players;
            private String _Environment;

            public SaveDataPaths(String environment, String players)
            {
                _Players = players;
                _Environment = environment;
            }
        }
}
