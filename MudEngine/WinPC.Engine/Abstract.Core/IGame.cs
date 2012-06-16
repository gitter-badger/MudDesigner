using WinPC.Engine.Abstract.Networking;
using WinPC.Engine.Abstract.Actions;

namespace WinPC.Engine.Abstract.Core
{
    public interface IGame : ILoadable, ISaveable, IUpdatable
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        IWorld World { get; }

        void Initialize(IServer startedServer, IWorld world);
        void Start();
        void Stop();
    }
}