//Microsoft .NET Using statements
using System.IO;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;

//Abstract.Actions namespace is used for Types that will perform action based code. Such as saving, loading or being used.
namespace MudDesigner.Engine.Actions
{    
    /// <summary>
    /// Defines a method that allows saving content to a binary saved state.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// TODO - Michael, can you fill this in for me? - JS
        /// </summary>
        /// <param name="writer"></param>
        void Save(BinaryWriter writer);
    }
}