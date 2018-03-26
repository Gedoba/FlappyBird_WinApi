using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TranslatorWinForms
{
    public partial class AddWord : Form
    {
        bool error = false;

        public AddWord(Form1 parent)
        {
            InitializeComponent();
            this.Owner = parent;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            error = true;
            if (String.IsNullOrWhiteSpace(textBox1.Text))
                errorProvider1.SetError(this.textBox1, "Can't be empty");
            else if (!textBox1.Text.All(c => Char.IsLetter(c)))
                errorProvider1.SetError(this.textBox1, "Contains forbidden signs");
            else
            {
                errorProvider1.SetError(this.textBox1, String.Empty);
                error = false;
            }

        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            error = true;
            if (String.IsNullOrWhiteSpace(textBox2.Text))
                errorProvider2.SetError(this.textBox2, "Can't be empty");
            else if (!textBox1.Text.All(c => Char.IsLetter(c)))
                errorProvider2.SetError(this.textBox2, "Contains forbidden signs");
            else
            {
                errorProvider2.SetError(this.textBox2, String.Empty);
                error = false;
            }

        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if(error)
            {
                MessageBox.Show("Wrong value", "Error");
                return;
            }
            string [] langs = new string[2];
            string [] columnNames = new string[2];
            columnNames[0] = "English";
            columnNames[1] = "Polish";

            langs[0] = textBox1.Text;
            langs[1] = textBox2.Text;
           
            if(Form1.words.Count == 0)
            {
                StringComparer comparer = StringComparer.CurrentCultureIgnoreCase;
                Form1.Dict = new Dictionary<string, string>(comparer);
                Form1.words.Add(columnNames);

            }

            if (langs[0].All(c => Char.IsLetter(c)) && langs[1].All(c => Char.IsLetter(c)))
                Form1.words.Add(langs);


            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
