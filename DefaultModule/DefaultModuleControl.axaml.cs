using Avalonia.Controls;
using Docklys.ModuleContracts;

namespace Docklys.Modules
{
    public partial class DefaultModuleControl : UserControl, IModule
    {
        // Identification
        public string Id => "BlackModule";
        public string ModuleName => "Default Module";
        public string ModuleVersion => "1.0.0";
        public string Category => "Default";
        public string[] Tags => new string[] { "DefaultModule", "example" };

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