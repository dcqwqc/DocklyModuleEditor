namespace Dockly.ModuleContracts
{
    public interface IModule
    {
        string ModuleName { get; }
        string ModuleVersion { get; }
        
        int PreferredTileWidth => 1;
        int PreferredTileHeight => 1;
    }
}