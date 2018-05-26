using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WarGame
{
    class Player
    {
        public List<Card> Cards { get; set; }

        public Player()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            Cards = new List<Card>();
            for(int i = 0; i<26; i++)
            {
                Cards.Add(window.deck[0]);
                window.deck.RemoveAt(0);
            }
        }
    }
}
