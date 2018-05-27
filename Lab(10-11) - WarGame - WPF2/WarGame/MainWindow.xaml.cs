using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Random random = new Random();
        public List<Card> deck = new List<Card>();
        public int deck1Size = 26;
        public int deck2Size = 26;
        int score1 = 0;
        int score2 = 0;
        Player player1;
        Player player2;

        public int Deck1Size
        {
            get { return deck1Size; }
            set
            {
                deck1Size = player1.Cards.Count;
                OnPropertyChanged("Deck1Size");
            }
        }
        public int Deck2Size
        {
            get { return deck2Size; }
            set
            {
                deck2Size = player2.Cards.Count;
                OnPropertyChanged("Deck2Size");
            }
        }

        public int Score1
        {
            get { return score1; }
            set
            {
                score1 = value;
                OnPropertyChanged("Score1");
            }
        }
        public int Score2
        {
            get { return score2; }
            set
            {
                score2 = value;
                OnPropertyChanged("Score2");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();
            for(int i = 0; i<13; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    deck.Add(new Card(i, j));
                }
            }
            deck = deck.OrderBy(item => random.Next()).ToList();
            player1 = new Player();
            player2 = new Player();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void deck1Button_Click(object sender, RoutedEventArgs e)
        {
            //(x: 0-12): A, 2, 3,..., Q, K
            //(y: 0-3): hearts (♥), spades (♠),  diamonds(♦) and clubs (♣)
            Card card1 = player1.Cards[0];
            Card card2 = player2.Cards[0];
            card1Img.Source = player1.Cards[0].ImageSrc;
            player1.Cards.RemoveAt(0);
            card2Img.Source = player2.Cards[0].ImageSrc;
            player2.Cards.RemoveAt(0);
            Deck1Size--;
            Deck2Size--;
            if (player1.Cards.Count == 0)
            {
                deck1Button.IsEnabled = false;
                deck1Button.Content = null;
            }
            if (player2.Cards.Count == 0)
            {
                deck2Button.IsEnabled = false;
                deck2Button.Content = null;
            }
            if (card1.Val > card2.Val)
                Score1++;
            else if (card1.Val < card2.Val)
                Score2++;
            else
            {
                Score1++;
                Score2++;
                if((bool)ShowWarsCheckBox.IsChecked)
                {
                    MessageBox.Show("war");
                }
            }
            
        }

        private void deck2Button_Click(object sender, RoutedEventArgs e)
        {
            //(x: 0-12): A, 2, 3,..., Q, K
            //(y: 0-3): hearts (♥), spades (♠),  diamonds(♦) and clubs (♣)
            Card card1 = player1.Cards[0];
            Card card2 = player2.Cards[0];
            card1Img.Source = player1.Cards[0].ImageSrc;
            player1.Cards.RemoveAt(0);
            card2Img.Source = player2.Cards[0].ImageSrc;
            player2.Cards.RemoveAt(0);
            Deck1Size--;
            Deck2Size--;
            if (player1.Cards.Count == 0)
            {
                deck1Button.IsEnabled = false;
                deck1Button.Content = null;
            }
            if (player2.Cards.Count == 0)
            {
                deck2Button.IsEnabled = false;
                deck2Button.Content = null;

            }
            if (card1.Val > card2.Val)
                Score1++;
            else if (card1.Val < card2.Val)
                Score2++;
            else
            {
                Score1++;
                Score2++;
                if ((bool)ShowWarsCheckBox.IsChecked)
                {
                    MessageBox.Show("war");
                }
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {

            deck1Button.IsEnabled = true;
            BitmapSource bSource = new BitmapImage(new Uri("pack://application:,,,/Resources/back1.png"));
            Image back = new Image();
            Image back1 = new Image();
            back.Source = bSource;
            back1.Source = bSource;
            deck1Button.Content = back;
            deck2Button.IsEnabled = true;
            deck2Button.Content = back1;
            deck.Clear();
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    deck.Add(new Card(i, j));
                }
            }
            deck = deck.OrderBy(item => random.Next()).ToList();
            player1 = new Player();
            player2 = new Player();
            Deck1Size = 26;
            Deck2Size = 26;
            card1Img.Source = null;
            card2Img.Source = null;

        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            while(player1.Cards.Count > 0 && player2.Cards.Count > 0)
            {
                deck1Button_Click(sender, e);
            }
        }
    }
}
