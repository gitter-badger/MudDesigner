using System;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Objects
{
    public interface IGameObject : ISaveable, ILoadable
    {
        Guid Id { get;}
    }
}