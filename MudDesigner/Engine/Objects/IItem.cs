using System;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Objects
{
    public interface IItem : IGameObject
    {
         //TODO - Add restriction lists.
        Guid ObjectBound { get; set; }
    }
}