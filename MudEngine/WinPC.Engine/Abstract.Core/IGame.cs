using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Abstract.Actions;
using MudDesigner.Engine.Abstract.Environment;
namespace MudDesigner.Engine.Abstract.Core
{
    public interface IGame : ILoadable, ISaveable
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        IWorld World { get; }

        bool Initialize(IServer startedServer, IWorld world);
        void Start();
        void Stop();
    }
}