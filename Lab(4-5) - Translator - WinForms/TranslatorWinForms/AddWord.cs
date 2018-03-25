using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TranslatorWinForms
{
    public partial class AddWord : Form
    {
        
        public AddWord(Form1 parent)
        {
            InitializeComponent();
            this.Owner = parent;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string [] langs = new string[2];
            langs[0] = textBox1.Text;
            langs[1] = textBox2.Text;
           
            if (langs[0].All(c => Char.IsLetter(c)) && langs[1].All(c => Char.IsLetter(c)))
                Form1.words.Add(langs);


            for (int i = 1; i < Form1.words.Count; i++)
            {
                if (!Form1.Dict.ContainsKey(Form1.words[i][0]))
                {
                    Form1.Dict.Add(Form1.words[i][0], Form1.words[i][1]);
                    Form1.listView1.Items.Add(
                        new ListViewItem(new[]
                        {
                            Form1.words[i][0],
                            Form1.words[i][1]
                        }));
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
