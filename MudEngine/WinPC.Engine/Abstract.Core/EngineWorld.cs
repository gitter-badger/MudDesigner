using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinPC.Engine.Abstract.Core
{
    public class EngineWorld : IWorld
    {
        private List<IRealm> realms = new List<IRealm>();

        public string Name { get; set; }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, List<IRealm> realms)
        {
            throw new NotImplementedException();
        }

        public IRealm GetRealm(string realmName)
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

        public int IndexOf(IRealm item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IRealm item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IRealm this[int index]
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

        public void Add(IRealm item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IRealm item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IRealm[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(IRealm item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IRealm> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public List<IRealm> Realms
        {
            get { throw new NotImplementedException(); }
        }
    }
}
