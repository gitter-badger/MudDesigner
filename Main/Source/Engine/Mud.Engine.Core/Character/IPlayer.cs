﻿//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Core.Engine;
namespace Mud.Engine.Core.Character
{
    /// <summary>
    /// Provides a contract for objects wanting to act as an IPlayer Type.
    /// </summary>
    public interface IPlayer : ICharacter
    {
        IPermission Permission { get; }
    }
}
