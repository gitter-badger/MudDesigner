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

            public String Scripts
            {
                get
                {
                    return _Scripts;
                }
                set
                {
                    _Scripts = value;
                }
            }

            private String _Players;
            private String _Environment;
            private String _Scripts;

            public SaveDataPaths(String environment, String players, String scripts)
            {
                _Players = players;
                _Environment = environment;
                _Scripts = scripts;
            }
        }
}
