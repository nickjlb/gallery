using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace PhotoGallery
{
    public partial class MainWindow : Window
    {
        private readonly string[] _supportedExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".ico" };
        private List<string> _currentPhotos = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Use OpenFolderDialog (available in .NET 8+)
                var dialog = new OpenFolderDialog
                {
                    Title = "Select a folder containing photos",
                    Multiselect = false
                };

                if (dialog.ShowDialog() == true)
                {
                    string folderPath = dialog.FolderName;
                    LoadPhotosFromFolder(folderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting folder: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPhotosFromFolder(string folderPath)
        {
            try
            {
                StatusText.Text = "Loading photos...";
                _currentPhotos.Clear();

                // Get all image files from the folder
                var photoFiles = Directory.GetFiles(folderPath)
                    .Where(file => _supportedExtensions.Contains(
                        Path.GetExtension(file).ToLowerInvariant()))
                    .OrderBy(file => file)
                    .ToList();

                if (photoFiles.Count == 0)
                {
                    MessageBox.Show("No photos found in the selected folder.",
                        "No Photos", MessageBoxButton.OK, MessageBoxImage.Information);
                    StatusText.Text = "No photos found";
                    return;
                }

                _currentPhotos = photoFiles;

                // Update UI
                FolderPathText.Text = folderPath;
                WelcomePanel.Visibility = Visibility.Collapsed;
                PhotoItemsControl.Visibility = Visibility.Visible;

                // Load photos into the gallery
                PhotoItemsControl.ItemsSource = _currentPhotos;

                StatusText.Text = $"Loaded {_currentPhotos.Count} photo(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading photos: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Error loading photos";
            }
        }

        private void Photo_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender is Border border && border.Tag is string photoPath)
                {
                    // Open the zoom window with the selected photo
                    var zoomWindow = new PhotoZoomWindow(photoPath);
                    zoomWindow.Owner = this;
                    zoomWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening photo: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
