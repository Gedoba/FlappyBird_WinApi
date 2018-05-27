using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// 
    public class HighScores
    {
        public int Score { get; set; }
        public string Name { get; set; }
    }

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
        ObservableCollection<Card> deck1View;
        ObservableCollection<Card> deck2View;


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
            deck1View = new ObservableCollection<Card>(player1.Cards);
            deck2View = new ObservableCollection<Card>(player2.Cards);
            deck1ListView.ItemsSource = deck1View;
            deck2ListView.ItemsSource = deck2View;
            //highscores = new ObservableCollection<(int Score, string player)>();
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

            if (card1.Val > card2.Val)
            {
                Score1 = Score1 + 2;
                player1.Cards.Add(card2);
                player1.Cards.Add(card1);
            }

            else if (card1.Val < card2.Val)
            {
                Score2 = Score2 + 2;
                player2.Cards.Add(card1);
                player2.Cards.Add(card2);
            }

            else
            {
                war(card1, card2, new List<Card>());
            }
            checkFinishConditions();

        }
        private void checkFinishConditions()
        {
            deck1View = new ObservableCollection<Card>(player1.Cards);
            deck2View = new ObservableCollection<Card>(player2.Cards);
            deck1ListView.ItemsSource = deck1View;
            deck2ListView.ItemsSource = deck2View;
            if (player1.Cards.Count == 0 || player2.Cards.Count == 0)
            {
                if (player1.Cards.Count == 0)
                {
                    deck1Button.Visibility = Visibility.Hidden;
                    deck2Button.IsEnabled = false;
                }
                if (player2.Cards.Count == 0)
                {
                    deck2Button.Visibility = Visibility.Hidden;
                    deck1Button.IsEnabled = false;
                }
                Deck1Size--;
                Deck2Size--;
                card1Img.Source = null;
                card2Img.Source = null;
                //save highscore
                if (score1 > score2)
                    highscoresListView.Items.Add(new HighScores { Name = "Left", Score = score1 });
                else
                    highscoresListView.Items.Add(new HighScores { Name = "Right", Score = score2 });
                highscoresListView.Items.SortDescriptions.Add(new SortDescription("Score", ListSortDirection.Descending));
                return;
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

            if (card1.Val > card2.Val)
            {
                Score1 = Score1 + 2;
                player1.Cards.Add(card2);
                player1.Cards.Add(card1);
            }
            else if (card1.Val < card2.Val)
            {
                Score2 = Score2 + 2;
                player2.Cards.Add(card1);
                player2.Cards.Add(card2);
            }
            else
            {
                war(card1, card2, new List<Card>());
            }
            checkFinishConditions();
        }
        public void war(Card card1, Card card2, List<Card> reward, int scoreChange = 4)
        {
            if (reward.Count == 0)
                reward = new List<Card>();
            if ((bool)ShowWarsCheckBox.IsChecked)
            {
                MessageBox.Show("War");
            }
            reward.Add(card1);
            reward.Add(card2);
            if (player1.Cards.Count < 2 || player2.Cards.Count < 2)
            {
                if ((bool)ShowWarsCheckBox.IsChecked)
                    MessageBox.Show("Not enough cards");
                if (player1.Cards.Count > player2.Cards.Count)
                {
                    foreach (var c in reward)
                        player1.Cards.Add(c);
                    Deck1Size++;
                }

                else
                {
                    foreach (var c in reward)
                        player2.Cards.Add(c);
                    Deck2Size++;
                }
                card1Img.Source = null;
                card2Img.Source = null;
                return;
            }
            reward.Add(player1.Cards[0]);
            player1.Cards.RemoveAt(0);
            Deck1Size--;
            reward.Add(player2.Cards[0]);
            player2.Cards.RemoveAt(0);
            Deck2Size--;

            Card currentCard1 = player1.Cards[0];
            player1.Cards.RemoveAt(0);
            Deck1Size--;
            Card currentCard2 = player2.Cards[0];
            player2.Cards.RemoveAt(0);
            Deck2Size--;
            if (currentCard1.Val > currentCard2.Val)
            {
                scoreChange += 2;
                reward.Add(currentCard2);
                reward.Add(currentCard1);
                foreach (var card in reward)
                {
                    player1.Cards.Add(card);
                }
                Deck1Size++;
                Score1 += scoreChange;
            }
            else if (card1.Val < card2.Val)
            {
                scoreChange += 2;
                reward.Add(currentCard1);
                reward.Add(currentCard2);
                foreach (var card in reward)
                {
                    player2.Cards.Add(card);
                }
                Deck2Size++;
                Score2 += scoreChange;
            }
            else
            {
                war(currentCard1, currentCard2, reward, scoreChange);
            }
            checkFinishConditions();
        }
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            Score1 = 0;
            Score2 = 0;
            deck1Button.Visibility = Visibility.Visible;
            deck2Button.Visibility = Visibility.Visible;
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
            deck1View = new ObservableCollection<Card>(player1.Cards);
            deck2View = new ObservableCollection<Card>(player2.Cards);
            deck1ListView.ItemsSource = deck1View;
            deck2ListView.ItemsSource = deck2View;

        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            while (player1.Cards.Count > 0 && player2.Cards.Count > 0)
            {
                deck1Button_Click(sender, e);
            }
        }

        private void changeBackButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "All supported graphics|*.png;*.jpg;*.jpeg|JPEG|*.jpg;*.jpeg|Portable Network Graphics|*.png" ;
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                BitmapSource bSource = new BitmapImage(new Uri(filename));
                bSource.Freeze();
                Image back = new Image();
                Image back1 = new Image();
                back.Source = bSource;
                back1.Source = bSource;
                back.Stretch = Stretch.Fill;
                back1.Stretch = Stretch.Fill;
                deck1Button.Content = back;
                deck2Button.Content = back1;
            }
        }
    }
}
