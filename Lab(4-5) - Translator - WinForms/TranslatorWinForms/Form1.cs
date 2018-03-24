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
            theDialog.InitialDirectory = "C:\\Users\\286327\\source\\repos\\TranslatorWinForms\\TranslatorWinForms";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show();
            }

            foreach (string line in File.ReadAllLines(theDialog.FileName.ToString()))
            {
                
                foreach(char c in line)
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
            for (int i = 0; i<words.Count; i++)
            {
                listView1.Items.Add(
                    new ListViewItem(new[]
                    {
                        words[i][0],
                        words[i][1]
                    }));
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i=0; i<words.Count; i++)
            {
                File.AppendAllText("..//..//dictionaryES1.txt", words[i][0] + " " + words[i][1] + Environment.NewLine);
            }
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
