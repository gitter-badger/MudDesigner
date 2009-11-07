using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEngine.FileSystem
{
    public static class FileSystem
    {
        public enum OutputFormats
        {
            XML = 0,
        }

        /// <summary>
        /// The filetype that the MUDs files will be saved as
        /// </summary>
        public static OutputFormats FileType
        {
            get;
            set;
        }

        public static void Save(string Filename, object o)
        {
            if (FileType == OutputFormats.XML)
            {
                XmlSerialization.Save(Filename, o);
            }
        }

        public static object Load(string Filename, object o)
        {
            if (FileType == OutputFormats.XML)
            {
                return XmlSerialization.Load(Filename, o);
            }
            else return null;
        }
    }
}
