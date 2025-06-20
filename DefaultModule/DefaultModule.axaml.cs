using Avalonia.Controls;
using Dockly.ModuleContracts;
namespace Dockly.Modules
{
    public partial class DefaultModuleControl  : UserControl, IModule
    {
        public string ModuleName => "My External Module";
        public string ModuleVersion => "1.0.0";
    
        // Specify the Module size!
        public int PreferredTileWidth => 2;  // 2x2 tiles
        public int PreferredTileHeight => 2; // 2x2 tiles
        public DefaultModuleControl ()
        {
            InitializeComponent();
        }
    }
}