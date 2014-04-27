//-----------------------------------------------------------------------
// <copyright file="IRace.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// A interface contract that provides properties and methods for creating custom character races
    /// </summary>
    public interface IRace
    {
        /// <summary>
        /// Gets or Sets the name of the race
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or Sets the Stat bonus for the race
        /// </summary>
        Dictionary<string, IStat> Stats { get; set; }

        /// <summary>
        /// Gets or Sets the Gender restrictions in-place for this race.
        /// </summary>
        List<IGender> GenderRestrictions { get; set; }

        /// <summary>
        /// Gets or Sets factions that are specific to this race
        /// </summary>
        Dictionary<string, IFaction> RacialFactions { get; set; }

        /// <summary>
        /// Gets or Sets default racial appearances
        /// </summary>
        Dictionary<string, IAppearanceAttribute> Appearance { get; set; }

        /// <summary>
        /// Adds a appearance attribute to the Race
        /// </summary>
        /// <param name="appearanceItem"></param>
        void AddAppearanceItem(IAppearanceAttribute appearanceItem);

        /// <summary>
        /// Adds a race specific faction to this race
        /// </summary>
        /// <param name="faction"></param>
        void AddFaction(IFaction faction);

        /// <summary>
        /// Adds a stat bonus to this race
        /// </summary>
        /// <param name="stat"></param>
        void AddStat(IStat stat);

        /// <summary>
        /// Gets an array of stat bonus that the race has
        /// </summary>
        /// <returns></returns>
        IStat[] GetStats();

        /// <summary>
        /// Gets an array of factions that pertain to this race.
        /// </summary>
        /// <returns></returns>
        IFaction[] GetFactions();
    }
}
