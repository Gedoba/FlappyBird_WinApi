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


namespace WarGame
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

        private void deck1Button_Click(object sender, RoutedEventArgs e)
        {
            // Create an Image element.
            // Create an Image element.
            //Image croppedImage = new Image();
            //croppedImage.Width = 200;
            //croppedImage.Margin = new Thickness(5);
            BitmapSource bSource = new BitmapImage(new Uri("C:\\Users\\286327\\source\\repos\\WarGame\\WarGame\\cards.jpg"));
            // Create a CroppedBitmap based off of a xaml defined resource.
            CroppedBitmap cb = new CroppedBitmap(
               bSource,
               new Int32Rect(226, 0, 225, 315));       //select region rect
            //croppedImage.Source = cb;
            card1Img.Source = cb;
            cb = new CroppedBitmap(bSource, new Int32Rect(226, 316, 225, 315));       //select region rect
            card2Img.Source = cb;

        }
    }
}
