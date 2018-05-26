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
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void deck1Button_Click(object sender, RoutedEventArgs e)
        {
            //(x: 0-12): A, 2, 3,..., Q, K
            //(y: 0-3): hearts (♥), spades (♠),  diamonds(♦) and clubs (♣)
            int x = random.Next(0,12);
            int y = random.Next(0, 3);
            BitmapSource bSource = new BitmapImage(new Uri("pack://application:,,,/Resources/cards.jpg"));
            CroppedBitmap cb = new CroppedBitmap(bSource, new Int32Rect(x*225, y*315, 225, 315));
            card1Img.Source = cb;
            x = random.Next(0, 12);
            y = random.Next(0, 3);
            cb = new CroppedBitmap(bSource, new Int32Rect(x*225, y*315, 225, 315));
            card2Img.Source = cb;
            decreaseDeckSize();

        }

        private void deck2Button_Click(object sender, RoutedEventArgs e)
        {
            //(x: 0-12): A, 2, 3,..., Q, K
            //(y: 0-3): hearts (♥), spades (♠),  diamonds(♦) and clubs (♣)
            int x = random.Next(0, 12);
            int y = random.Next(0, 3);
            BitmapSource bSource = new BitmapImage(new Uri("pack://application:,,,/Resources/cards.jpg"));
            CroppedBitmap cb = new CroppedBitmap(bSource, new Int32Rect(x * 225, y * 315, 225, 315));
            card1Img.Source = cb;
            x = random.Next(0, 12);
            y = random.Next(0, 3);
            cb = new CroppedBitmap(bSource, new Int32Rect(x * 225, y * 315, 225, 315));
            card2Img.Source = cb;
            decreaseDeckSize();

        }
        private void decreaseDeckSize()
        {
            int deck1Size = Int32.Parse(deck1SizeText.Text);
            int deck2Size = Int32.Parse(deck2SizeText.Text);
            deck1Size--;
            deck2Size--;
            deck1SizeText.Text = deck1Size.ToString();
            deck2SizeText.Text = deck2Size.ToString();
        }
    }
}
