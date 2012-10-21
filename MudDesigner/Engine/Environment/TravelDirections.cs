using System;

namespace MudDesigner.Engine.Environment
{
    //Available directions that the character can travel in the world.
    public enum AvailableTravelDirections
    {
        None = 0,
        North = 1,
        South = 2,
        East = 3,
        West = 4,
        Up = 5,
        Down = 6,
    }

    public static class TravelDirections
    {
        /// <summary>
        /// Returns a direction that is reversed from what was supplied.
        /// </summary>
        /// <param name="Direction"></param>
        /// <returns></returns>
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
                default:
                    return AvailableTravelDirections.None;
            }
        }

        /// <summary>
        /// Returns a enum value that matches that of the string supplied.
        /// </summary>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public static AvailableTravelDirections GetTravelDirectionValue(String Direction)
        {
            //Blow all of the available values up into an array.
            Array values = Enum.GetValues(typeof(AvailableTravelDirections));

            //Loop through each available value, converting it into a string.
            foreach (int value in values)
            {
                //Get the string representation of the current value
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), value);

                //Check if this value matches that of the supplied one.
                //If so, return it as a enum
                if (displayName.ToLower() == Direction.ToLower())
                    return (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
            }

            return AvailableTravelDirections.None;
        }
    }
}