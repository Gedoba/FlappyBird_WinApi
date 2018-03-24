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
            theDialog.InitialDirectory = "..//..//";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string line in File.ReadAllLines(theDialog.FileName.ToString()))
                {

                    foreach (char c in line)
                    {
                        if (Char.IsLetter(c))
                        {
                            words.Add(line.Split(' '));
                            break;
                        }
                        else continue;
                    }
                }
                listView1.View = View.Details;
                listView1.Columns.Add(words[0][0]);
                listView1.Columns.Add(words[0][1]);
                

                for (int i = 1; i < words.Count; i++)
                {
                    listView1.Items.Add(
                        new ListViewItem(new[]
                        {
                        words[i][0],
                        words[i][1]
                        }));
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
            theDialog.InitialDirectory = "..//..//";
            if (theDialog.ShowDialog() == DialogResult.OK)

                for (int i = 0; i < words.Count; i++)
            {
                File.AppendAllText(theDialog.FileName.ToString(), words[i][0] + " " + words[i][1] + Environment.NewLine);
            }

        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }  
}

