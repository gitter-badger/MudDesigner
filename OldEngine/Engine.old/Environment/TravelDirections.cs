//-----------------------------------------------------------------------
// <copyright file="TravelDirections.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// Available directions that a character can travel in the world.
    /// </summary>
    public enum AvailableTravelDirections
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The north
        /// </summary>
        North = 1,

        /// <summary>
        /// The south
        /// </summary>
        South = 2,

        /// <summary>
        /// The east
        /// </summary>
        East = 3,

        /// <summary>
        /// The west
        /// </summary>
        West = 4,

        /// <summary>
        /// Up
        /// </summary>
        Up = 5,

        /// <summary>
        /// Down
        /// </summary>
        Down = 6,
    }

    /// <summary>
    /// Manages the different actions associated with travel directions in the world.
    /// </summary>
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
            // Blow all of the available values up into an array.
            Array values = Enum.GetValues(typeof(AvailableTravelDirections));

            // Loop through each available value, converting it into a string.
            foreach (int value in values)
            {
                // Get the string representation of the current value
                String displayName = Enum.GetName(typeof(AvailableTravelDirections), value);

                // Check if this value matches that of the supplied one.
                // If so, return it as a enum
                if (displayName.ToLower() == Direction.ToLower())
                    return (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
            }

            return AvailableTravelDirections.None;
        }
    }
}