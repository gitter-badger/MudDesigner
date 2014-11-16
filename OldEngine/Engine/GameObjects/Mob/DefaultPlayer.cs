//-----------------------------------------------------------------------
// <copyright file="DefaultPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob
{
    [UniqueStorageIdentifier("Name")]
    [Serializable]
    public class DefaultPlayer : EngineMob, IPlayer
    {
        public DefaultPlayer() : base()
        {
            this.Name = "New Player";
        }
    }
}
