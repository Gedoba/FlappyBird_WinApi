using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace WinFormsPaint
{
    public partial class Form1 : Form
    {
        private Point? _Previous = null, point = null;
        private Bitmap bmp, tmp;
        private Pen pen = new Pen(Color.Black, 2); // main Pen
        private Color CurrentColor = Color.Black;
        private bool isDrawable = false;
        public Form1()
        {
            //default - English
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");

            InitializeComponent();

            if (pictureBox1.Image == null)
            {
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                }
                pictureBox1.Image = bmp;
            }

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
                    Color col = Color.FromArgb(colorPanel.BackColor.ToArgb() ^ 0xffffff);
                    Pen p = new Pen(col, 3)
                    {
                        DashStyle = DashStyle.Dot
                    };
                    Graphics g = colorPanel.CreateGraphics();
                    g.DrawRectangle(p, colorPanel.ClientRectangle);
                    p.Dispose();
                    g.Dispose();
                    
                    currentColorButton.BackColor = CurrentColor;

                };
                colorPanel.Paint += (object sender, PaintEventArgs e) =>
                {
                    if (colorPanel.BackColor == CurrentColor)
                    {
                        Color col = Color.FromArgb(colorPanel.BackColor.ToArgb() ^ 0xffffff);
                        Pen p = new Pen(col, 3)
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

            for (int i = 1; i < 4; i++)
            {
                ThicknessComboBox.Items.Add(i);
            }
            ThicknessComboBox.SelectedItem = ThicknessComboBox.Items[1];
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {

            _Previous = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                isDrawable = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawable = false;
                StopDrawing();
            }

        }

        private void StopDrawing()
        {
            if (rectangleButton.Checked || ellipseButton.Checked)
            {
                if (isDrawable)
                {
                    bmp = tmp;
                }
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
            }
        }
        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (brushButton.Checked)
            {
                if (_Previous != null && isDrawable)
                {
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        //smoooooth
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawLine(pen, _Previous.Value, e.Location);
                    }
                    pictureBox1.Invalidate();
                    _Previous = e.Location;
                }
            }
            else if (rectangleButton.Checked || ellipseButton.Checked)
            {
                if (isDrawable && point != null)
                {
                    tmp = new Bitmap(bmp);
                    int x = Math.Min(point.Value.X, e.Location.X);
                    int y = Math.Min(point.Value.Y, e.Location.Y);
                    Rectangle rect = new Rectangle(x, y, Math.Abs(e.Location.X - point.Value.X), Math.Abs(e.Location.Y - point.Value.Y));

                    using (Graphics g = Graphics.FromImage(tmp))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        if (rectangleButton.Checked)
                            g.DrawRectangle(pen, rect);
                        else if (ellipseButton.Checked)
                            g.DrawEllipse(pen, rect);
                    }
 
                    pictureBox1.Image = tmp;
                    pictureBox1.Refresh();
                }
                else if (point == null)
                {
                    point = e.Location;
                }
                else
                {
                    point = new Point?();
                }
            }
            System.GC.Collect();
        }
        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StopDrawing();
                isDrawable = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tmp = bmp;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.DrawImage(tmp, 0, 0);
            }
            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }

        private void brushButton_Click(object sender, EventArgs e)
        {
            if (this.brushButton.Checked)
            {
                this.brushButton.Checked = false;
                this.rectangleButton.Checked = false;
                this.ellipseButton.Checked = false;
            }
            else
            {
                this.brushButton.Checked = true;
                this.rectangleButton.Checked = false;
                this.ellipseButton.Checked = false;
            }
        }

        private void ThicknessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pen = new Pen(this.pen.Color, (int)ThicknessComboBox.SelectedItem);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ImageFormat format;
            saveFileDialog1.Title = "Save img file";
            saveFileDialog1.Filter = "PNG Image|*.png|Bitmap Image|*.bmp|JPeg Image|*.jpeg";
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..//..//");
            saveFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
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

        private void loadButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load img file";
            openFileDialog1.Filter = "PNG Image|*.png|Bitmap Image|*.bmp|JPeg Image|*.jpeg";
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..//..//");
            openFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap loaded = new Bitmap(openFileDialog1.FileName);
                Size diff = loaded.Size - bmp.Size;
                //resizing
                bmp = loaded;
                pictureBox1.Size = bmp.Size;
                pictureBox1.Image = bmp;
                //protection from cropping
                this.SizeChanged -= new EventHandler(Form1_SizeChanged);
                pictureBox1.Refresh();
                //Change size of form and refresh it
                this.Size = this.Size + diff;
                this.Invalidate();
                // reAdd SizeChanged event
                this.SizeChanged += new EventHandler(Form1_SizeChanged);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            try
            {
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.Clear(Color.White);
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
                graphics.Dispose();
            }
            catch (ArgumentNullException) { /* no need to clear */ }

        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            if (this.rectangleButton.Checked)
            {
                this.brushButton.Checked = false;
                this.rectangleButton.Checked = false;
                this.ellipseButton.Checked = false;
            }
            else
            {
                this.brushButton.Checked = false;
                this.rectangleButton.Checked = true;
                this.ellipseButton.Checked = false;
            }
        }

        private void ellipseButton_Click(object sender, EventArgs e)
        {
            if (this.ellipseButton.Checked)
            {
                this.brushButton.Checked = false;
                this.rectangleButton.Checked = false;
                this.ellipseButton.Checked = false;
            }
            else
            {
                this.brushButton.Checked = false;
                this.rectangleButton.Checked = false;
                this.ellipseButton.Checked = true;
            }
        }

        private void language_Changed(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            if (button == englishButton)
            {
                if (!polishButton.Checked)
                {
                    englishButton.Checked = true;
                    return;
                }
                polishButton.Checked = false;
                CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
            }
            else
            {
                if (!englishButton.Checked)
                {
                    polishButton.Checked = true;
                    return;
                }
                englishButton.Checked = false;
                CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture("pl");
            }
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            //applying resources too all items in toolStrip
            foreach (ToolStripItem l in toolStrip1.Items)
            {
                resources.ApplyResources(l, l.Name);
            }
            //applying resources for 'colors' groupBox
            resources.ApplyResources(groupBox1, groupBox1.Name);

        }
    }
}
