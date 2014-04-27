// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Mud Designer Framework
using Mud.DataAccess;
using Mud.Models;

namespace Mud.DataAccess.FileSystem
{
    /// <summary>
    /// The FileSystemContext object is used to persist object data to the local disk using flat files.
    /// </summary>
    // TODO: Conditional check for .NET 4.0 and and flag this class as targeted for Windows XP
    // [PlatformSupport("Windows XP", 5, 0)] // Not supported due to .NET Framework 4.5 not being available for the OS.
    [PlatformSupport("Windows Vista", 6, 0)]
    [PlatformSupport("Windows 7", 6, 1)]
    [PlatformSupport("Windows 8", 6, 2)]
    [PlatformSupport("Windows 8.1", 6, 3)]
    [DisplayName("File System Data Context")]
    public class FileSystemContext : IDataContext
    {
        /// <summary>
        /// Gets or Sets the root path for the data storage.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Instances the File System context using a default directory of /Data
        /// </summary>
        public FileSystemContext()
            : this(Path.Combine(Environment.CurrentDirectory, "Data"))
        { }

        /// <summary>
        /// Instances the File System context with data stored in the specified path.
        /// </summary>
        /// <param name="rootPath">The desired path for data storage.</param>
        public FileSystemContext(string rootPath)
        {
            this.RootPath = rootPath;
        }

        /// <summary>
        /// Initializes the data context for data persistance. 
        /// This ensures that the path specified in RootPath exists prior to use.
        /// </summary>
        /// <returns>Returns true if the context was initialized properly.</returns>
        public bool InitializeContext()
        {
            // Make sure path specified exists.
            if (!Directory.Exists(this.RootPath))
                Directory.CreateDirectory(this.RootPath);

            return true;
        }

        /// <summary>
        /// Saves an object to the local disk.
        /// </summary>
        /// <param name="item">The item that needs to be persisted to storage.</param>
        /// <returns>Returns true if the data is saved.</returns>
        public bool Save(IGameObject item)
        {
            // Set up the path and filename to save the object.
            // We use a directory structure of Rootpath/TypeName/ItemName.
            /* (Example)
             * + RootPath
             * + --Realm
             * + ----MyRealm1.realm
             * + ----AnotherRealm.realm
             */
            string savePath = Path.Combine(RootPath, item.GetType().Name);
            string filename = item.Name;

            // Make sure the full path exists prior to trying to save.
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            // Get all of the properties within the object.
            PropertyInfo[] properties = item.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance); ;

            // Create the file and persist the properties to disk.
            using (StreamWriter file = new StreamWriter(Path.Combine(savePath, filename)))
            {
                try
                {
                    foreach (PropertyInfo property in properties)
                    {
                        // If the property has the Ignore attribute, then we do not persist it to disk.
                        if (property.GetCustomAttributes().Contains(default(IgnoreAttribute)))
                            continue;
                        // Otherwise we write the property name and its value to disk.
                        else
                        {
                            file.WriteLine(property.Name + "=" + property.GetValue(item).ToString());
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Saves an object to disk. This method is not used by the File System context. 
        /// When invoked, it redirects to FileSystemContext.Save(IGameObject)
        /// </summary>
        /// <typeparam name="T">The Type that will be persisted.</typeparam>
        /// <param name="item">The object to be persisted.</param>
        /// <returns>Returns true if the data is saved.</returns>
        public bool Save<T>(IGameObject item) where T : class, new()
        {
            return this.Save(item);
        }

        /// <summary>
        /// Loads an object from disk.
        /// </summary>
        /// <typeparam name="T">The Type that the data must be returned as.</typeparam>
        /// <param name="item">The unique identifier belonging to the data being loaded.</param>
        /// <returns>Returns an instance of the object once loaded.</returns>
        public T Load<T>(IGameObject item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an object from disk.
        /// </summary>
        /// <typeparam name="T">The type that the data must be returned as.</typeparam>
        /// <param name="identifier">The unique identifier belonging to the data being deleted.</param>
        /// <returns>Returns true if the object was deleted.</returns>
        public bool Delete<T>(IGameObject item) where T : class, new()
        {            // Set up the path and filename to save the object.
            // We use a directory structure of Rootpath/TypeName/ItemName.
            /* (Example)
             * + RootPath
             * + --Realm
             * + ----MyRealm1.realm
             * + ----AnotherRealm.realm
             */
            string savePath = Path.Combine(RootPath, item.GetType().Name);
            string filename = item.Name;

            // Make sure the full path exists prior to trying to save.
            if (!Directory.Exists(savePath))
                return false;

            // If the file exists, we delete it.
            if (File.Exists(filename))
                File.Delete(filename);
            else
                return false;

            return true;
        }
    }
}
