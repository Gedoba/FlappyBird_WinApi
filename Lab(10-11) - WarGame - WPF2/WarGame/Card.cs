using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WarGame
{
    public class Card
    {
        //(x: 0-12): A, 2, 3,..., Q, K
        //(y: 0-3): hearts (♥), spades (♠),  diamonds(♦) and clubs (♣)
        public int Pos { get; set; }//Ace-King(0-12)
        public int Val { get; set; } //2-Ace(1-13)
        int Color { get; set; }//4 of them(0-3)
        public ImageSource ImageSrc { get; private set; } //cards' image
        public Card(int pos, int color)
        {
            this.Pos = pos;
            this.Color = color;
            if (this.Pos == 0)
                this.Val = 14;
            else
                this.Val = Pos + 1;
            BitmapSource bSource = new BitmapImage(new Uri("pack://application:,,,/Resources/cards.jpg"));
            CroppedBitmap cb = new CroppedBitmap(bSource, new Int32Rect(this.Pos * 225, this.Color * 315, 225, 315));
            ImageSrc = cb;
        }
    }
}
