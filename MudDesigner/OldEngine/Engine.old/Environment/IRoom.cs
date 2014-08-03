//-----------------------------------------------------------------------
// <copyright file="IRoom.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System;
using System.Collections;
using MudDesigner.Engine.Core;
using MudDesigner;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    /// <summary>
    /// The 5-senses that the player can use to provide a detailed description of their environment
    /// </summary>
    public struct Senses
    {
        /// <summary>
        /// Gets or sets the see.
        /// </summary>
        /// <value>
        /// The see.
        /// </value>
        public string See { get; set; }

        /// <summary>
        /// Gets or sets the hear.
        /// </summary>
        /// <value>
        /// The hear.
        /// </value>
        public string Hear { get; set; }

        /// <summary>
        /// Gets or sets the smell.
        /// </summary>
        /// <value>
        /// The smell.
        /// </value>
        public string Smell { get; set; }

        /// <summary>
        /// Gets or sets the feel.
        /// </summary>
        /// <value>
        /// The feel.
        /// </value>
        public string Feel { get; set; }

        /// <summary>
        /// Gets or sets the taste.
        /// </summary>
        /// <value>
        /// The taste.
        /// </value>
        public string Taste { get; set; }
    }

    /// <summary>
    /// Contract for implementing game Rooms for players to traverse
    /// </summary>
    public interface IRoom : IEnvironment
    {
        /// <summary>
        /// Zone that this Room resides within
        /// </summary>
        IZone Zone { get; set; }

        /// <summary>
        /// Gets or Sets information pertaining to a users 5-senses for the Room.
        /// </summary>
        Senses Sense { get; set; }

        /// <summary>
        /// Gets or Sets the players that currently occupy this Room.
        /// </summary>
        List<IMob> Occupants { get; set; }

        /// <summary>
        /// Gets or Sets the collection of Doorways that this Room has, leading to other Rooms.
        /// </summary>
        Dictionary<AvailableTravelDirections, IDoor> Doorways { get; set; }

        /// <summary>
        /// Gets a reference to the Items collection within the Room.
        /// </summary>
        List<IItem> Items { get; set; }

        /// <summary>
        /// Adds the character.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="entryDirection">The entry direction.</param>
        void AddCharacter(IMob character, AvailableTravelDirections entryDirection);

        /// <summary>
        /// Removes the character.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="leavingDireciton">The leaving direciton.</param>
        void RemoveCharacter(IMob character, AvailableTravelDirections leavingDireciton);

        /// <summary>
        /// Adds a doorway to the Room, linking it to another Room in the world.
        /// </summary>
        /// <param name="direction">The direction that the player must travel in order to move through the doorway</param>
        /// <param name="arrivalRoom">The room that the player will enter, once they walk through the doorway</param>
        /// <param name="autoAddReverseDirection">If true, the arrival room will have the opposite doorway automatically linked back to this Room</param>
        /// <param name="forceOverwrite">If true, if a doorway already exists for the specified direction, it will overwrite it.</param>
        void AddDoorway(AvailableTravelDirections direction, IRoom arrival, bool autoAddReverseDireciton, bool forceOverwrite);
        
        /// <summary>
        /// Gets a reference to the doorway that matches the specified direction, if it exists
        /// </summary>
        /// <param name="direction">The direction that you want the doorway for.</param>
        /// <returns></returns>
        IDoor GetDoorway(AvailableTravelDirections direction);

        /// <summary>
        /// Doorways the exists.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        bool DoorwayExists(AvailableTravelDirections direction);

        /// <summary>
        /// Returns an array of all doorways within the Room.
        /// </summary>
        /// <returns></returns>
        IDoor[] GetDoorways();

        /// <summary>
        /// Allows for removal of a doorway.
        /// </summary>
        /// <param name="direction">The direction of travel that the doorway should be removed.</param>
        /// <param name="autoRemoveReverseDirection">If true, the room on the otherside of the doorway will have it's door removed as well.</param>
        void RemoveDoorway(AvailableTravelDirections direction, bool autoRemoveReverseDirection);

        /// <summary>
        /// Adds a game item to the Room.
        /// </summary>
        /// <param name="item">The item you want to add.</param>
        void AddItem(IItem item);

        /// <summary>
        /// Adds the decoration.
        /// </summary>
        /// <param name="decoration">The decoration.</param>
        void AddDecoration(string decoration);

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        IItem[] GetItems();

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        IItem GetItem(string itemName);

        /// <summary>
        /// Gets the decorations.
        /// </summary>
        /// <returns></returns>
        string[] GetDecorations();

        /// <summary>
        /// Removes a game item from the Room.
        /// </summary>
        /// <param name="item">The item you want to remove</param>
        void RemoveItem(IItem item);

        /// <summary>
        /// Removes the decoration.
        /// </summary>
        /// <param name="decoration">The decoration.</param>
        void RemoveDecoration(string decoration);

        /// <summary>
        /// Clears the Room of all items.
        /// </summary>
        void ClearItems();

        /// <summary>
        /// Clears the decorations.
        /// </summary>
        void ClearDecorations();
    }
}