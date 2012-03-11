using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

using MudEngine.GameScripts;
using MudEngine.Core;

namespace MudEngine.DAL
{
    public class XMLData
    {
        public XElement SaveData { get; private set; }

        /// <summary>
        /// Instances the ObjectSaver and all of its Properties and Fields
        /// </summary>
        public XMLData(String objectType)
        {
            SaveData = new XElement(objectType);
            this._RootNode = objectType;
        }

        public void AddSaveData(String property, String value)
        {
            this.SaveData.Add(new XElement(property, value));
        }

        public String GetData(String property)
        {
            foreach (XElement element in SaveData.Elements())
            {
                if (element.Name.LocalName == property)
                    return element.Value;
            }

            return String.Empty;
        }

        public String[] GetDataCollection(String property)
        {
            List<String> data = new List<string>();

            foreach (XElement element in SaveData.Elements())
            {
                if (element.Name.LocalName == property)
                    data.Add(element.Value);
            }

            return data.ToArray();
        }

        public Boolean Save(String filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }

            try
            {
                this.SaveData.Save(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean Load(String filename)
        {
            try
            {
                this.SaveData = XElement.Load(filename);

                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                return false;
            }
        }

        private String _RootNode;
    }
}
