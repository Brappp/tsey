using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class Windows95Desktop : Window, IDisposable
{
    private Plugin Plugin;
    private string GoatImagePath;
    private bool showStartMenu = false;
    private List<DesktopIcon> desktopIcons;
    private List<TaskbarItem> taskbarItems;
    private Dictionary<string, Window> openWindows;
    private DateTime currentTime = DateTime.Now;
    
    public Windows95Desktop(Plugin plugin, string goatImagePath) 
        : base("Windows 95 Desktop##Desktop", 
               ImGuiWindowFlags.NoTitleBar | 
               ImGuiWindowFlags.NoResize | 
               ImGuiWindowFlags.NoMove | 
               ImGuiWindowFlags.NoScrollbar | 
               ImGuiWindowFlags.NoScrollWithMouse |
               ImGuiWindowFlags.NoBringToFrontOnFocus |
               ImGuiWindowFlags.NoNavFocus)
    {
        Plugin = plugin;
        GoatImagePath = goatImagePath;
        
        // Make it fullscreen-like
        Position = Vector2.Zero;
        PositionCondition = ImGuiCond.Always;
        
        // Initialize desktop icons
        desktopIcons = new List<DesktopIcon>
        {
            new DesktopIcon("My Computer", new Vector2(50, 50), () => OpenWindow("MyComputer")),
            new DesktopIcon("Network\nNeighborhood", new Vector2(50, 140), () => OpenWindow("NetworkNeighborhood")),
            new DesktopIcon("Recycle Bin", new Vector2(50, 230), () => OpenWindow("RecycleBin")),
            new DesktopIcon("My Documents", new Vector2(50, 320), () => OpenWindow("MyDocuments")),
            new DesktopIcon("FFXIV Plugin\nConfig", new Vector2(50, 410), () => OpenWindow("PluginConfig")),
            new DesktopIcon("Control Panel", new Vector2(50, 500), () => OpenWindow("ControlPanel"))
        };
        
        taskbarItems = new List<TaskbarItem>();
        openWindows = new Dictionary<string, Window>();
    }

    public void Dispose() 
    {
        foreach (var window in openWindows.Values)
        {
            if (window is IDisposable disposable)
                disposable.Dispose();
        }
    }

    public override void PreDraw()
    {
        // Update window size to match viewport
        var viewport = ImGui.GetMainViewport();
        Size = viewport.Size;
        Position = viewport.Pos;
        
        currentTime = DateTime.Now;
    }

    public override void Draw()
    {
        // Draw desktop background
        Windows95Theme.DrawDesktopBackground();
        
        // Draw desktop icons
        DrawDesktopIcons();
        
        // Draw taskbar
        DrawTaskbar();
        
        // Draw Start menu if open
        if (showStartMenu)
        {
            DrawStartMenu();
        }
    }
    
    private void DrawDesktopIcons()
    {
        foreach (var icon in desktopIcons)
        {
            ImGui.SetCursorScreenPos(icon.Position);
            
            // Icon background for selection
            var iconSize = new Vector2(80, 80);
            var isHovered = ImGui.IsMouseHoveringRect(icon.Position, icon.Position + iconSize);
            
            if (isHovered && ImGui.IsMouseClicked(ImGuiMouseButton.Left))
            {
                icon.OnClick?.Invoke();
            }
            
            // Draw icon "button"
            ImGui.PushID($"icon_{icon.Name}");
            ImGui.SetCursorScreenPos(icon.Position);
            
            // Icon appearance
            var buttonColor = isHovered ? Windows95Theme.ButtonHovered : new Vector4(0, 0, 0, 0);
            ImGui.PushStyleColor(ImGuiCol.Button, buttonColor);
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, Windows95Theme.ButtonHovered);
            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(8, 8));
            
            if (ImGui.Button($"📁\n{icon.Name}", iconSize))
            {
                icon.OnClick?.Invoke();
            }
            
            ImGui.PopStyleVar();
            ImGui.PopStyleColor(2);
            ImGui.PopID();
        }
    }
    
    private void DrawTaskbar()
    {
        var viewport = ImGui.GetMainViewport();
        var taskbarHeight = 30.0f;
        var taskbarPos = new Vector2(viewport.Pos.X, viewport.Pos.Y + viewport.Size.Y - taskbarHeight);
        var taskbarSize = new Vector2(viewport.Size.X, taskbarHeight);
        
        // Draw taskbar background
        var drawList = ImGui.GetWindowDrawList();
        Windows95Theme.DrawBeveledRect(taskbarPos, taskbarPos + taskbarSize, true);
        
        // Start button
        ImGui.SetCursorScreenPos(new Vector2(taskbarPos.X + 2, taskbarPos.Y + 2));
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(8, 4));
        
        if (Windows95Theme.RaisedButton("Start", new Vector2(60, 26)))
        {
            showStartMenu = !showStartMenu;
        }
        
        ImGui.PopStyleVar();
        
        // Taskbar items (open windows)
        var currentX = taskbarPos.X + 70;
        foreach (var item in taskbarItems)
        {
            ImGui.SetCursorScreenPos(new Vector2(currentX, taskbarPos.Y + 2));
            
            var isActive = item.IsActive;
            var buttonColor = isActive ? Windows95Theme.ButtonActive : Windows95Theme.ButtonFace;
            
            ImGui.PushStyleColor(ImGuiCol.Button, buttonColor);
            if (ImGui.Button(item.Title, new Vector2(120, 26)))
            {
                item.OnClick?.Invoke();
            }
            ImGui.PopStyleColor();
            
            currentX += 125;
        }
        
        // System tray area (clock)
        var clockText = currentTime.ToString("HH:mm");
        var clockSize = ImGui.CalcTextSize(clockText);
        var clockPos = new Vector2(taskbarPos.X + taskbarSize.X - clockSize.X - 10, taskbarPos.Y + 6);
        
        // Clock background
        Windows95Theme.DrawBeveledRect(
            new Vector2(clockPos.X - 5, clockPos.Y - 2),
            new Vector2(clockPos.X + clockSize.X + 5, clockPos.Y + clockSize.Y + 2),
            false // inset
        );
        
        drawList.AddText(clockPos, ImGui.ColorConvertFloat4ToU32(Windows95Theme.WindowText), clockText);
    }
    
    private void DrawStartMenu()
    {
        var viewport = ImGui.GetMainViewport();
        var menuWidth = 200.0f;
        var menuHeight = 300.0f;
        var taskbarHeight = 30.0f;
        
        var menuPos = new Vector2(
            viewport.Pos.X + 2,
            viewport.Pos.Y + viewport.Size.Y - taskbarHeight - menuHeight
        );
        
        ImGui.SetNextWindowPos(menuPos);
        ImGui.SetNextWindowSize(new Vector2(menuWidth, menuHeight));
        
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 2));
        
        if (ImGui.Begin("StartMenu", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove))
        {
            // Start menu header with gradient
            var drawList = ImGui.GetWindowDrawList();
            var headerHeight = 50.0f;
            var headerMin = ImGui.GetWindowPos();
            var headerMax = new Vector2(headerMin.X + menuWidth, headerMin.Y + headerHeight);
            
            // Gradient background (simplified)
            drawList.AddRectFilledMultiColor(
                headerMin, headerMax,
                ImGui.ColorConvertFloat4ToU32(new Vector4(0.0f, 0.0f, 0.5f, 1.0f)), // Dark blue
                ImGui.ColorConvertFloat4ToU32(new Vector4(0.0f, 0.0f, 0.3f, 1.0f)), // Darker blue
                ImGui.ColorConvertFloat4ToU32(new Vector4(0.0f, 0.0f, 0.3f, 1.0f)), // Darker blue
                ImGui.ColorConvertFloat4ToU32(new Vector4(0.0f, 0.0f, 0.5f, 1.0f))  // Dark blue
            );
            
            // "Windows 95" text
            ImGui.SetCursorPosY(15);
            ImGui.SetCursorPosX(10);
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1, 1, 1, 1));
            ImGui.TextUnformatted("Windows 95");
            ImGui.PopStyleColor();
            
            ImGui.SetCursorPosY(headerHeight + 5);
            
            // Menu items
            DrawStartMenuItem("Programs", "📁", () => { /* Submenu */ });
            DrawStartMenuItem("Documents", "📄", () => { /* Open documents */ });
            DrawStartMenuItem("Settings", "⚙️", () => OpenWindow("Settings"));
            DrawStartMenuItem("Find", "🔍", () => { /* Find dialog */ });
            DrawStartMenuItem("Help", "❓", () => OpenWindow("Help"));
            DrawStartMenuItem("Run...", "🏃", () => OpenWindow("Run"));
            
            ImGui.Separator();
            
            DrawStartMenuItem("FFXIV Plugin Config", "🎮", () => OpenWindow("PluginConfig"));
            DrawStartMenuItem("Plugin Game Info", "⚔️", () => OpenWindow("GameInfo"));
            DrawStartMenuItem("Plugin About", "ℹ️", () => OpenWindow("AboutPlugin"));
            
            ImGui.Separator();
            
            DrawStartMenuItem("Shut Down...", "🔌", () => {
                showStartMenu = false;
                // Could show shutdown dialog
            });
        }
        ImGui.End();
        
        ImGui.PopStyleVar(2);
        
        // Close start menu if clicked outside
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Left) && !ImGui.IsWindowHovered())
        {
            showStartMenu = false;
        }
    }
    
    private void DrawStartMenuItem(string text, string icon, Action onClick)
    {
        var fullText = $"{icon}  {text}";
        
        if (ImGui.Selectable(fullText, false, ImGuiSelectableFlags.None, new Vector2(0, 25)))
        {
            onClick?.Invoke();
            showStartMenu = false;
        }
        
        if (ImGui.IsItemHovered())
        {
            ImGui.SetMouseCursor(ImGuiMouseCursor.Hand);
        }
    }
    
    public void OpenWindow(string windowType)
    {
        if (openWindows.ContainsKey(windowType))
        {
            // Focus existing window
            return;
        }
        
        Window? newWindow = windowType switch
        {
            "MyComputer" => new MyComputerWindow(this),
            "NetworkNeighborhood" => new NetworkNeighborhoodWindow(this),
            "RecycleBin" => new RecycleBinWindow(this),
            "MyDocuments" => new MyDocumentsWindow(this),
            "ControlPanel" => new ControlPanelWindow(this),
            "Settings" => new SettingsWindow(this),
            "Help" => new HelpWindow(this),
            "Run" => new RunWindow(this),
            "PluginConfig" => new PluginConfigWindow(this),
            "GameInfo" => new GameInfoWindow(this),
            "AboutPlugin" => new AboutPluginWindow(this),
            _ => null
        };
        
        if (newWindow != null)
        {
            openWindows[windowType] = newWindow;
            Plugin.WindowSystem.AddWindow(newWindow);
            
            // Add to taskbar
            taskbarItems.Add(new TaskbarItem
            {
                Title = GetWindowTitle(windowType),
                IsActive = false,
                OnClick = () => { /* Focus window */ }
            });
        }
    }
    
    public void CloseWindow(string windowType)
    {
        if (openWindows.TryGetValue(windowType, out var window))
        {
            Plugin.WindowSystem.RemoveWindow(window);
            if (window is IDisposable disposable)
                disposable.Dispose();
            openWindows.Remove(windowType);
            
            // Remove from taskbar
            taskbarItems.RemoveAll(item => item.Title == GetWindowTitle(windowType));
        }
    }
    
    private string GetWindowTitle(string windowType) => windowType switch
    {
        "MyComputer" => "My Computer",
        "NetworkNeighborhood" => "Network Neighborhood",
        "RecycleBin" => "Recycle Bin",
        "MyDocuments" => "My Documents",
        "ControlPanel" => "Control Panel",
        "Settings" => "Settings",
        "Help" => "Help",
        "Run" => "Run",
        "PluginConfig" => "FFXIV Plugin Config",
        "GameInfo" => "Game Information",
        "AboutPlugin" => "About Plugin",
        _ => windowType
    };
    
    // Public method to access plugin and goat image path
    public Plugin GetPlugin() => Plugin;
    public string GetGoatImagePath() => GoatImagePath;
}

public class DesktopIcon
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Action OnClick { get; set; }
    
    public DesktopIcon(string name, Vector2 position, Action onClick)
    {
        Name = name;
        Position = position;
        OnClick = onClick;
    }
}

public class TaskbarItem
{
    public string Title { get; set; } = "";
    public bool IsActive { get; set; }
    public Action? OnClick { get; set; }
} 