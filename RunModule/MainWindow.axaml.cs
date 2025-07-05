using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.VisualTree;
using Avalonia.Rendering.SceneGraph;
using SkiaSharp;
using System.IO;
using Avalonia;
using System.Diagnostics;
using System;
using Docklys.ModuleContracts;

namespace RunModule;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Auto-size window to fit module content
        AutoSizeWindow();
        Console.WriteLine(typeof(IModule.FontDummy).Assembly.GetName().Name);

    }

    private void AutoSizeWindow()
    {
        var control = this.FindControl<Control>("ModuleControl");
        if (control == null) return;

        // Get screen dimensions
        var screen = Screens.Primary;
        var workingArea = screen?.WorkingArea ?? new PixelRect(0, 0, 1920, 1080);
        var maxWidth = workingArea.Width * 0.9; // 90% of screen width
        var maxHeight = workingArea.Height * 0.9; // 90% of screen height

        // Force measure the control to get its desired size
        control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        var desiredSize = control.DesiredSize;

        // Get button height (approximately 40px + margins)
        var buttonHeight = 60;

        // Calculate window size based on module content
        var windowWidth = Math.Min(Math.Max(desiredSize.Width + 40, 400), maxWidth); // Min 400px, max 90% screen
        var windowHeight = Math.Min(Math.Max(desiredSize.Height + buttonHeight + 40, 300), maxHeight); // Min 300px, max 90% screen

        // Set window size
        this.Width = windowWidth;
        this.Height = windowHeight + 50; // Add extra space for WebP Button and margins
        
        // Position window in center-right of screen
        var centerX = workingArea.X + workingArea.Width - windowWidth - 20;
        var centerY = workingArea.Y + (workingArea.Height / 2) - ((windowHeight + 50) / 2) - 15; // Center vertically -15 for Header
        
        this.Position = new PixelPoint((int)centerX, (int)centerY);
        
        // Set minimum size to ensure usability
        this.MinWidth = Math.Min(400, windowWidth);
        this.MinHeight = Math.Min(300, windowHeight);

        Debug.WriteLine($"Module desired size: {desiredSize.Width} x {desiredSize.Height}");
        Debug.WriteLine($"Window size set to: {windowWidth} x {windowHeight + 50}");
        Debug.WriteLine($"Window position set to: {centerX} x {centerY}");
        Debug.WriteLine($"Screen working area: {workingArea.Width} x {workingArea.Height}");
    }

    private void CaptureToWebP_Click(object sender, RoutedEventArgs e)
    {
        var control = this.FindControl<Control>("ModuleControl");
        if (control == null) return;

        var bounds = control.Bounds;
        var size = new PixelSize((int)bounds.Width, (int)bounds.Height);
        var dpi = new Vector(96, 96);
        
        using var rtb = new RenderTargetBitmap(size, dpi);
        control.Measure(bounds.Size);
        control.Arrange(bounds);
        rtb.Render(control);
        
        using var stream = new MemoryStream();
        rtb.Save(stream);
        stream.Seek(0, SeekOrigin.Begin);

        // Load into SkiaSharp bitmap
        using var skStream = new SKManagedStream(stream);
        using var skBitmap = SKBitmap.Decode(skStream);

        // Save as WebP
        var currentDir = Directory.GetCurrentDirectory();
        
        // Level up in directory
        var searchDir = currentDir;
        string editorRoot = null;
        
        // Keep going up until we find DocklyModuleEditor folder
        while (searchDir != null && !Path.GetFileName(searchDir).Equals("DocklyModuleEditor", StringComparison.OrdinalIgnoreCase))
        {
            searchDir = Directory.GetParent(searchDir)?.FullName;
        }
        
        if (searchDir != null)
        {
            editorRoot = searchDir;
        }

        
        var outputDir = Path.Combine(editorRoot, "OutputWebPModule");
        
        // Create directory if it doesn't exist
        Directory.CreateDirectory(outputDir);
        
        var savePath = Path.Combine(outputDir, "ModulePreview.webp");
        
        // Debug output
        Debug.WriteLine($"Current Directory: {currentDir}");
        Debug.WriteLine($"Found Editor Root: {editorRoot}");
        Debug.WriteLine($"Output Directory: {outputDir}");
        Debug.WriteLine($"Full Save Path: {savePath}");
        Debug.WriteLine($"File will be saved at: {Path.GetFullPath(savePath)}");
        
        // console for non-debug
        System.Console.WriteLine($"Current Directory: {currentDir}");
        System.Console.WriteLine($"Found Editor Root: {editorRoot}");
        System.Console.WriteLine($"Output Directory: {outputDir}");
        System.Console.WriteLine($"Full Save Path: {savePath}");
        System.Console.WriteLine($"File will be saved at: {Path.GetFullPath(savePath)}");
        
        using var output = File.OpenWrite(savePath);
        using var skWebp = SKImage.FromBitmap(skBitmap);
        using var skData = skWebp.Encode(SKEncodedImageFormat.Webp, 90);
        skData.SaveTo(output);
        
        // Confirm file was created
        if (File.Exists(savePath))
        {
            var fileInfo = new FileInfo(savePath);
            Debug.WriteLine($"✓ WebP file created successfully!");
            Debug.WriteLine($"File size: {fileInfo.Length} bytes");
            Debug.WriteLine($"Created at: {fileInfo.CreationTime}");
            
            System.Console.WriteLine($"✓ WebP file created successfully!");
            System.Console.WriteLine($"File size: {fileInfo.Length} bytes");
            System.Console.WriteLine($"Created at: {fileInfo.CreationTime}");
        }
        else
        {
            Debug.WriteLine("❌ Failed to create WebP file");
            System.Console.WriteLine("❌ Failed to create WebP file");
        }
    }
}