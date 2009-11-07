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

        /// <summary>
        /// Saves the object using the specified output format
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="o"></param>
        public static void Save(string Filename, object o)
        {
            if (FileType == OutputFormats.XML)
            {
                XmlSerialization.Save(Filename, o);
            }
        }

        /// <summary>
        /// Loads the object using the specified FileType format
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="o"></param>
        /// <returns></returns>
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
