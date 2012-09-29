using System.IO;

namespace MudDesigner.Engine.Abstract.Actions
{
    public interface ISaveable
    {
        void Save(BinaryWriter writer);
    }
}