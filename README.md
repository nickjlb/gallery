# Photo Gallery - Windows Native Application

A beautiful, modern Windows desktop application for viewing photos in a tiled gallery layout with zoom functionality.

## Features

- **Modern, Beautiful UI**: Clean and intuitive interface with a professional design
- **Folder Selection**: Easy folder browser to select any directory containing photos
- **Tiled Gallery View**: Photos displayed in an attractive tiled grid layout
- **Click to Zoom**: Click any photo to open it in a detailed zoom viewer
- **Advanced Zoom Controls**:
  - Mouse wheel zoom in/out
  - Zoom buttons (+ and -)
  - Click and drag to pan when zoomed in
  - Reset zoom button
  - Keyboard shortcut (ESC to close)
- **Supported Formats**: JPG, JPEG, PNG, BMP, GIF, TIFF, ICO

## Screenshots

### Main Gallery View
The main window shows all photos in a beautiful tiled layout with hover effects.

### Zoom View
Click any photo to open it in full-screen zoom mode with pan and zoom controls.

## Requirements

- **Windows 10 or later**
- **.NET 8.0 Runtime or SDK**

## Building from Source

### Prerequisites

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
2. A Windows machine (Windows 10 or later recommended)

### Build Steps

1. Clone or download this repository
2. Open a command prompt or PowerShell window
3. Navigate to the project directory:
   ```
   cd path\to\gallery
   ```
4. Build the project:
   ```
   dotnet build
   ```
5. Run the application:
   ```
   dotnet run
   ```

### Creating an Executable

To create a standalone executable:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

The executable will be located in:
```
bin\Release\net8.0-windows\win-x64\publish\PhotoGallery.exe
```

You can copy this file anywhere and run it without needing to install .NET.

## Usage

1. **Launch the Application**: Run `PhotoGallery.exe` or use `dotnet run`
2. **Select a Folder**: Click the "Select Folder" button in the top-right corner
3. **Browse Photos**: Choose a folder containing your photos
4. **View Gallery**: Photos will appear in a tiled grid layout
5. **Zoom In**: Click any photo to open it in the zoom viewer
6. **Pan and Zoom**:
   - Use the mouse wheel to zoom in and out
   - Click and drag to pan around when zoomed in
   - Use the +/- buttons for precise zoom control
   - Click "Reset" to return to original zoom level
   - Press ESC to close the zoom viewer

## Project Structure

```
PhotoGallery/
├── PhotoGallery.csproj    # Project configuration
├── App.xaml               # Application resources and styles
├── App.xaml.cs            # Application entry point
├── MainWindow.xaml        # Main gallery window UI
├── MainWindow.xaml.cs     # Main window logic
├── PhotoZoomWindow.xaml   # Zoom viewer UI
└── PhotoZoomWindow.xaml.cs # Zoom viewer logic
```

## Technical Details

- **Framework**: WPF (Windows Presentation Foundation) on .NET 8.0
- **Language**: C#
- **UI Technology**: XAML
- **Architecture**: MVVM-inspired with code-behind
- **Image Loading**: BitmapImage with caching for performance
- **Zoom Implementation**: ScaleTransform and TranslateTransform for smooth zooming

## Customization

You can customize the appearance by modifying the XAML files:

- **Colors**: Change the hex color codes in `MainWindow.xaml` and `PhotoZoomWindow.xaml`
- **Tile Size**: Adjust the `Width` and `Height` properties in the DataTemplate (currently 280x280)
- **Zoom Limits**: Modify `MinZoom` and `MaxZoom` constants in `PhotoZoomWindow.xaml.cs`
- **Zoom Speed**: Adjust `ZoomIncrement` in `PhotoZoomWindow.xaml.cs`

## Troubleshooting

### "No photos found" message
- Ensure the folder contains image files with supported extensions
- Check that file extensions are correct (.jpg, .png, etc.)

### Application won't start
- Verify .NET 8.0 or later is installed
- Try rebuilding: `dotnet clean` then `dotnet build`

### Photos not displaying
- Check file permissions on the photo folder
- Ensure image files are not corrupted
- Try with a different folder

## License

This project is open source and available for personal and commercial use.

## Contributing

Feel free to submit issues, fork the repository, and create pull requests for any improvements.
