using System;

namespace MudEngine.World
{
    [System.Flags]
    public enum AvailableTravelDirections : uint
    {
        None = 0,
        North = 1,
        South = 2,
        East = 4,
        West = 8,
        Up = 16,
        Down = 32,
        Northeast = North | East,
        Northwest = North | West,
        Southeast = South | East,
        Southwest = South | West
    }

    public static class TravelDirections
    {
        public static AvailableTravelDirections GetReverseDirection(AvailableTravelDirections Direction)
        {
            switch (Direction)
            {
                case AvailableTravelDirections.North:
                    return AvailableTravelDirections.South;
                case AvailableTravelDirections.South:
                    return AvailableTravelDirections.North;
                case AvailableTravelDirections.East:
                    return AvailableTravelDirections.West;
                case AvailableTravelDirections.West:
                    return AvailableTravelDirections.East;
                case AvailableTravelDirections.Up:
                    return AvailableTravelDirections.Down;
                case AvailableTravelDirections.Down:
                    return AvailableTravelDirections.Up;
                case AvailableTravelDirections.Northeast:
                    return AvailableTravelDirections.Southwest;
                case AvailableTravelDirections.Southwest:
                    return AvailableTravelDirections.Northeast;
                case AvailableTravelDirections.Northwest:
                    return AvailableTravelDirections.Southeast;
                case AvailableTravelDirections.Southeast:
                    return AvailableTravelDirections.Northwest;
                default:
                    return AvailableTravelDirections.None;
            }
        }

        public static AvailableTravelDirections GetTravelDirectionValue(String Direction)
        {
            Array values = Enum.GetValues(typeof(AvailableTravelDirections));

            foreach (Int32 value in values)
            {
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), value);

                if (displayName.ToLower() == Direction.ToLower())
                    return (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
            }

            return AvailableTravelDirections.None;
        }
    }
}