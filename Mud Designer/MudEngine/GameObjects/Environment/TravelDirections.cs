namespace MudDesigner.MudEngine.GameObjects
{
    public enum AvailableTravelDirections
    {
        None = 0,
        North,
        South,
        East,
        West,
        Up,
        Down,
    }

    public static class TravelDirections
    {
        public static AvailableTravelDirections GetReverseDirection(AvailableTravelDirections Direction)
        {
            switch (Direction)
            {
                case AvailableTravelDirections.Down:
                    return AvailableTravelDirections.Up;
                case AvailableTravelDirections.East:
                    return AvailableTravelDirections.West;
                case AvailableTravelDirections.None:
                    return AvailableTravelDirections.None;
                case AvailableTravelDirections.North:
                    return AvailableTravelDirections.South;
                case AvailableTravelDirections.South:
                    return AvailableTravelDirections.North;
                case AvailableTravelDirections.Up:
                    return AvailableTravelDirections.Down;
                case AvailableTravelDirections.West:
                    return AvailableTravelDirections.East;
                default:
                    return AvailableTravelDirections.None;
            }
        }
    }
}