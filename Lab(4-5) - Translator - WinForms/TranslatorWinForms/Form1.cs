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
        private List<string[]> words = new List<string[]>();
        Dictionary<string, string> Dict;
        
        public Form1()
        {

            InitializeComponent();
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
            if (theDialog.ShowDialog() == DialogResult.OK)

                for (int i = 0; i < words.Count; i++)
                {
                    File.AppendAllText(theDialog.FileName.ToString(), words[i][0] + " " + words[i][1] + Environment.NewLine);
                }

        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            List<string[]> Lines = new List<string[]>();
            if (richTextBox1.Text.Contains('\n'))
            {
                string[] SplitLines = richTextBox1.Text.Split('\n');
                foreach (string a in SplitLines)
                {
                    string[] LineOfWords = a.Split(' ');
                    LineOfWords[LineOfWords.Length - 1] += " \n";
                    Lines.Add(LineOfWords);
                }
            }
            else
            {
                Lines.Add(richTextBox1.Text.Split(' '));
            }

            foreach (string[] LineOfWords in Lines)
            {
                foreach (string a in LineOfWords)
                {
                    if (Dict.ContainsKey(a))
                    {
                        richTextBox2.SelectionColor = Color.FromArgb(0, 0, 0);
                        richTextBox2.AppendText(Dict[a] + " ");
                    }
                    else if (Dict.ContainsValue(a))
                    {
                        richTextBox2.SelectionColor = Color.FromArgb(0, 0, 0);
                        richTextBox2.AppendText(Dict.FirstOrDefault(x => x.Value == a).Key + " ");
                    }
                    else if (a == "420")
                        richTextBox2.AppendText("kod Spalińskiego\n");
                    else
                    {
                        richTextBox2.SelectionColor = Color.FromArgb(255, 0, 0);
                        richTextBox2.AppendText(a + " ");
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

    }
}

