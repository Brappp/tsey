# Windows 95 UI Theme for Dalamud Plugin

This implementation provides a complete Windows 95 user interface theme for ImGui-based Dalamud plugins, featuring authentic colors, typography, and visual effects that recreate the classic desktop computing experience.

## Features

### 🎨 Authentic Color Palette
- **Button Face**: `#C0C0C0` - The signature light gray for buttons and dialogs
- **Desktop Teal**: `#008080` - The iconic turquoise desktop background
- **Active Title Bar**: `#000080` - Navy blue for active window titles
- **Window Text**: `#000000` - Black text for maximum readability
- **3D Effects**: White highlights and gray shadows for beveled appearance

### 🖼️ Visual Design Elements
- **Sharp Corners**: Zero border radius for authentic blocky appearance
- **3D Beveled Effects**: Upper-left light source simulation
- **Classic Spacing**: Period-appropriate padding and margins
- **Inset Input Fields**: Recessed appearance for text inputs
- **Raised Buttons**: Elevated appearance with proper hover states

### 🎯 Component Library
- `Windows95Theme.ApplyTheme()` - Apply complete theme
- `Windows95Theme.RaisedButton()` - Classic 3D buttons
- `Windows95Theme.BeginInsetFrame()` / `EndInsetFrame()` - Input field styling
- `Windows95Theme.DrawDesktopBackground()` - Teal desktop background
- `Windows95Theme.BeginGroupBox()` / `EndGroupBox()` - Framed sections
- `Windows95Theme.DrawBeveledRect()` - Custom 3D rectangles

## Usage

### Basic Theme Application

```csharp
// Apply Windows 95 theme globally
Windows95Theme.ApplyTheme();

// Or use the theme manager for multiple themes
var themeManager = new RetroThemeManager();
themeManager.ApplyTheme("Windows 95");
```

### Creating Windows 95 Style Buttons

```csharp
// Classic raised button with 3D effect
if (Windows95Theme.RaisedButton("Click Me!"))
{
    // Button clicked
}

// Sized button
if (Windows95Theme.RaisedButton("Wide Button", new Vector2(120, 30)))
{
    // Button clicked
}
```

### Input Field Styling

```csharp
// Classic inset input field
Windows95Theme.BeginInsetFrame();
ImGui.InputText("Username", ref username, 256);
Windows95Theme.EndInsetFrame();
```

### Group Boxes

```csharp
// Windows 95 style group box with title
if (Windows95Theme.BeginGroupBox("Settings"))
{
    ImGui.TextUnformatted("Group content here");
    Windows95Theme.EndGroupBox();
}
```

### Desktop Background

```csharp
// Draw the iconic teal desktop background
Windows95Theme.DrawDesktopBackground();
```

## Theme Variants

The theme manager supports multiple Windows 95 variants:

- **Windows 95** - Classic gray theme
- **Windows 95 Blue** - Blue-tinted variant
- **Windows 95 Green** - Green-tinted variant  
- **Windows 95 Purple** - Purple-tinted variant
- **Default** - Standard ImGui theme

```csharp
// Theme selection UI
themeManager.ShowThemeSelector();

// Programmatic theme switching
themeManager.ApplyTheme("Windows 95 Blue");
```

## Technical Implementation

### Color System
The implementation uses exact Windows 95 color specifications:

```csharp
public static readonly Vector4 ButtonFace = new(0.75f, 0.75f, 0.75f, 1.0f);        // #C0C0C0
public static readonly Vector4 DesktopTeal = new(0.0f, 0.5f, 0.5f, 1.0f);          // #008080
public static readonly Vector4 ActiveTitleBar = new(0.0f, 0.0f, 0.5f, 1.0f);       // #000080
```

### 3D Effect System
The signature Windows 95 3D effects are achieved through strategic border placement:

- **Light Source**: Upper-left corner (white highlights)
- **Shadow Areas**: Bottom and right edges (gray shadows)
- **Depth Simulation**: Multiple shadow levels for maximum depth perception

### Performance Optimization
- Efficient push/pop style management
- Cached style references
- Batched style modifications
- Minimal dynamic allocations

## Integration with Dalamud Plugins

### Plugin Setup

```csharp
public class Plugin : IDalamudPlugin
{
    public readonly RetroThemeManager ThemeManager = new();
    
    public Plugin()
    {
        // Apply Windows 95 theme on startup
        ThemeManager.ApplyTheme("Windows 95");
    }
}
```

### Window Implementation

```csharp
public class MyWindow : Window
{
    public override void Draw()
    {
        if (Windows95Theme.BeginGroupBox("My Application"))
        {
            if (Windows95Theme.RaisedButton("Action Button"))
            {
                // Handle button click
            }
            
            Windows95Theme.BeginInsetFrame();
            ImGui.InputText("Input", ref inputText, 256);
            Windows95Theme.EndInsetFrame();
            
            Windows95Theme.EndGroupBox();
        }
    }
}
```

## Demo Application

The plugin includes a comprehensive demo window (`Windows95DemoWindow`) showcasing:

- All theme variants
- Button styles and states
- Input controls with proper styling
- Desktop background simulation
- Technical specifications table
- Interactive UI components

Access the demo through the main plugin window or use the `/pmycommand` slash command.

## Best Practices

### Thread Safety
- All theme operations are designed for the main thread
- No background thread modifications required
- Compatible with Dalamud's threading model

### Performance
- Apply themes once during initialization
- Use `BeginInsetFrame()` / `EndInsetFrame()` pairs efficiently
- Batch multiple UI elements with same styling

### Compatibility
- Works with all standard ImGui controls
- Compatible with Dalamud's ImRaii system
- Maintains ImGui's immediate mode paradigm

## Historical Context

This implementation recreates the Windows 95 interface that defined desktop computing from 1995-2001:

- **256-color VGA optimization**: Color palette designed for limited color displays
- **MS Sans Serif typography**: Bitmap font system for crisp text rendering
- **3D metaphor**: Revolutionary interface design using light source simulation
- **Accessibility**: High contrast design for maximum readability

The theme provides an authentic retro computing experience while leveraging modern ImGui capabilities for smooth performance and compatibility.

## Customization

### Creating Custom Variants

```csharp
// Create a custom color variant
Windows95Theme.ApplyWindows95Variant(0.4f); // Hue shift

// Add to theme manager
themeManager.AddTheme("Custom Theme", () => {
    Windows95Theme.ApplyWindows95Variant(0.4f);
});
```

### Advanced Styling

```csharp
// Custom beveled rectangles
Windows95Theme.DrawBeveledRect(topLeft, bottomRight, raised: true);

// Manual color overrides
ImGui.PushStyleColor(ImGuiCol.Button, Windows95Theme.ButtonFace);
// ... draw elements
ImGui.PopStyleColor();
```

This implementation provides everything needed to create authentic Windows 95-style interfaces in modern Dalamud plugins, combining nostalgic aesthetics with contemporary functionality. 