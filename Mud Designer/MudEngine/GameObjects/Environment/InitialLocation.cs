//MudEngine
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.Objects;
using MudDesigner.MudEngine.Objects.Environment;

namespace MudDesigner.MudEngine.Objects.Environment
{
    [Unusable(true)]
    public struct StartingLocation
    {
        public Room Room;
        public Zone Zone;
        public Realm Realm;
    }
}