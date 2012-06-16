namespace WinPC.Engine.Abstract.Actions
{
    public interface ISaveable
    {
        string Filename { get; }
        
        bool Save();
    }
}