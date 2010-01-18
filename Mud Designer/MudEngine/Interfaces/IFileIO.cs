using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.MudEngine.Interfaces
{
    public interface IFileIO
    {
        string Filename { get; }

        Object Load(string filename);
        void Save(string filename);
    }
}
