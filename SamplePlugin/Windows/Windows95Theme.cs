using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;

namespace SamplePlugin.Windows;

public static class Windows95Theme
{
    // Windows 95 Core Color Palette (exact hex values from the guide)
    public static readonly Vector4 ButtonFace = new(0.75f, 0.75f, 0.75f, 1.0f);        // #C0C0C0
    public static readonly Vector4 DesktopTeal = new(0.0f, 0.5f, 0.5f, 1.0f);          // #008080
    public static readonly Vector4 WindowText = new(0.0f, 0.0f, 0.0f, 1.0f);           // #000000
    public static readonly Vector4 WindowBackground = new(0.99f, 1.0f, 1.0f, 1.0f);    // #FDFFFF
    public static readonly Vector4 Highlight = new(1.0f, 1.0f, 1.0f, 1.0f);            // #FFFFFF
    public static readonly Vector4 LightGray = new(0.76f, 0.76f, 0.76f, 1.0f);         // #C3C3C3
    public static readonly Vector4 MediumGray = new(0.51f, 0.51f, 0.51f, 1.0f);        // #818181
    public static readonly Vector4 DarkShadow = new(0.5f, 0.5f, 0.5f, 1.0f);           // #808080
    public static readonly Vector4 ActiveTitleBar = new(0.0f, 0.0f, 0.5f, 1.0f);       // #000080
    public static readonly Vector4 InactiveTitleBar = new(0.5f, 0.5f, 0.5f, 1.0f);     // #808080
    
    // Button state colors
    public static readonly Vector4 ButtonHovered = new(0.85f, 0.85f, 0.85f, 1.0f);
    public static readonly Vector4 ButtonActive = new(0.65f, 0.65f, 0.65f, 1.0f);
    
    private static bool _isApplied = false;
    private static ImGuiStylePtr _originalStyle;
    
    /// <summary>
    /// Applies the complete Windows 95 theme to ImGui
    /// </summary>
    public static void ApplyTheme()
    {
        if (_isApplied) return;
        
        var style = ImGui.GetStyle();
        
        // Store original style for restoration if needed
        _originalStyle = style;
        
        // Disable all rounding for sharp, classic look
        style.WindowRounding = 0.0f;
        style.ChildRounding = 0.0f;
        style.FrameRounding = 0.0f;
        style.PopupRounding = 0.0f;
        style.ScrollbarRounding = 0.0f;
        style.GrabRounding = 0.0f;
        style.TabRounding = 0.0f;
        
        // Classic Windows 95 spacing
        style.WindowPadding = new Vector2(4, 4);
        style.FramePadding = new Vector2(4, 2);
        style.ItemSpacing = new Vector2(8, 4);
        style.ItemInnerSpacing = new Vector2(4, 4);
        style.TouchExtraPadding = new Vector2(0, 0);
        style.IndentSpacing = 20.0f;
        style.ScrollbarSize = 16.0f;
        style.GrabMinSize = 8.0f;
        
        // Border sizes for 3D effect
        style.WindowBorderSize = 1.0f;
        style.ChildBorderSize = 1.0f;
        style.PopupBorderSize = 1.0f;
        style.FrameBorderSize = 1.0f;
        style.TabBorderSize = 0.0f;
        
        // Apply Windows 95 color scheme
        ApplyColorScheme(style);
        
        _isApplied = true;
    }
    
    /// <summary>
    /// Applies the Windows 95 color scheme to ImGui style
    /// </summary>
    private static void ApplyColorScheme(ImGuiStylePtr style)
    {
        // Window colors
        style.Colors[(int)ImGuiCol.WindowBg] = ButtonFace;
        style.Colors[(int)ImGuiCol.ChildBg] = ButtonFace;
        style.Colors[(int)ImGuiCol.PopupBg] = ButtonFace;
        
        // Title bar colors
        style.Colors[(int)ImGuiCol.TitleBg] = InactiveTitleBar;
        style.Colors[(int)ImGuiCol.TitleBgActive] = ActiveTitleBar;
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = InactiveTitleBar;
        
        // Menu colors
        style.Colors[(int)ImGuiCol.MenuBarBg] = ButtonFace;
        
        // Button colors with 3D effect simulation
        style.Colors[(int)ImGuiCol.Button] = ButtonFace;
        style.Colors[(int)ImGuiCol.ButtonHovered] = ButtonHovered;
        style.Colors[(int)ImGuiCol.ButtonActive] = ButtonActive;
        
        // Frame colors (input fields, etc.)
        style.Colors[(int)ImGuiCol.FrameBg] = WindowBackground;
        style.Colors[(int)ImGuiCol.FrameBgHovered] = WindowBackground;
        style.Colors[(int)ImGuiCol.FrameBgActive] = WindowBackground;
        
        // Checkbox and radio button colors
        style.Colors[(int)ImGuiCol.CheckMark] = WindowText;
        
        // Slider colors
        style.Colors[(int)ImGuiCol.SliderGrab] = MediumGray;
        style.Colors[(int)ImGuiCol.SliderGrabActive] = DarkShadow;
        
        // Scrollbar colors
        style.Colors[(int)ImGuiCol.ScrollbarBg] = ButtonFace;
        style.Colors[(int)ImGuiCol.ScrollbarGrab] = MediumGray;
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = DarkShadow;
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = WindowText;
        
        // Header colors (collapsing headers, selectables)
        style.Colors[(int)ImGuiCol.Header] = LightGray;
        style.Colors[(int)ImGuiCol.HeaderHovered] = ButtonHovered;
        style.Colors[(int)ImGuiCol.HeaderActive] = ButtonActive;
        
        // Separator colors
        style.Colors[(int)ImGuiCol.Separator] = MediumGray;
        style.Colors[(int)ImGuiCol.SeparatorHovered] = DarkShadow;
        style.Colors[(int)ImGuiCol.SeparatorActive] = WindowText;
        
        // Text colors
        style.Colors[(int)ImGuiCol.Text] = WindowText;
        style.Colors[(int)ImGuiCol.TextDisabled] = MediumGray;
        style.Colors[(int)ImGuiCol.TextSelectedBg] = ActiveTitleBar;
        
        // Border colors
        style.Colors[(int)ImGuiCol.Border] = MediumGray;
        style.Colors[(int)ImGuiCol.BorderShadow] = DarkShadow;
        
        // Tab colors
        style.Colors[(int)ImGuiCol.Tab] = ButtonFace;
        style.Colors[(int)ImGuiCol.TabHovered] = ButtonHovered;
        style.Colors[(int)ImGuiCol.TabActive] = WindowBackground;
        style.Colors[(int)ImGuiCol.TabUnfocused] = ButtonFace;
        style.Colors[(int)ImGuiCol.TabUnfocusedActive] = LightGray;
        
        // Table colors
        style.Colors[(int)ImGuiCol.TableHeaderBg] = ButtonFace;
        style.Colors[(int)ImGuiCol.TableBorderStrong] = MediumGray;
        style.Colors[(int)ImGuiCol.TableBorderLight] = LightGray;
        style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0, 0, 0, 0);
        style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1, 1, 1, 0.03f);
        
        // Drag and drop colors
        style.Colors[(int)ImGuiCol.DragDropTarget] = ActiveTitleBar;
        
        // Navigation colors
        style.Colors[(int)ImGuiCol.NavHighlight] = ActiveTitleBar;
        style.Colors[(int)ImGuiCol.NavWindowingHighlight] = ActiveTitleBar;
        style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.8f, 0.8f, 0.9f, 0.2f);
        
        // Modal background
        style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.35f);
    }
    
    /// <summary>
    /// Creates a Windows 95-style raised button with proper 3D effect
    /// </summary>
    public static bool RaisedButton(string label, Vector2 size = default)
    {
        ImGui.PushStyleColor(ImGuiCol.Button, ButtonFace);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ButtonHovered);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, ButtonActive);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);
        
        bool result = ImGui.Button(label, size);
        
        ImGui.PopStyleVar(1);
        ImGui.PopStyleColor(3);
        
        return result;
    }
    
    /// <summary>
    /// Creates a Windows 95-style inset frame (for input fields)
    /// </summary>
    public static void BeginInsetFrame()
    {
        ImGui.PushStyleColor(ImGuiCol.FrameBg, WindowBackground);
        ImGui.PushStyleColor(ImGuiCol.Border, MediumGray);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);
    }
    
    /// <summary>
    /// Ends the inset frame styling
    /// </summary>
    public static void EndInsetFrame()
    {
        ImGui.PopStyleVar(1);
        ImGui.PopStyleColor(2);
    }
    
    /// <summary>
    /// Draws a Windows 95-style desktop background
    /// </summary>
    public static void DrawDesktopBackground()
    {
        var drawList = ImGui.GetWindowDrawList();
        var canvasPos = ImGui.GetCursorScreenPos();
        var canvasSize = ImGui.GetContentRegionAvail();
        
        // Classic teal desktop background
        drawList.AddRectFilled(
            canvasPos,
            new Vector2(canvasPos.X + canvasSize.X, canvasPos.Y + canvasSize.Y),
            ImGui.ColorConvertFloat4ToU32(DesktopTeal)
        );
    }
    
    /// <summary>
    /// Draws a 3D beveled rectangle (for custom UI elements)
    /// </summary>
    public static void DrawBeveledRect(Vector2 topLeft, Vector2 bottomRight, bool raised = true)
    {
        var drawList = ImGui.GetWindowDrawList();
        
        // Fill the rectangle with button face color
        drawList.AddRectFilled(topLeft, bottomRight, ImGui.ColorConvertFloat4ToU32(ButtonFace));
        
        if (raised)
        {
            // Light edges (top and left)
            drawList.AddLine(topLeft, new Vector2(bottomRight.X, topLeft.Y), ImGui.ColorConvertFloat4ToU32(Highlight));
            drawList.AddLine(topLeft, new Vector2(topLeft.X, bottomRight.Y), ImGui.ColorConvertFloat4ToU32(Highlight));
            
            // Dark edges (bottom and right)
            drawList.AddLine(new Vector2(topLeft.X, bottomRight.Y), bottomRight, ImGui.ColorConvertFloat4ToU32(DarkShadow));
            drawList.AddLine(new Vector2(bottomRight.X, topLeft.Y), bottomRight, ImGui.ColorConvertFloat4ToU32(DarkShadow));
        }
        else
        {
            // Inverted for inset appearance
            drawList.AddLine(topLeft, new Vector2(bottomRight.X, topLeft.Y), ImGui.ColorConvertFloat4ToU32(DarkShadow));
            drawList.AddLine(topLeft, new Vector2(topLeft.X, bottomRight.Y), ImGui.ColorConvertFloat4ToU32(DarkShadow));
            
            drawList.AddLine(new Vector2(topLeft.X, bottomRight.Y), bottomRight, ImGui.ColorConvertFloat4ToU32(Highlight));
            drawList.AddLine(new Vector2(bottomRight.X, topLeft.Y), bottomRight, ImGui.ColorConvertFloat4ToU32(Highlight));
        }
    }
    
    /// <summary>
    /// Creates a Windows 95-style group box with title
    /// </summary>
    public static bool BeginGroupBox(string title, Vector2 size = default)
    {
        var style = ImGui.GetStyle();
        var drawList = ImGui.GetWindowDrawList();
        var pos = ImGui.GetCursorScreenPos();
        
        // Calculate title size
        var titleSize = ImGui.CalcTextSize(title);
        var frameHeight = titleSize.Y + style.FramePadding.Y * 2;
        
        // Draw the group box frame
        var frameMin = new Vector2(pos.X, pos.Y + frameHeight * 0.5f);
        var frameMax = size.X > 0 ? new Vector2(pos.X + size.X, pos.Y + size.Y) : 
                      new Vector2(pos.X + ImGui.GetContentRegionAvail().X, pos.Y + ImGui.GetContentRegionAvail().Y);
        
        // Draw inset frame
        drawList.AddRect(frameMin, frameMax, ImGui.ColorConvertFloat4ToU32(MediumGray));
        drawList.AddRect(
            new Vector2(frameMin.X + 1, frameMin.Y + 1), 
            new Vector2(frameMax.X - 1, frameMax.Y - 1), 
            ImGui.ColorConvertFloat4ToU32(Highlight)
        );
        
        // Draw title background
        var titleBg = new Vector2(pos.X + 8, pos.Y);
        var titleBgEnd = new Vector2(titleBg.X + titleSize.X + 4, titleBg.Y + titleSize.Y);
        drawList.AddRectFilled(titleBg, titleBgEnd, ImGui.ColorConvertFloat4ToU32(ButtonFace));
        
        // Draw title text
        drawList.AddText(new Vector2(titleBg.X + 2, titleBg.Y), ImGui.ColorConvertFloat4ToU32(WindowText), title);
        
        // Set cursor position for content
        ImGui.SetCursorScreenPos(new Vector2(frameMin.X + style.FramePadding.X, frameMin.Y + style.FramePadding.Y));
        
        return true;
    }
    
    /// <summary>
    /// Ends the group box
    /// </summary>
    public static void EndGroupBox()
    {
        // Nothing special needed, just for symmetry
    }
    
    /// <summary>
    /// Resets ImGui style to default
    /// </summary>
    public static void ResetTheme()
    {
        if (!_isApplied) return;
        
        // This would require storing the original style, which is complex
        // For now, just mark as not applied
        _isApplied = false;
    }
    
    /// <summary>
    /// Generates a Windows 95 variant with hue shift
    /// </summary>
    public static void ApplyWindows95Variant(float hueShift)
    {
        ApplyTheme();
        
        var style = ImGui.GetStyle();
        
        // Convert base Windows 95 gray to HSV and adjust
        ImGui.ColorConvertRGBtoHSV(ButtonFace.X, ButtonFace.Y, ButtonFace.Z, out float h, out float s, out float v);
        h += hueShift;
        
        ImGui.ColorConvertHSVtoRGB(h, s * 0.1f, v, out float r, out float g, out float b);
        var newBaseColor = new Vector4(r, g, b, 1.0f);
        
        style.Colors[(int)ImGuiCol.WindowBg] = newBaseColor;
        style.Colors[(int)ImGuiCol.ChildBg] = newBaseColor;
        style.Colors[(int)ImGuiCol.Button] = newBaseColor;
        style.Colors[(int)ImGuiCol.MenuBarBg] = newBaseColor;
    }
}

/// <summary>
/// Theme manager for switching between different retro themes
/// </summary>
public class RetroThemeManager
{
    private readonly Dictionary<string, Action> _themes;
    private string _currentTheme = "Windows 95";
    
    public string CurrentTheme => _currentTheme;
    
    public RetroThemeManager()
    {
        _themes = new Dictionary<string, Action>
        {
            ["Windows 95"] = Windows95Theme.ApplyTheme,
            ["Windows 95 Blue"] = () => Windows95Theme.ApplyWindows95Variant(0.6f),
            ["Windows 95 Green"] = () => Windows95Theme.ApplyWindows95Variant(0.3f),
            ["Windows 95 Purple"] = () => Windows95Theme.ApplyWindows95Variant(0.8f),
            ["Default"] = () => { /* Default ImGui theme */ }
        };
    }
    
    public void ApplyTheme(string themeName)
    {
        if (_themes.TryGetValue(themeName, out var themeAction))
        {
            themeAction();
            _currentTheme = themeName;
        }
    }
    
    public void ShowThemeSelector()
    {
        if (ImGui.BeginCombo("Theme", _currentTheme))
        {
            foreach (var theme in _themes.Keys)
            {
                bool isSelected = (_currentTheme == theme);
                if (ImGui.Selectable(theme, isSelected))
                {
                    ApplyTheme(theme);
                }
                
                if (isSelected)
                {
                    ImGui.SetItemDefaultFocus();
                }
            }
            ImGui.EndCombo();
        }
    }
    
    public IEnumerable<string> GetAvailableThemes()
    {
        return _themes.Keys;
    }
} 