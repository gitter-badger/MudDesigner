namespace MudDesigner.Engine.Objects
{
    public interface IInventory : IGameObject
    {
        InventoryBounds Bounds { get; }
    }
}