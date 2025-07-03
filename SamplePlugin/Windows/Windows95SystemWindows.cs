using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

// My Computer Window
public class MyComputerWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    
    public MyComputerWindow(Windows95Desktop desktop) 
        : base("My Computer", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(400, 300);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Computer"))
        {
            ImGui.TextUnformatted("💾 3½ Floppy (A:)");
            ImGui.TextUnformatted("💿 CD-ROM Drive (D:)");
            ImGui.TextUnformatted("🖥️ Hard Disk (C:)");
            ImGui.TextUnformatted("🌐 Network Drive (Z:)");
            
            ImGui.Spacing();
            ImGui.TextUnformatted("System Information:");
            ImGui.BulletText("Windows 95 (Simulated)");
            ImGui.BulletText("486 DX2/66 Processor");
            ImGui.BulletText("8 MB RAM");
            ImGui.BulletText("540 MB Hard Drive");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("MyComputer");
        }
    }
}

// Network Neighborhood Window
public class NetworkNeighborhoodWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    
    public NetworkNeighborhoodWindow(Windows95Desktop desktop) 
        : base("Network Neighborhood", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(350, 250);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Network"))
        {
            ImGui.TextUnformatted("🖥️ WORKGROUP");
            ImGui.Indent();
            ImGui.TextUnformatted("💻 COMPUTER1");
            ImGui.TextUnformatted("💻 COMPUTER2");
            ImGui.TextUnformatted("🖨️ PRINTER-01");
            ImGui.Unindent();
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Network Status: Connected");
            ImGui.TextUnformatted("Protocol: NetBEUI, TCP/IP");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("NetworkNeighborhood");
        }
    }
}

// Recycle Bin Window
public class RecycleBinWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    
    public RecycleBinWindow(Windows95Desktop desktop) 
        : base("Recycle Bin", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(400, 300);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Deleted Items"))
        {
            ImGui.TextUnformatted("🗑️ The Recycle Bin is empty");
            ImGui.Spacing();
            
            ImGui.TextUnformatted("Tip: You can restore deleted files by");
            ImGui.TextUnformatted("selecting them and clicking 'Restore'.");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        
        if (Windows95Theme.RaisedButton("Empty Recycle Bin"))
        {
            // Already empty
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("RecycleBin");
        }
    }
}

// My Documents Window
public class MyDocumentsWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    
    public MyDocumentsWindow(Windows95Desktop desktop) 
        : base("My Documents", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(450, 350);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Documents"))
        {
            ImGui.TextUnformatted("📄 Letter to Mom.doc");
            ImGui.TextUnformatted("📊 Budget 1995.xls");
            ImGui.TextUnformatted("🖼️ Vacation Photos");
            ImGui.TextUnformatted("📝 Shopping List.txt");
            ImGui.TextUnformatted("💾 Backup Files");
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Total: 5 items");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        
        if (Windows95Theme.RaisedButton("Open"))
        {
            // Open selected file
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Delete"))
        {
            // Delete selected file
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("MyDocuments");
        }
    }
}

// Control Panel Window
public class ControlPanelWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    
    public ControlPanelWindow(Windows95Desktop desktop) 
        : base("Control Panel", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(500, 400);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("System Settings"))
        {
            // Control Panel icons in a grid
            var buttonSize = new Vector2(80, 80);
            
            if (Windows95Theme.RaisedButton("🖥️\nDisplay", buttonSize))
            {
                // Open Display Properties
            }
            
            ImGui.SameLine();
            if (Windows95Theme.RaisedButton("🔊\nSounds", buttonSize))
            {
                // Open Sound Properties
            }
            
            ImGui.SameLine();
            if (Windows95Theme.RaisedButton("🖱️\nMouse", buttonSize))
            {
                // Open Mouse Properties
            }
            
            ImGui.SameLine();
            if (Windows95Theme.RaisedButton("⌨️\nKeyboard", buttonSize))
            {
                // Open Keyboard Properties
            }
            
            // Second row
            if (Windows95Theme.RaisedButton("🌐\nNetwork", buttonSize))
            {
                // Open Network Properties
            }
            
            ImGui.SameLine();
            if (Windows95Theme.RaisedButton("📅\nDate/Time", buttonSize))
            {
                // Open Date/Time Properties
            }
            
            ImGui.SameLine();
            if (Windows95Theme.RaisedButton("🖨️\nPrinters", buttonSize))
            {
                // Open Printers folder
            }
            
            ImGui.SameLine();
            if (Windows95Theme.RaisedButton("➕\nAdd/Remove", buttonSize))
            {
                // Open Add/Remove Programs
            }
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("ControlPanel");
        }
    }
}

// Settings Window
public class SettingsWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    private bool enableSounds = true;
    private bool showClock = true;
    private int colorDepth = 1;
    private readonly string[] colorOptions = { "16 colors", "256 colors", "High Color (16-bit)" };
    
    public SettingsWindow(Windows95Desktop desktop) 
        : base("Settings", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(350, 300);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("System Settings"))
        {
            ImGui.Checkbox("Enable system sounds", ref enableSounds);
            ImGui.Checkbox("Show clock in taskbar", ref showClock);
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Color depth:");
            Windows95Theme.BeginInsetFrame();
            ImGui.Combo("##ColorDepth", ref colorDepth, colorOptions, colorOptions.Length);
            Windows95Theme.EndInsetFrame();
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Screen resolution: 640 x 480");
            ImGui.TextUnformatted("Refresh rate: 60 Hz");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        
        if (Windows95Theme.RaisedButton("OK"))
        {
            Desktop.CloseWindow("Settings");
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Cancel"))
        {
            Desktop.CloseWindow("Settings");
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Apply"))
        {
            // Apply settings
        }
    }
}

// Help Window
public class HelpWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    
    public HelpWindow(Windows95Desktop desktop) 
        : base("Windows Help", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(500, 400);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Help Topics"))
        {
            ImGui.TextUnformatted("📖 Getting Started with Windows 95");
            ImGui.TextUnformatted("🖱️ Using the Mouse");
            ImGui.TextUnformatted("⌨️ Keyboard Shortcuts");
            ImGui.TextUnformatted("📁 Working with Files and Folders");
            ImGui.TextUnformatted("🖥️ Customizing Your Desktop");
            ImGui.TextUnformatted("🌐 Networking");
            ImGui.TextUnformatted("🔧 Troubleshooting");
            
            ImGui.Spacing();
            ImGui.Separator();
            ImGui.Spacing();
            
            ImGui.TextUnformatted("Welcome to Windows 95!");
            ImGui.TextUnformatted("");
            ImGui.TextUnformatted("This simulated Windows 95 desktop provides");
            ImGui.TextUnformatted("an authentic retro computing experience.");
            ImGui.TextUnformatted("");
            ImGui.TextUnformatted("• Click desktop icons to open applications");
            ImGui.TextUnformatted("• Use the Start menu to access programs");
            ImGui.TextUnformatted("• Check the time in the system tray");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("Help");
        }
    }
}

// Run Dialog Window
public class RunWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    private string commandText = "";
    
    public RunWindow(Windows95Desktop desktop) 
        : base("Run", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Size = new Vector2(350, 150);
        SizeCondition = ImGuiCond.Always;
        
        // Center the window
        var viewport = ImGui.GetMainViewport();
        Position = new Vector2(
            viewport.Pos.X + (viewport.Size.X - Size.Value.X) / 2,
            viewport.Pos.Y + (viewport.Size.Y - Size.Value.Y) / 2
        );
        PositionCondition = ImGuiCond.Appearing;
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.TextUnformatted("Type the name of a program, folder, document, or");
        ImGui.TextUnformatted("Internet resource, and Windows will open it for you.");
        
        ImGui.Spacing();
        
        ImGui.TextUnformatted("Open:");
        Windows95Theme.BeginInsetFrame();
        if (ImGui.InputText("##Command", ref commandText, 256, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            ExecuteCommand();
        }
        Windows95Theme.EndInsetFrame();
        
        ImGui.Spacing();
        
        if (Windows95Theme.RaisedButton("OK"))
        {
            ExecuteCommand();
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Cancel"))
        {
            Desktop.CloseWindow("Run");
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Browse..."))
        {
            // Open file browser
        }
    }
    
    private void ExecuteCommand()
    {
        if (string.IsNullOrWhiteSpace(commandText))
            return;
            
        var command = commandText.ToLower().Trim();
        
        switch (command)
        {
            case "notepad":
                // Could open a notepad window
                break;
            case "calc":
                // Could open calculator
                break;
            case "mspaint":
                // Could open paint
                break;
            case "control":
                Desktop.OpenWindow("ControlPanel");
                break;
            default:
                // Show error dialog or just close
                break;
        }
        
        Desktop.CloseWindow("Run");
    }
}

// Plugin Configuration Window
public class PluginConfigWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    private Plugin Plugin;
    private Configuration Configuration;
    
    public PluginConfigWindow(Windows95Desktop desktop) 
        : base("FFXIV Plugin Configuration", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Plugin = desktop.GetPlugin();
        Configuration = Plugin.Configuration;
        Size = new Vector2(400, 300);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Plugin Settings"))
        {
            ImGui.TextUnformatted("FFXIV Plugin Configuration");
            ImGui.Spacing();
            
            // Plugin configuration options
            var configValue = Configuration.SomePropertyToBeSavedAndWithADefault;
            if (ImGui.Checkbox("Random Config Bool", ref configValue))
            {
                Configuration.SomePropertyToBeSavedAndWithADefault = configValue;
                Configuration.Save();
            }

            var movable = Configuration.IsConfigWindowMovable;
            if (ImGui.Checkbox("Movable Windows", ref movable))
            {
                Configuration.IsConfigWindowMovable = movable;
                Configuration.Save();
            }
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Theme Settings:");
            Plugin.ThemeManager.ShowThemeSelector();
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Plugin Version: 1.0.0");
            ImGui.TextUnformatted("Windows 95 Theme: Enabled");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        
        if (Windows95Theme.RaisedButton("Save Settings"))
        {
            Configuration.Save();
        }
        
        ImGui.SameLine();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("PluginConfig");
        }
    }
}

// Game Information Window
public class GameInfoWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    private Plugin Plugin;
    
    public GameInfoWindow(Windows95Desktop desktop) 
        : base("Final Fantasy XIV Information", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Plugin = desktop.GetPlugin();
        Size = new Vector2(500, 400);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("Game Status"))
        {
            var localPlayer = Plugin.ClientState.LocalPlayer;
            if (localPlayer == null)
            {
                ImGui.TextUnformatted("🔴 Not logged into FFXIV");
                ImGui.Spacing();
                ImGui.TextUnformatted("Please log into the game to view character information.");
            }
            else
            {
                ImGui.TextUnformatted("🟢 Connected to FFXIV");
                ImGui.Spacing();
                
                ImGui.TextUnformatted($"Character: {localPlayer.Name}");
                
                if (localPlayer.ClassJob.IsValid)
                {
                    ImGui.TextUnformatted($"Job: ({localPlayer.ClassJob.RowId}) \"{localPlayer.ClassJob.Value.Abbreviation.ExtractText()}\"");
                    ImGui.TextUnformatted($"Level: {localPlayer.Level}");
                }
                
                var territoryId = Plugin.ClientState.TerritoryType;
                if (Plugin.DataManager.GetExcelSheet<Lumina.Excel.Sheets.TerritoryType>().TryGetRow(territoryId, out var territoryRow))
                {
                    ImGui.TextUnformatted($"Location: \"{territoryRow.PlaceName.Value.Name.ExtractText()}\"");
                    ImGui.TextUnformatted($"Territory ID: {territoryId}");
                }
            }
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        
        if (Windows95Theme.BeginGroupBox("Plugin Mascot"))
        {
            ImGui.TextUnformatted("Have a retro goat in classic Windows 95 style:");
            ImGui.Spacing();
            
            var goatImage = Plugin.TextureProvider.GetFromFile(Desktop.GetGoatImagePath()).GetWrapOrDefault();
            if (goatImage != null)
            {
                ImGui.Image(goatImage.ImGuiHandle, new Vector2(goatImage.Width, goatImage.Height));
            }
            else
            {
                ImGui.TextUnformatted("🐐 Goat image not found");
            }
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("GameInfo");
        }
    }
}

// About Plugin Window
public class AboutPluginWindow : Window, IDisposable
{
    private Windows95Desktop Desktop;
    private Plugin Plugin;
    
    public AboutPluginWindow(Windows95Desktop desktop) 
        : base("About FFXIV Plugin", ImGuiWindowFlags.None)
    {
        Desktop = desktop;
        Plugin = desktop.GetPlugin();
        Size = new Vector2(450, 350);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("About This Plugin"))
        {
            ImGui.TextUnformatted("🖥️ FFXIV Windows 95 Plugin");
            ImGui.Spacing();
            
            ImGui.TextUnformatted("Version: 1.0.0");
            ImGui.TextUnformatted("Built: December 2024");
            ImGui.TextUnformatted("Framework: Dalamud");
            ImGui.Spacing();
            
            ImGui.TextUnformatted("This plugin provides an authentic Windows 95");
            ImGui.TextUnformatted("computing experience within Final Fantasy XIV.");
            ImGui.Spacing();
            
            ImGui.TextUnformatted("Features:");
            ImGui.BulletText("Complete CRT monitor simulation");
            ImGui.BulletText("Authentic Windows 95 desktop");
            ImGui.BulletText("Working Start menu and taskbar");
            ImGui.BulletText("Classic system windows");
            ImGui.BulletText("Retro visual effects and styling");
            ImGui.BulletText("FFXIV game integration");
            
            ImGui.Spacing();
            ImGui.TextUnformatted("Experience nostalgia while playing FFXIV!");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        
        if (Windows95Theme.BeginGroupBox("System Information"))
        {
            ImGui.TextUnformatted("Running on:");
            ImGui.BulletText($"Plugin: {Plugin.PluginInterface.Manifest.Name}");
            ImGui.BulletText("UI Framework: ImGui with Windows 95 theme");
            ImGui.BulletText("Monitor: Simulated CRT SVGA Display");
            ImGui.BulletText("Color Depth: 256 colors (simulated)");
            ImGui.BulletText("Resolution: 640x480 (in monitor)");
            
            Windows95Theme.EndGroupBox();
        }
        
        ImGui.Spacing();
        if (Windows95Theme.RaisedButton("Close"))
        {
            Desktop.CloseWindow("AboutPlugin");
        }
    }
} 