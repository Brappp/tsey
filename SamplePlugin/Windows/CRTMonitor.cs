using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class CRTMonitor : Window, IDisposable
{
    private Plugin Plugin;
    private Windows95Desktop Desktop;
    private readonly Vector4 MonitorBezelColor = new(0.15f, 0.15f, 0.15f, 1.0f); // Dark gray bezel
    private readonly Vector4 MonitorFrameColor = new(0.25f, 0.25f, 0.25f, 1.0f); // Lighter gray frame
    private readonly Vector4 ScreenGlowColor = new(0.8f, 0.9f, 1.0f, 0.1f); // Subtle blue glow
    private readonly Vector4 PowerLEDColor = new(0.0f, 1.0f, 0.0f, 1.0f); // Green power LED
    
    // Monitor specifications (typical 90s CRT)
    private readonly float BezelThickness = 40.0f;
    private readonly float ScreenInset = 8.0f;
    private readonly string MonitorModel = "SVGA Monitor 1995";
    
    public CRTMonitor(Plugin plugin, string goatImagePath) 
        : base("Windows 95 Computer##CRTMonitor", 
               ImGuiWindowFlags.NoTitleBar | 
               ImGuiWindowFlags.NoResize | 
               ImGuiWindowFlags.NoMove | 
               ImGuiWindowFlags.NoScrollbar | 
               ImGuiWindowFlags.NoScrollWithMouse |
               ImGuiWindowFlags.NoBringToFrontOnFocus)
    {
        Plugin = plugin;
        Desktop = new Windows95Desktop(plugin, goatImagePath);
        
        // Set monitor size (typical 15" CRT monitor resolution scaled for modern displays)
        var monitorSize = new Vector2(800, 600 + BezelThickness * 2 + 60); // Extra height for bottom bezel with controls
        Size = monitorSize;
        SizeCondition = ImGuiCond.Always;
        
        // Center the monitor on screen
        var viewport = ImGui.GetMainViewport();
        Position = new Vector2(
            viewport.Pos.X + (viewport.Size.X - monitorSize.X) / 2,
            viewport.Pos.Y + (viewport.Size.Y - monitorSize.Y) / 2
        );
        PositionCondition = ImGuiCond.FirstUseEver;
    }

    public void Dispose() 
    {
        Desktop?.Dispose();
    }

    public override void Draw()
    {
        var drawList = ImGui.GetWindowDrawList();
        var windowPos = ImGui.GetWindowPos();
        var windowSize = ImGui.GetWindowSize();
        
        // Draw monitor bezel/frame
        DrawMonitorFrame(drawList, windowPos, windowSize);
        
        // Calculate screen area (inside the bezel)
        var screenPos = new Vector2(
            windowPos.X + BezelThickness, 
            windowPos.Y + BezelThickness
        );
        var screenSize = new Vector2(
            windowSize.X - BezelThickness * 2,
            windowSize.Y - BezelThickness * 2 - 60 // Extra space for bottom controls
        );
        
        // Draw screen background (slight inset effect)
        DrawScreenBezel(drawList, screenPos, screenSize);
        
        // Set up clipping for the screen content
        var actualScreenPos = new Vector2(
            screenPos.X + ScreenInset,
            screenPos.Y + ScreenInset
        );
        var actualScreenSize = new Vector2(
            screenSize.X - ScreenInset * 2,
            screenSize.Y - ScreenInset * 2
        );
        
        // Draw screen glow effect
        DrawScreenGlow(drawList, actualScreenPos, actualScreenSize);
        
        // Draw Windows 95 desktop inside the screen area
        DrawDesktopContent(actualScreenPos, actualScreenSize);
        
        // Draw monitor controls and branding
        DrawMonitorControls(drawList, windowPos, windowSize);
        
        // Draw scanlines effect (subtle)
        DrawScanlines(drawList, actualScreenPos, actualScreenSize);
    }
    
    private void DrawMonitorFrame(ImDrawListPtr drawList, Vector2 windowPos, Vector2 windowSize)
    {
        // Outer monitor frame (plastic bezel)
        drawList.AddRectFilled(
            windowPos,
            new Vector2(windowPos.X + windowSize.X, windowPos.Y + windowSize.Y),
            ImGui.ColorConvertFloat4ToU32(MonitorFrameColor)
        );
        
        // Inner bezel (darker, inset look)
        var bezelInner = new Vector2(windowPos.X + 8, windowPos.Y + 8);
        var bezelSize = new Vector2(windowSize.X - 16, windowSize.Y - 16);
        
        drawList.AddRectFilled(
            bezelInner,
            new Vector2(bezelInner.X + bezelSize.X, bezelInner.Y + bezelSize.Y),
            ImGui.ColorConvertFloat4ToU32(MonitorBezelColor)
        );
        
        // Add some 3D effect to the bezel
        Windows95Theme.DrawBeveledRect(
            windowPos,
            new Vector2(windowPos.X + windowSize.X, windowPos.Y + windowSize.Y),
            true
        );
        
        // Inner bezel inset effect
        Windows95Theme.DrawBeveledRect(
            bezelInner,
            new Vector2(bezelInner.X + bezelSize.X, bezelInner.Y + bezelSize.Y),
            false
        );
    }
    
    private void DrawScreenBezel(ImDrawListPtr drawList, Vector2 screenPos, Vector2 screenSize)
    {
        // Screen bezel (inset effect around the actual display)
        var bezelColor = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
        
        drawList.AddRectFilled(
            screenPos,
            new Vector2(screenPos.X + screenSize.X, screenPos.Y + screenSize.Y),
            ImGui.ColorConvertFloat4ToU32(bezelColor)
        );
        
        // Inset effect for screen
        Windows95Theme.DrawBeveledRect(
            screenPos,
            new Vector2(screenPos.X + screenSize.X, screenPos.Y + screenSize.Y),
            false
        );
    }
    
    private void DrawScreenGlow(ImDrawListPtr drawList, Vector2 screenPos, Vector2 screenSize)
    {
        // Subtle screen glow effect (CRT monitors had a slight glow)
        var glowSize = 4.0f;
        var glowPos = new Vector2(screenPos.X - glowSize, screenPos.Y - glowSize);
        var glowEnd = new Vector2(
            screenPos.X + screenSize.X + glowSize,
            screenPos.Y + screenSize.Y + glowSize
        );
        
        // Very subtle glow
        drawList.AddRectFilled(
            glowPos, glowEnd,
            ImGui.ColorConvertFloat4ToU32(ScreenGlowColor)
        );
    }
    
    private void DrawDesktopContent(Vector2 screenPos, Vector2 screenSize)
    {
        // Set up a child window for the desktop content
        ImGui.SetCursorScreenPos(screenPos);
        
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, Vector2.Zero);
        
        if (ImGui.BeginChild("MonitorScreen", screenSize, false, 
            ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
        {
            // Draw the Windows 95 desktop content
            Desktop.Draw();
        }
        ImGui.EndChild();
        
        ImGui.PopStyleVar(2);
    }
    
    private void DrawMonitorControls(ImDrawListPtr drawList, Vector2 windowPos, Vector2 windowSize)
    {
        // Bottom control panel area
        var controlsY = windowPos.Y + windowSize.Y - 50;
        
        // Control panel background
        drawList.AddRectFilled(
            new Vector2(windowPos.X + 8, controlsY),
            new Vector2(windowPos.X + windowSize.X - 8, windowPos.Y + windowSize.Y - 8),
            ImGui.ColorConvertFloat4ToU32(MonitorFrameColor)
        );
        
        // Power LED
        var ledPos = new Vector2(windowPos.X + 20, controlsY + 15);
        drawList.AddCircleFilled(ledPos, 4, ImGui.ColorConvertFloat4ToU32(PowerLEDColor));
        
        // Monitor model text
        var modelTextPos = new Vector2(windowPos.X + 40, controlsY + 10);
        drawList.AddText(modelTextPos, ImGui.ColorConvertFloat4ToU32(new Vector4(0.7f, 0.7f, 0.7f, 1.0f)), MonitorModel);
        
        // Control knobs (simplified as small circles)
        var knobY = controlsY + 20;
        var knobSpacing = 30;
        var startX = windowPos.X + windowSize.X - 150;
        
        // Brightness knob
        drawList.AddCircle(new Vector2(startX, knobY), 8, ImGui.ColorConvertFloat4ToU32(new Vector4(0.4f, 0.4f, 0.4f, 1.0f)), 12, 2);
        drawList.AddText(new Vector2(startX - 10, knobY + 12), ImGui.ColorConvertFloat4ToU32(new Vector4(0.6f, 0.6f, 0.6f, 1.0f)), "BRIGHT");
        
        // Contrast knob
        drawList.AddCircle(new Vector2(startX + knobSpacing, knobY), 8, ImGui.ColorConvertFloat4ToU32(new Vector4(0.4f, 0.4f, 0.4f, 1.0f)), 12, 2);
        drawList.AddText(new Vector2(startX + knobSpacing - 12, knobY + 12), ImGui.ColorConvertFloat4ToU32(new Vector4(0.6f, 0.6f, 0.6f, 1.0f)), "CONTRAST");
        
        // Power button
        var powerButtonPos = new Vector2(startX + knobSpacing * 2, knobY);
        Windows95Theme.DrawBeveledRect(
            new Vector2(powerButtonPos.X - 10, powerButtonPos.Y - 8),
            new Vector2(powerButtonPos.X + 10, powerButtonPos.Y + 8),
            true
        );
        drawList.AddText(new Vector2(powerButtonPos.X - 8, powerButtonPos.Y + 12), ImGui.ColorConvertFloat4ToU32(new Vector4(0.6f, 0.6f, 0.6f, 1.0f)), "POWER");
        
        // Brand logo area (right side)
        var logoPos = new Vector2(windowPos.X + windowSize.X - 80, controlsY + 5);
        drawList.AddText(logoPos, ImGui.ColorConvertFloat4ToU32(new Vector4(0.8f, 0.8f, 0.8f, 1.0f)), "COMPAQ");
        drawList.AddText(new Vector2(logoPos.X, logoPos.Y + 12), ImGui.ColorConvertFloat4ToU32(new Vector4(0.5f, 0.5f, 0.5f, 1.0f)), "SVGA");
    }
    
    private void DrawScanlines(ImDrawListPtr drawList, Vector2 screenPos, Vector2 screenSize)
    {
        // Subtle scanlines effect (every 4th line)
        var scanlineColor = ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.03f));
        
        for (float y = screenPos.Y; y < screenPos.Y + screenSize.Y; y += 4)
        {
            drawList.AddLine(
                new Vector2(screenPos.X, y),
                new Vector2(screenPos.X + screenSize.X, y),
                scanlineColor
            );
        }
    }
    
    // Public method to access the desktop for plugin integration
    public Windows95Desktop GetDesktop() => Desktop;
} 