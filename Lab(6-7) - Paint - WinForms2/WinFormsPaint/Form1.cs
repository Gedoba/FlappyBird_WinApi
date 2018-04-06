using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
///  TODO:
///  RMB click -> disallow drawing.
///  hovering over currentColorButton
/// </summary>
namespace WinFormsPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            KnownColor[] values = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            currentColorButton.BackColor = CurrentColor;

            foreach (KnownColor kc in values)
            {

                Color RealColor = Color.FromKnownColor(kc);
                Panel colorPanel = new Panel();
                colorPanel.BackColor = RealColor;
                colorPanel.Size = new Size(25, 25);
                colorPanel.Margin = new Padding(2);
                colorPanel.Click += (object sender, EventArgs e) =>
                {
                    foreach (Panel b1 in flowLayoutPanel1.Controls)
                    {
                        if (b1.BackColor == CurrentColor)
                        {
                            Graphics g1 = b1.CreateGraphics();
                            g1.Clear(CurrentColor);
                            g1.Dispose();
                            break;
                        }
                    }
                    //Set current color
                    CurrentColor = colorPanel.BackColor;
                    pen.Color = CurrentColor;
                    //Border
                    Color c = Color.FromArgb(colorPanel.BackColor.ToArgb() ^ 0xffffff);
                    Pen p = new Pen(c, 3)
                    {
                        DashStyle = DashStyle.Dot
                    };
                    Graphics g = colorPanel.CreateGraphics();
                    g.DrawRectangle(p, colorPanel.ClientRectangle);
                    p.Dispose();
                    g.Dispose();
                    //Set toolstrip button colour
                    currentColorButton.BackColor = CurrentColor;

                };
                colorPanel.Paint += (object sender, PaintEventArgs e) =>
                {
                    if (colorPanel.BackColor == CurrentColor)
                    {
                        Color c = Color.FromArgb(colorPanel.BackColor.ToArgb() ^ 0xffffff);
                        Pen p = new Pen(c, 3)
                        {
                            DashStyle = DashStyle.Dot
                        };
                        e.Graphics.DrawRectangle(p, colorPanel.ClientRectangle);
                        p.Dispose();
                    }
                    //Otherwise - clear it
                    else
                    {
                        e.Graphics.Clear(colorPanel.BackColor);
                    }
                };
                flowLayoutPanel1.Controls.Add(colorPanel);
            }


            for (int i = 1; i<4; i++)
            {
                ThicknessComboBox.Items.Add(i);
            }
            ThicknessComboBox.SelectedItem = ThicknessComboBox.Items[1];

        }
        Bitmap bmp;
        bool isBrush = false;
        Pen pen = new Pen(Color.Black, 2);
        Color CurrentColor = Color.Black;
        
    


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
                this.brushButton.Checked = false;


            }
            else
            {
                isBrush = true;
                this.brushButton.Checked = true;
            }
        }

        private void ThicknessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pen = new Pen(this.pen.Color, (int)ThicknessComboBox.SelectedItem);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ImageFormat format;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get proper ImageFormat for saving
                switch (saveFileDialog1.FilterIndex)
                {
                    case 0:
                        format = ImageFormat.Png;
                        break;
                    case 1:
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }
                //Save the file
                bmp.Save(saveFileDialog1.FileName, format);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
