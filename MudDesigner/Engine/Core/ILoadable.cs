//Microsoft .NET Using statements
using System.IO;

//Abstract.Actions namespace is used for Types that will perform action based code. Such as saving, loading or being used.
namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Defines a method that allows loading content from a saved state.
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// This Load function is similar to the save method. It is a recursive load that follows the path of save and reloads objects that were saved. 
        /// If for any reason you change a class post-Save than reloading it will be almost impossible as certain properties will not be likely to be reloaded.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="reader"></param>
        void Load(IGame game, BinaryReader reader);
    }
}