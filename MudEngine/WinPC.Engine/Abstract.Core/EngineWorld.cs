using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinPC.Engine.Abstract.Core
{
    public class EngineWorld : IWorld
    {
        public List<IRealm> Realms
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, List<IRealm> realms)
        {
            throw new NotImplementedException();
        }

        public bool Load(string filename)
        {
            throw new NotImplementedException();
        }

        public string Filename
        {
            get { throw new NotImplementedException(); }
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
