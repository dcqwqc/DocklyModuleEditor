using Avalonia.Controls;
using Dockly.ModuleContracts;

namespace Dockly.Modules
{
    public partial class DefaultModuleControl : UserControl, IModule
    {
        // Identification
        public string Id => "DefaultModule";
        public string ModuleName => "Default Module";
        public string ModuleVersion => "1.0.0";
        public string Category => "Default";
        public string[] Tags => new string[] { "Default", "example" };

        // Layout info
        public int TileWidth => 2;
        public int TileHeight => 2;

        // Compatibility
        public string MinAppVersion => "1.0.0";
        public string MaxAppVersion => "2.0.0";
        public string[] SupportedPlatforms => new string[] { "Windows", "Linux", "Mac" };

        public DefaultModuleControl()
        {
            InitializeComponent();
        }
    }
}