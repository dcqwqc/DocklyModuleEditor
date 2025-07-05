namespace Docklys.ModuleContracts
{
    public interface IModule
    {
        string ModuleName { get; }
        string ModuleVersion { get; }
        
        int PreferredTileWidth => 1;
        int PreferredTileHeight => 1;
        public class FontDummy
        {
        }
    }
}