using Avalonia.Controls;
using Docklys.ModuleContracts;

namespace Docklys.Modules
{
    public partial class DefaultModuleControl : UserControl, IModule
    {
        // Identification
        public string Id => "ModuleId";
        public string ModuleName => "ModuleDisplayName";
        public string ModuleVersion => "ModuleVersion";
        public string Category => "ModuleCategory";
        public string[] Tags => new string[] { "ModuleTags" };

        // Layout info
        public int TileWidth => TileWidth;
        public int TileHeight => TileHeight;

        // Compatibility
        public string MinAppVersion => "MinAppVersion";
        public string MaxAppVersion => "MaxAppVersion";
        public string[] SupportedPlatforms => new string[] { "SupportedPlatforms" };

        public DefaultModuleControl()
        {
            InitializeComponent();
        }
    }
}