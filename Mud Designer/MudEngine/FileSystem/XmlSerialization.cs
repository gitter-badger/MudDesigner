#region ====== Using Statements ======
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
#endregion

namespace MudDesigner.MudEngine.FileSystem
{
    internal class XmlSerialization
    {
        internal static void Save(string Filename, object o)
        {
            Stream stream = File.Create(Filename);

            XmlSerializer serializer = new XmlSerializer(o.GetType());
            serializer.Serialize(stream, o);
            stream.Close();
        }


        /// <summary>
        /// Loads an item via Xml Deserialization
        /// </summary>
        /// <param name="Filename">The Xml document to deserialize.</param>
        /// <returns></returns>
        internal static object Load(string Filename, object o)
        {
            Stream stream = File.OpenRead(Filename);

            object obj = new object();
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            obj = (object)serializer.Deserialize(stream);

            stream.Close();
            return obj;
        }
    }
}
