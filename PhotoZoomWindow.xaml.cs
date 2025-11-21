using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PhotoGallery
{
    public partial class PhotoZoomWindow : Window
    {
        private const double ZoomIncrement = 0.2;
        private const double MinZoom = 0.1;
        private const double MaxZoom = 10.0;

        private double _currentZoom = 1.0;
        private Point _lastMousePosition;
        private bool _isDragging = false;

        public PhotoZoomWindow(string photoPath)
        {
            InitializeComponent();
            LoadPhoto(photoPath);
        }

        private void LoadPhoto(string photoPath)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(photoPath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                PhotoImage.Source = bitmap;
                PhotoNameText.Text = Path.GetFileName(photoPath);
                UpdateZoomDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading photo: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void PhotoImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Zoom in/out based on mouse wheel
            double zoomFactor = e.Delta > 0 ? 1 + ZoomIncrement : 1 - ZoomIncrement;
            double newZoom = _currentZoom * zoomFactor;

            newZoom = Math.Max(MinZoom, Math.Min(MaxZoom, newZoom));

            if (Math.Abs(newZoom - _currentZoom) > 0.001)
            {
                // Get mouse position relative to image
                Point mousePos = e.GetPosition(PhotoImage);

                // Calculate the point in the image that should remain under the cursor
                double imageX = (mousePos.X - TranslateTransform.X) / _currentZoom;
                double imageY = (mousePos.Y - TranslateTransform.Y) / _currentZoom;

                // Apply new zoom
                _currentZoom = newZoom;
                ScaleTransform.ScaleX = _currentZoom;
                ScaleTransform.ScaleY = _currentZoom;

                // Adjust translation to keep the point under the cursor
                TranslateTransform.X = mousePos.X - imageX * _currentZoom;
                TranslateTransform.Y = mousePos.Y - imageY * _currentZoom;

                UpdateZoomDisplay();
            }

            e.Handled = true;
        }

        private void PhotoImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentZoom > 1.0)
            {
                _isDragging = true;
                _lastMousePosition = e.GetPosition(this);
                PhotoImage.Cursor = Cursors.SizeAll;
                PhotoImage.CaptureMouse();
            }
        }

        private void PhotoImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            PhotoImage.Cursor = Cursors.Hand;
            PhotoImage.ReleaseMouseCapture();
        }

        private void PhotoImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(this);
                double deltaX = currentPosition.X - _lastMousePosition.X;
                double deltaY = currentPosition.Y - _lastMousePosition.Y;

                TranslateTransform.X += deltaX;
                TranslateTransform.Y += deltaY;

                _lastMousePosition = currentPosition;
            }
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            double newZoom = Math.Min(MaxZoom, _currentZoom * (1 + ZoomIncrement));
            SetZoom(newZoom);
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            double newZoom = Math.Max(MinZoom, _currentZoom * (1 - ZoomIncrement));
            SetZoom(newZoom);
        }

        private void ResetZoom_Click(object sender, RoutedEventArgs e)
        {
            SetZoom(1.0);
            TranslateTransform.X = 0;
            TranslateTransform.Y = 0;
        }

        private void SetZoom(double zoom)
        {
            _currentZoom = zoom;
            ScaleTransform.ScaleX = _currentZoom;
            ScaleTransform.ScaleY = _currentZoom;
            UpdateZoomDisplay();
        }

        private void UpdateZoomDisplay()
        {
            ZoomLevelText.Text = $"{Math.Round(_currentZoom * 100)}%";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
