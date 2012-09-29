//Microsoft .NET Using statements
using System.IO;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Abstract.Core;

//Abstract.Actions namespace is used for Types that will perform action based code. Such as saving, loading or being used.
namespace MudDesigner.Engine.Abstract.Actions
{
    /// <summary>
    /// Defines a method that allows loading content from a saved state.
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// TODO - Michael can you fill this out for me? - JS.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="reader"></param>
        void Load(IGame game, BinaryReader reader);
    }
}