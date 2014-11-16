//-----------------------------------------------------------------------
// <copyright file="FileIO.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using log4net;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Provides methods for saving and loading game objects. Most game objects can be saved, however any
    /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
    /// to the offending circular referenced property.
    /// </summary>
    public class FileIO : ISavable, ILoadable
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(FileIO)); 

        /// <summary>
        /// Provides methods for saving game objects. Most game objects can be saved with no special requirements, however any
        /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
        /// to the offending circular referenced property.
        /// </summary>
        /// <param name="objectToSave">A reference to an object that needs to be wrote to file</param>
        /// <param name="fullFilePath">The full path including filename that this file needs to be saved</param>
        public void Save(object objectToSave, string fullFilePath)
        {

            // Get the directory path without filename
            var path = Path.GetDirectoryName(fullFilePath);

            // Check if the path exists. If not, create it.
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check if the file already exists. If so, delete it
            if (File.Exists(fullFilePath))
                File.Delete(fullFilePath);

            // Setup our binary writer
            using (var writer = new BinaryWriter(File.Open(fullFilePath, FileMode.OpenOrCreate)))
            {
                var settings = new JsonSerializerSettings();
                // Must use the custom SerializationContracts contract to ensure
                // that public read-only properties have their values restored later.
                var contract = new SerializationContracts();

                settings.TypeNameHandling = TypeNameHandling.All;
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

                // Serialize the object to text.
                var serialziedObject = JsonConvert.SerializeObject(objectToSave, Formatting.Indented, settings);

                // Write the text to file.
                writer.Write(serialziedObject);
            }
        }

        /// <summary>
        /// Provides a method for loading game objects. Most game objects can be loaded after saving with no special requirements, however any
        /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
        /// to the offending circular referenced property.
        /// </summary>
        /// <param name="fullFilePath">The full path including filename that this file needs to be saved</param>
        /// <param name="t">The Type that needs to be instanced and restored.</param>
        /// <returns></returns>
        public object Load(string fullFilePath, Type t)
        {
            // Get the directory path without filename
            var path = Path.GetDirectoryName(fullFilePath);

            if (path == null)
            {
                return null;
            }

            // Check if the path exists. If not, create it.
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // if the file doesn't exist, abort.
            if (!File.Exists(fullFilePath)) return null;

            // Setup our binary reader.
            using (var br = new BinaryReader(File.Open(fullFilePath, FileMode.Open)))
            {
                // Read the contents of the file and store them
                var objectToLoad = br.ReadString();

                var settings = new JsonSerializerSettings();

                // Must use the custom SerializationContracts contract to ensure
                // that public read-only properties have their values restored later.
                var contract = new SerializationContracts();

                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                settings.TypeNameHandling = TypeNameHandling.All;
                settings.ContractResolver = contract;

                // Deserialize the object and return it.
                return JsonConvert.DeserializeObject<Object>(objectToLoad, settings);
            }
        }
    }
}
