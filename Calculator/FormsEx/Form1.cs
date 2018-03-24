using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsEx
{
    public partial class Form1 : Form
    {
        private double value = 0;
        private double operationValue = 0;
        private string operation;
        //ivate string valueStr = "";
        public Form1()
        {
            InitializeComponent();
            valueLabel.Text = value.ToString();
        }

        private void digit_Click(object sender, EventArgs e)
        {
            Button button;
            int val = 0;
            if (sender is Button)
            {
                button = sender as Button;
                val = Int32.Parse(button.Text);
            }
            
            if (value == 0)
                value += val;
            else
                value = value * 10 + val;
            valueLabel.Text = value.ToString();
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            if(value % 1 == 0)
            {
                value /= 10;
                if (value > 0)
                    value = Math.Floor(value);
                else
                    value = Math.Ceiling(value);
                valueLabel.Text = value.ToString();
            }
            else
            {
                value /= 10;
                valueLabel.Text = value.ToString();
            }
        }

        private void plusMinus_Click(object sender, EventArgs e)
        {
            value -= 2 * value;
            valueLabel.Text = value.ToString();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            value = 0;
            valueLabel.Text = value.ToString();
        }

        private void square_Click(object sender, EventArgs e)
        {
            value = value * value;
            valueLabel.Text = value.ToString();
        }

        private void squareRoot_Click(object sender, EventArgs e)
        {
            value = Math.Sqrt(value);
            valueLabel.Text = value.ToString();
        }

        private void oneOver_Click(object sender, EventArgs e)
        {
            value = 1 / value;
            valueLabel.Text = value.ToString();
        }

        private void operation_click(object sender, EventArgs e)
        {
            Button button;
            if (sender is Button)
            {
                button = sender as Button;
                this.operation = (button.Name);
            }
            operationValue = value;
            value = 0;
            valueLabel.Text = value.ToString();
        }

        private void equals_Click(object sender, EventArgs e)
        {
            //gimme result
            if (operation == "divide")
                value = operationValue / value;
            if (operation == "plus")
                value = operationValue + value;
            if (operation == "minus")
                value = operationValue - value;
            if (operation == "multiply")
                value = operationValue * value;

            valueLabel.Text = value.ToString();
                
        }
    }
}
