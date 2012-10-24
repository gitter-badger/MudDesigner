using System;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Objects
{
    public interface IGameObject : ISaveable, ILoadable
    {
        Guid Id { get;}
        string Name { get; set; }
        string Description { get; set; }
    }
}