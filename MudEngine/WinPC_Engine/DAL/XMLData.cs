using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

using MudEngine.GameScripts;

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
        }

        public void AddSaveData(String property, String value)
        {
            this.SaveData.Add(new XElement(property, value));
        }

        public Boolean Save(String filename)
        {
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
    }
}
