using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace UniversalImageConverter
{
    public partial class MainWindow : Window
    {
        private byte[] image;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog() { Filter = "Pliki graficzne (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" };
            var result = dialog.ShowDialog();
            if (result == false) return;

            image = File.ReadAllBytes(dialog.FileName);
        }

        private void LoadFromArray(object sender, RoutedEventArgs e)
        {
            var stream = new MemoryStream(image);
            stream.Seek(0, SeekOrigin.Begin);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();

            PicPreview.Source = bitmap;
        }
    }
}
