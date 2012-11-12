/* INPC
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: A interface contract that provides properties and methods for creating custom NPC Characters
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Environment;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// A interface contract that provides properties and methods for creating custom NPC Characters
    /// </summary>
    public interface INPC : IMob
    {
        /// <summary>
        /// Gets or Sets if this Pawn can move around the world or is restricted to its current Room.
        /// </summary>
        bool Stationary { get; set; }

        /// <summary>
        /// Gets or Sets if a IPlayer can control this Pawn such as through a marriage system or Pet system.
        /// </summary>
        bool CanControl { get; set; }

        /// <summary>
        /// Gets or Sets an array of Rooms that the Pawn can move around, provided IPawn.Stationary is false.
        /// </summary>
        IRoom[] TraversableMap { get; set; }

        /// <summary>
        /// Gets or Sets if this Pawn is restricted to traversing the IPawn.TraversableMap.  If False, the Pawn can freely travel.
        /// </summary>
        bool RestrictToMap { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of Rooms the Pawn can travel if IPawn.RestrictToMap is False and TraversableMap is null.  Set to 0 to allow unlimited distance.
        /// Set IPawn.Stationary to True to prevent travel, regardless of MaxTravelDistance.
        /// </summary>
        int MaxTravelDistance { get; set; }

        /// <summary>
        /// Gets or Sets this Pawns original Room.  IPawn.MaxTravelDistance will calculate distance from this location if IPawn.RestrictToMap is false, regardless if Ipawn.TraversableMap is null or not.
        /// This Property is ignored if IPawn.RestrictToMap is True.
        /// </summary>
        IRoom Origin { get; set; }

        /// <summary>
        /// The faction that this NPC belongs to.
        /// </summary>
        IFaction Faction { get; set; }

        /// <summary>
        /// Gets or Sets the alignment for this NPC.
        /// </summary>
        Alignments Alignment { get; set; }

        void Update();

        void OnTalk(IPlayer initiator);
        void OnControl(IPlayer owner); //Called when the IPlayer takes control of this object. Such as marraige or a pet that can recieve commands.
        void OnMobEnter(IMob initiator);
    }
}
