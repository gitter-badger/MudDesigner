using System;
using MudDesigner.Engine.Abstract.Actions;

namespace MudDesigner.Engine.Abstract.Objects
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
        string Type { get;  }
    }
}