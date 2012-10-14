using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
namespace MudDesigner.Engine.Scripting
{
    internal class ScriptEnumerator : IEnumerator<BaseScript>
    {
        public ScriptEnumerator(ObjectCollection objectManager)
        {
            _ObjectCollection = objectManager;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public BaseScript Current
        {
            get
            {
                if (_currentIndex < 0)
                    return null;
                else if (_currentIndex > _ObjectCollection.Length)
                    return null;
                else
                    return _ObjectCollection[_currentIndex];
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        private Int32 _currentIndex = -1;
        private ObjectCollection _ObjectCollection;
    }
}
*/