using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Networking
{
    public const int UDP = 2;
    class Socket
    {
        public Socket()
        {
            type = 0;
        }
        public ~Socket()
        {
            type = 0;
        }
        // all methods return int, 1 = success, -1 = error, > 1 are just extra info w/ success
        //  < -1, known error find it out with: (string)getError(er_code);

        public int init()
        {
            return 0;
        }
        public int start()
        {
            return 0;
        }
        public int bind()
        {
            return 0;
        }
        public int listen()
        {
            return 0;
        }
        public int accept()
        {
            return 0;
        }
        public int end()
        {
            return 0;
        }
        public string getError(int er_code)
        {
            if (er_code > 0)
                return "No Error";
            switch (er_code)
            {

            default:
                return "Unknown Error";
            }
        }
        private int type; // 1 = TCP, 2 = UDP
        private int stage;
    }
}
