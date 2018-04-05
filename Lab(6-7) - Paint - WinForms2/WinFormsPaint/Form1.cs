using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            KnownColor[] values = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor kc in values)
            {
                Color RealColor = Color.FromKnownColor(kc);
                Panel n = new Panel();
                n.BackColor = RealColor;
                n.Size = new Size(25, 25);
                n.Padding = new Padding(1);
                n.Margin = new Padding(2);
                flowLayoutPanel1.Controls.Add(n);
                n.Click += (object sender, EventArgs e) =>
                {
                    pen = new Pen(n.BackColor, 1);
                };
            }
            
            
        }
        Bitmap bmp;
        bool isBrush = false;
        Pen pen = Pens.Black;
        
        private Point? _Previous = null;
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if(isBrush)
            {
                _Previous = e.Location;
                pictureBox1_MouseMove_1(sender, e);
            }
            
        }
        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            
            if (_Previous != null)
            {
                if (pictureBox1.Image == null)
                {
                    bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    pictureBox1.Image = bmp;
                }
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                   
                    g.DrawLine(pen, _Previous.Value, e.Location);
                }
                pictureBox1.Invalidate();
                _Previous = e.Location;
            }
        }
        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            _Previous = null;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Bitmap temp = bmp;
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics grfx = Graphics.FromImage(bmp))
                {
                    grfx.DrawImage(temp, 0, 0);
                }
                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();
            }
            catch(Exception)
            {

            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isBrush)
            {
                isBrush = false;
                this.toolStripButton1.Checked = false;


            }
            else
            {
                isBrush = true;
                this.toolStripButton1.Checked = true;
            }
        }

    }
}
