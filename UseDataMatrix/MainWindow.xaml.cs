using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DataMatrix.net;
using System.IO;
using System.Drawing;


namespace UseDataMatrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private System.Drawing.Image Encode(string input)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();

            DmtxImageEncoderOptions encoderOptions = new DmtxImageEncoderOptions();
            encoderOptions.BackColor = System.Drawing.Color.White;
            encoderOptions.ForeColor = System.Drawing.Color.Black;
            encoderOptions.ModuleSize = 96;
            encoderOptions.MarginSize = 5;

            System.Drawing.Image outputImage = encoder.EncodeImage(input, encoderOptions);
            return outputImage;
        }

        private BitmapImage ConvertToWpfImage(System.Drawing.Image sdi)
        {
            // ImageSource ...
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();

            // Save to a memory stream...
            sdi.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            // Rewind the stream... 
            ms.Seek(0, SeekOrigin.Begin);

            // Tell the WPF image to use this stream... 
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }


        private void btnMakeBarcode_Click(object sender, RoutedEventArgs e)
        {
            btnMakeBarcode.IsEnabled = false;
            imgBarcode.Visibility = System.Windows.Visibility.Hidden;
            
            imgBarcode.Source = ConvertToWpfImage(Encode(txtInput.Text));
            imgBarcode.Visibility = System.Windows.Visibility.Visible;

            btnMakeBarcode.IsEnabled = true;
        }
    }
}
