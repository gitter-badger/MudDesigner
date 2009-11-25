using MUDEngine.Attributes;

namespace MUDEngine.Objects.Environment
{
    [Unusable(true)]
    public struct StartingLocation
    {
        public Room Room;
        public Zone Zone;
        public Realm Realm;
    }
}