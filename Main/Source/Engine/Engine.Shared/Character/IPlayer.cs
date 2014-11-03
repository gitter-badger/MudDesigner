//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Mud.Engine.Shared.Core;
namespace Mud.Engine.Shared.Character
{
    /// <summary>
    /// Provides a contract for objects wanting to act as an IPlayer Type.
    /// </summary>
    public interface IPlayer : ICharacter
    {
        IPermission Permission { get; }
    }
}
