using System.IO;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Abstract.Actions
{
    public interface ILoadable
    {
        void Load(IGame game, BinaryReader reader);
        
    }
}