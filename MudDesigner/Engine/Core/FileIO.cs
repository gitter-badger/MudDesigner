using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Newtonsoft.Json;
namespace MudDesigner.Engine.Core
{
    public class FileIO : ISaveable, ILoadable
    {
        public void Save(object objectToSave, string fullFilePath)
        {
            var path = Path.GetDirectoryName(fullFilePath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var writer = new BinaryWriter(File.Open(fullFilePath, FileMode.OpenOrCreate)))
            {
                var settings = new JsonSerializerSettings();
                var contract = new SerializationContracts();

                settings.TypeNameHandling = TypeNameHandling.All;
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

                var serialziedObject = JsonConvert.SerializeObject(objectToSave, Formatting.Indented, settings);
                writer.Write(serialziedObject);
            }
        }

        public object Load(string fullFilePath, Type t)
        {
            var path = fullFilePath;

            if (path == null)
            {
                return null;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(string.Format(path));
            }

            if (!File.Exists(path)) return null;

            using (var br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var objectToLoad = br.ReadString();
                var settings = new JsonSerializerSettings();
                var contract = new SerializationContracts();
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                settings.TypeNameHandling = TypeNameHandling.All;
                settings.ContractResolver = contract;


                return JsonConvert.DeserializeObject<Object>(objectToLoad, settings);
            }
        }
    }
}
