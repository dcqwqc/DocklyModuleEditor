using Avalonia.Controls;
using Docklys.ModuleContracts;

namespace DefaultModule
{
    public partial class DefaultModule : UserControl, IModule
    {
        // Identification
        public string Id => "BlackModule";
        public string ModuleName => "Default Module";
        public string ModuleVersion => "1.0.0";
        public string Category => "Default";
        public string[] Tags => new [] { "DefaultModule", "example" };

        // Layout info
        public int TileWidth => 4;
        public int TileHeight => 8;

        // Compatibility
        public string MinAppVersion => "1.0.0";
        public string MaxAppVersion => "2.0.0";
        public string[] SupportedPlatforms => new [] { "Windows", "Linux", "Mac" };

        public DefaultModule()
        {
            InitializeComponent();
        }
    }
}