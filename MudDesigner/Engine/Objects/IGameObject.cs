using System;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Objects
{
    public enum GameObjectType
    {
        Room,
        Player,
        Enemy,
        Realm,
        World,
        Item,
    }

    public interface IGameObject : ISaveable, ILoadable
    {
        Guid Id { get;}
        GameObjectType Type { get; }

    }
}