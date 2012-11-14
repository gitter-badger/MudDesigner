/* IRoom
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Contract for implementing game Rooms for players to traverse.
 */
//Microsoft .NET using statements
using System.Collections.Generic;
using System;
using System.Collections;

//AllocateThis! Mud Designer using statements
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
        public string See { get; set; }
        public string Hear { get; set; }
        public string Smell { get; set; }
        public string Feel { get; set; }
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

        void AddCharacter(IMob character, AvailableTravelDirections entryDirection);

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
        
        void AddDecoration(string decoration);

        IItem[] GetItems();
        
        IItem GetItem(string itemName);
        
        string[] GetDecorations();

        /// <summary>
        /// Removes a game item from the Room.
        /// </summary>
        /// <param name="item">The item you want to remove</param>
        void RemoveItem(IItem item);
        
        void RemoveDecoration(string decoration);

        /// <summary>
        /// Clears the Room of all items.
        /// </summary>
        void ClearItems();
        
        void ClearDecorations();
    }
}