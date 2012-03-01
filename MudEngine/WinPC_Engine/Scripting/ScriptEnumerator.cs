using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameScripts;
using MudEngine.Core;

namespace MudEngine.Scripting
{
    internal class ScriptEnumerator : IEnumerator<BaseScript>
    {
        public ScriptEnumerator(ObjectCollection objectManager)
        {
            this._ObjectCollection = objectManager;
        }

        public void Reset()
        {
            this._currentIndex = -1;
        }

        public BaseScript Current
        {
            get
            {
                if (this._currentIndex < 0)
                    return null;
                else if (this._currentIndex > this._ObjectCollection.Length)
                    return null;
                else
                    return this._ObjectCollection[this._currentIndex];
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
