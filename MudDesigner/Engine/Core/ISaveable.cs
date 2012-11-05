//Microsoft .NET Using statements
using System;
using System.IO;

//Abstract.Actions namespace is used for Types that will perform action based code. Such as saving, loading or being used.
namespace MudDesigner.Engine.Core
{    
    /// <summary>
    /// Defines a method that allows saving content to a binary saved state.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// An ISaveable interface is attachable to any object we wan to save, by default we will just make any GameObject ISaveable so that it can be picked up with 
        /// the writer and saved to a binary file.
        /// Save Command is recursive and should write all properties within a class to file, if the class object has any lists or IEnumerable gameObjects such as a 
        /// container in a room, then this command should call Save on its game objects before ending. Until the save command is no longer called through n+ISaveable Objects 
        /// </summary>
        /// <param name="writer"></param>
        void Save(object objectToSave, string fullFilePath);
    }
}