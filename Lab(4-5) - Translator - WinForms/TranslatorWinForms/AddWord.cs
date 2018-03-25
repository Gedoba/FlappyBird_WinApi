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
           
            if(Form1.words.Count == 0)
            {
                StringComparer comparer = StringComparer.CurrentCultureIgnoreCase;
                Form1.Dict = new Dictionary<string, string>(comparer);
            }

            if (langs[0].All(c => Char.IsLetter(c)) && langs[1].All(c => Char.IsLetter(c)))
                Form1.words.Add(langs);


            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
