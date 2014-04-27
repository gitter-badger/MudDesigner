// Mud Designer Framework
using Mud.Models;

namespace Mud.DataAccess
{
    /// <summary>
    /// The IDataContext interface exposes several methods that must be implemented in order for a storage container to work with the Mud Engine.
    /// This provides an abstract approach to data persistance, allowing multiple storage types to be used without having to make engine or object level changes.
    /// You can use a flat-file system on the local disk or store the data in a database. By implementing IDataContext you can provide your custom storage mechanism to the engines IGame.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Gets or Sets the root path for the data storage.
        /// </summary>
        string RootPath { get; set; }

        /// <summary>
        /// Initializes the data context for data persistance. 
        /// Any data persistance initialization such as setting up directory structure or establishing a database connection is performed.
        /// </summary>
        /// <returns>Returns true if the context was initialized properly.</returns>
        bool InitializeContext();
        
        /// <summary>
        /// Saves an object in the context's data storage format
        /// </summary>
        /// <param name="item">The item that needs to be persisted to storage.</param>
        /// <returns>Returns true if the data is saved.</returns>
        bool Save(IGameObject item);

        /// <summary>
        /// Saves an object in the context's data storage format.
        /// </summary>
        /// <typeparam name="T">The Type that will be persisted.</typeparam>
        /// <param name="item">The object to be persisted.</param>
        /// <returns>Returns true if the data is saved.</returns>
        bool Save<T>(IGameObject item) where T : class, new();

        /// <summary>
        /// Loads an object from the context's storage.
        /// </summary>
        /// <typeparam name="T">The Type that the data must be returned as.</typeparam>
        /// <param name="item">The unique identifier belonging to the data being loaded.</param>
        /// <returns>Returns an instance of the object once loaded.</returns>
        T Load<T>(IGameObject item) where T : class, new();

        /// <summary>
        /// Deletes an object from the context's storage.
        /// </summary>
        /// <typeparam name="T">The type that the data must be returned as.</typeparam>
        /// <param name="item">The unique identifier belonging to the data being deleted.</param>
        /// <returns>Returns true if the object was deleted.</returns>
        bool Delete<T>(IGameObject item) where T : class, new();
    }
}
