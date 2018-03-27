using ListViewSortAnyColumn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TranslatorWinForms
{
    public partial class Form1 : Form
    {
        static public List<string[]> words = new List<string[]>();
        static public Dictionary<string, string> Dict;
        List<string> fonts = new List<string>();
        bool isBold = false, isUnderlined = false, isItalic = false;
        int fontStyle = 0; //0 - regular, 1 - bold, 2- italic, 3 - underlined, 1+2=3 - bold/italic, etc. 6 - all
        FontStyle style = new FontStyle();



        public Form1()
        {

            InitializeComponent();
            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {

                fontComboBox.Items.Add(oneFontFamily.Name);
            }

            fontComboBox.Text = this.upperRichTextBox.Font.Name.ToString();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            upperRichTextBox.Focus();
        }
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null)
            {
                MessageBox.Show("File is empty");
            }
            listView1.Items.Clear();
            words.Clear();
            StringComparer comparer = StringComparer.CurrentCultureIgnoreCase;
            Dict = new Dictionary<string, string>(comparer);


            foreach (string line in File.ReadAllLines(files[0]))
            {
                string[] word = line.Split(' ');
                if (word.Length != 2)
                    continue;
                if (word[0].All(c => Char.IsLetter(c)) && word[1].All(c => Char.IsLetter(c)))
                    words.Add(word);

            }
            listView1.Columns[0].Text = words[0][0];
            listView1.Columns[1].Text = words[0][1];


            for (int i = 1; i < words.Count; i++)
            {
                if (!Dict.ContainsKey(words[i][0]))
                {
                    Dict.Add(words[i][0], words[i][1]);
                    listView1.Items.Add(
                        new ListViewItem(new[]
                        {
                            words[i][0],
                            words[i][1]
                        }));
                }
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..//..//");
            theDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            listView1.Items.Clear();
            words.Clear();
            StringComparer comparer = StringComparer.CurrentCultureIgnoreCase;
            Dict = new Dictionary<string, string>(comparer);

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string line in File.ReadAllLines(theDialog.FileName.ToString()))
                {
                    string[] word = line.Split(' ');
                    if (word.Length != 2)
                        continue;
                    if (word[0].All(c => Char.IsLetter(c)) && word[1].All(c => Char.IsLetter(c)))
                        words.Add(word);

                }
                listView1.Columns[0].Text = words[0][0];
                listView1.Columns[1].Text = words[0][1];


                for (int i = 1; i < words.Count; i++)
                {
                    if (!Dict.ContainsKey(words[i][0]))
                    {
                        Dict.Add(words[i][0], words[i][1]);
                        listView1.Items.Add(
                            new ListViewItem(new[]
                            {
                            words[i][0],
                            words[i][1]
                            }));
                    }
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog openFileDialog1 = new SaveFileDialog();
            SaveFileDialog theDialog = new SaveFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..//..//");
            theDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            // copy of Dict, not a reference!
            var tempDict = new Dictionary<string, string>(Dict);
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(theDialog.FileName.ToString(), String.Empty);
                for (int i = 0; i < words.Count; i++)
                {
                    if (tempDict.ContainsKey(words[i][0]))
                    {
                        File.AppendAllText(theDialog.FileName.ToString(), words[i][0] + " " + words[i][1] + Environment.NewLine);
                        tempDict.Remove(words[i][0]);
                    }
                }
            }
        }

        private void Translate_click(object sender, EventArgs e)
        {

            lowerRichTextBox.Clear();
            List<string[]> Lines = new List<string[]>();
            if (upperRichTextBox.Text.Contains('\n'))
            {
                string[] SplitLines = upperRichTextBox.Text.Split('\n');
                foreach (string a in SplitLines)
                {
                    string[] LineOfWords = a.Split(' ');
                    LineOfWords[LineOfWords.Length - 1] += " \n";
                    Lines.Add(LineOfWords);
                }
            }
            else
            {
                Lines.Add(upperRichTextBox.Text.Split(' '));
            }

            foreach (string[] LineOfWords in Lines)
            {
                foreach (string a in LineOfWords)
                {
                    if (words.Count == 0)
                    {
                        lowerRichTextBox.SelectionColor = Color.FromArgb(255, 0, 0);
                        lowerRichTextBox.AppendText(a + " ");
                        return;
                    }
                    if (Dict.ContainsKey(a))
                    {
                        lowerRichTextBox.SelectionColor = Color.FromArgb(0, 0, 0);
                        lowerRichTextBox.AppendText(Dict[a] + " ");
                    }
                    else if (Dict.ContainsValue(a))
                    {
                        lowerRichTextBox.SelectionColor = Color.FromArgb(0, 0, 0);
                        lowerRichTextBox.AppendText(Dict.FirstOrDefault(x => x.Value == a).Key + " ");
                    }
                    else if (a == "420")
                        lowerRichTextBox.AppendText("kod Spalińskiego\n");
                    else
                    {
                        lowerRichTextBox.SelectionColor = Color.FromArgb(255, 0, 0);
                        lowerRichTextBox.AppendText(a + " ");
                    }
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var toDelete = listView1.SelectedItems;
                foreach (ListViewItem item in toDelete)
                {
                    string toDelKey = item.SubItems[0].Text;
                    //remove word pair from dictionary
                    Dict.Remove(toDelKey);
                    //remove it from the list
                    item.Remove();
                }

            }
        }

        // ColumnClick event handler.
        private void ColumnClick(object o, ColumnClickEventArgs e)
        {
            ItemComparer sorter = listView1.ListViewItemSorter as ItemComparer;
            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                listView1.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                if (sorter.Order == SortOrder.Ascending)
                    sorter.Order = SortOrder.Descending;
                else
                    sorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listView1.Sort();
        }

        private void AddWord_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            AddWord _AddWord = new AddWord(this);
            if (words.Count == 0)
            {
                _AddWord.label1.Text = "English";
                _AddWord.label2.Text = "Polish";
            }
            else
            {
                _AddWord.label1.Text = listView1.Columns[0].Text;
                _AddWord.label2.Text = listView1.Columns[1].Text;
            }
            dr = _AddWord.ShowDialog();
            if (dr == DialogResult.OK)
            {
                for (int i = 1; i < words.Count; i++)
                {
                    if (!Dict.ContainsKey(words[i][0]))
                    {
                        Dict.Add(words[i][0], words[i][1]);
                        listView1.Items.Add(
                            new ListViewItem(new[]
                            {
                                Form1.words[i][0],
                                Form1.words[i][1]
                            }));
                    }
                }
            }
            else
            {
                //MessageBox.Show("nothing added");
            }
        }

        private void toolStripComboBox1_Selected(object sender, EventArgs e)
        {
            upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Regular);
            lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Regular);
        }

        private void toolStrip_BackgroundColor(object sender, EventArgs e)
        {
            colorDialog1.Color = upperRichTextBox.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                upperRichTextBox.BackColor = colorDialog1.Color;
                lowerRichTextBox.BackColor = colorDialog1.Color;

            }
        }

        private void toolStripItalic_Click(object sender, EventArgs e)
        {
            if (!isItalic)
            {
                isItalic = true;
                fontStyle += 2;
                italicButton.Checked = true;
            }
            else
            {
                isItalic = false;
                fontStyle -= 2;
                italicButton.Checked = false;

            }
            changeFontStyle();
        }

        private void toolStripUnderline_Click(object sender, EventArgs e)
        {
            if (!isUnderlined)
            {
                isUnderlined = true;
                fontStyle += 4;
                underlinedButton.Checked = true;

            }
            else
            {
                isUnderlined = false;
                fontStyle -= 4;
                underlinedButton.Checked = false;

            }
            changeFontStyle();
        }

        private void toolStripFontColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = upperRichTextBox.ForeColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                upperRichTextBox.ForeColor = colorDialog1.Color;
                lowerRichTextBox.ForeColor = colorDialog1.Color;

            }
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            this.listView1.Columns[0].Width = this.listView1.Width / 2;
            this.listView1.Columns[1].Width = this.listView1.Width / 2;

        }

        private void lowerRichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                addWordToolStripMenuItem.Text = "Add " + lowerRichTextBox.Text;
                addWordMenuStrip.Show(Cursor.Position);
            }
        }

        private void addWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            AddWord _AddWord = new AddWord(this);

            //lowerRichTextBox.Clear();
            List<string[]> Lines = new List<string[]>();
            if (lowerRichTextBox.Text.Contains('\n'))
            {
                string[] SplitLines = lowerRichTextBox.Text.Split('\n');
                foreach (string a in SplitLines)
                {
                    string[] LineOfWords = a.Split(' ');
                    LineOfWords[LineOfWords.Length - 1] += " \n";
                    Lines.Add(LineOfWords);
                }
            }
            else
            {
                Lines.Add(lowerRichTextBox.Text.Split(' '));
            }
            _AddWord.textBox1.Text = Lines[0][0];



            _AddWord.textBox1.Enabled = false;
            if (words.Count == 0)
            {
                _AddWord.label1.Text = "English";
                _AddWord.label2.Text = "Polish";
            }
            else
            {
                _AddWord.label1.Text = listView1.Columns[0].Text;
                _AddWord.label2.Text = listView1.Columns[1].Text;
            }
            dr = _AddWord.ShowDialog();
            if (dr == DialogResult.OK)
            {
                for (int i = 1; i < words.Count; i++)
                {
                    if (!Dict.ContainsKey(words[i][0]))
                    {
                        Dict.Add(words[i][0], words[i][1]);
                        listView1.Items.Add(
                            new ListViewItem(new[]
                            {
                                Form1.words[i][0],
                                Form1.words[i][1]
                            }));
                    }
                }
            }
            else
            {
                //MessageBox.Show("nothing added");
            }
        }

        private void toolStripBold_Click(object sender, EventArgs e)
        {
            if (!isBold)
            {
                isBold = true;
                fontStyle += 1;
                boldButton.Checked = true;
            }
            else
            {
                isBold = false;
                fontStyle -= 1;
                boldButton.Checked = false;

            }
            changeFontStyle();
        }
        private void changeFontStyle()
        {
            switch (fontStyle)
            {
                case 0:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Regular);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Regular);
                    break;
                case 1:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Bold);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Bold);
                    break;
                case 2:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Italic);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Italic);
                    break;
                case 3:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Italic | FontStyle.Bold);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Italic | FontStyle.Bold);
                    break;
                case 4:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Underline);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Underline);
                    break;
                case 5:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Bold | FontStyle.Underline);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Bold | FontStyle.Underline);
                    break;
                case 6:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Underline | FontStyle.Italic);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Underline | FontStyle.Italic);
                    break;
                case 7:
                    upperRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold);
                    lowerRichTextBox.Font = new Font(fontComboBox.SelectedItem.ToString(), 12, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold);
                    break;
                default:
                    break;
            }

        }
    }
}

