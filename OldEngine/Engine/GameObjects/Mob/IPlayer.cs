//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob
{
    /// <summary>
    /// Creates a contract for objects whom want to implement IPlayer
    /// </summary>
    public interface IPlayer : IMob
    {
    }
}
