namespace FormsEx
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.percent = new System.Windows.Forms.Button();
            this.squareRoot = new System.Windows.Forms.Button();
            this.square = new System.Windows.Forms.Button();
            this.oneOver = new System.Windows.Forms.Button();
            this.clearEntry = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.backspace = new System.Windows.Forms.Button();
            this.divide = new System.Windows.Forms.Button();
            this.seven = new System.Windows.Forms.Button();
            this.eight = new System.Windows.Forms.Button();
            this.nine = new System.Windows.Forms.Button();
            this.multiply = new System.Windows.Forms.Button();
            this.four = new System.Windows.Forms.Button();
            this.five = new System.Windows.Forms.Button();
            this.six = new System.Windows.Forms.Button();
            this.subtract = new System.Windows.Forms.Button();
            this.one = new System.Windows.Forms.Button();
            this.two = new System.Windows.Forms.Button();
            this.three = new System.Windows.Forms.Button();
            this.plus = new System.Windows.Forms.Button();
            this.plusMinus = new System.Windows.Forms.Button();
            this.zero = new System.Windows.Forms.Button();
            this.dot = new System.Windows.Forms.Button();
            this.equals = new System.Windows.Forms.Button();
            this.valueLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // percent
            // 
            this.percent.Location = new System.Drawing.Point(2, 85);
            this.percent.Margin = new System.Windows.Forms.Padding(2);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(82, 61);
            this.percent.TabIndex = 0;
            this.percent.Text = "%";
            this.percent.UseVisualStyleBackColor = true;
            // 
            // squareRoot
            // 
            this.squareRoot.Location = new System.Drawing.Point(88, 85);
            this.squareRoot.Margin = new System.Windows.Forms.Padding(2);
            this.squareRoot.Name = "squareRoot";
            this.squareRoot.Size = new System.Drawing.Size(82, 61);
            this.squareRoot.TabIndex = 1;
            this.squareRoot.Text = "sqrt";
            this.squareRoot.UseVisualStyleBackColor = true;
            this.squareRoot.Click += new System.EventHandler(this.squareRoot_Click);
            // 
            // square
            // 
            this.square.Location = new System.Drawing.Point(174, 85);
            this.square.Margin = new System.Windows.Forms.Padding(2);
            this.square.Name = "square";
            this.square.Size = new System.Drawing.Size(82, 61);
            this.square.TabIndex = 2;
            this.square.Text = "x^2";
            this.square.UseVisualStyleBackColor = true;
            this.square.Click += new System.EventHandler(this.square_Click);
            // 
            // oneOver
            // 
            this.oneOver.Location = new System.Drawing.Point(260, 85);
            this.oneOver.Margin = new System.Windows.Forms.Padding(2);
            this.oneOver.Name = "oneOver";
            this.oneOver.Size = new System.Drawing.Size(82, 61);
            this.oneOver.TabIndex = 3;
            this.oneOver.Text = "1/x";
            this.oneOver.UseVisualStyleBackColor = true;
            this.oneOver.Click += new System.EventHandler(this.oneOver_Click);
            // 
            // clearEntry
            // 
            this.clearEntry.Location = new System.Drawing.Point(2, 150);
            this.clearEntry.Margin = new System.Windows.Forms.Padding(2);
            this.clearEntry.Name = "clearEntry";
            this.clearEntry.Size = new System.Drawing.Size(82, 61);
            this.clearEntry.TabIndex = 4;
            this.clearEntry.Text = "CE";
            this.clearEntry.UseVisualStyleBackColor = true;
            this.clearEntry.Click += new System.EventHandler(this.clear_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(88, 150);
            this.clear.Margin = new System.Windows.Forms.Padding(2);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(82, 61);
            this.clear.TabIndex = 5;
            this.clear.Text = "C";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // backspace
            // 
            this.backspace.Location = new System.Drawing.Point(174, 150);
            this.backspace.Margin = new System.Windows.Forms.Padding(2);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(82, 61);
            this.backspace.TabIndex = 6;
            this.backspace.Text = "Backspace";
            this.backspace.UseVisualStyleBackColor = true;
            this.backspace.Click += new System.EventHandler(this.backspace_Click);
            // 
            // divide
            // 
            this.divide.Location = new System.Drawing.Point(260, 150);
            this.divide.Margin = new System.Windows.Forms.Padding(2);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(82, 61);
            this.divide.TabIndex = 7;
            this.divide.Text = "/";
            this.divide.UseVisualStyleBackColor = true;
            this.divide.Click += new System.EventHandler(this.operation_click);
            // 
            // seven
            // 
            this.seven.Location = new System.Drawing.Point(2, 215);
            this.seven.Margin = new System.Windows.Forms.Padding(2);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(82, 61);
            this.seven.TabIndex = 8;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = true;
            this.seven.Click += new System.EventHandler(this.digit_Click);
            // 
            // eight
            // 
            this.eight.Location = new System.Drawing.Point(88, 215);
            this.eight.Margin = new System.Windows.Forms.Padding(2);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(82, 61);
            this.eight.TabIndex = 9;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = true;
            this.eight.Click += new System.EventHandler(this.digit_Click);
            // 
            // nine
            // 
            this.nine.Location = new System.Drawing.Point(174, 215);
            this.nine.Margin = new System.Windows.Forms.Padding(2);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(82, 61);
            this.nine.TabIndex = 10;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = true;
            this.nine.Click += new System.EventHandler(this.digit_Click);
            // 
            // multiply
            // 
            this.multiply.Location = new System.Drawing.Point(260, 215);
            this.multiply.Margin = new System.Windows.Forms.Padding(2);
            this.multiply.Name = "multiply";
            this.multiply.Size = new System.Drawing.Size(82, 61);
            this.multiply.TabIndex = 11;
            this.multiply.Text = "*";
            this.multiply.UseVisualStyleBackColor = true;
            this.multiply.Click += new System.EventHandler(this.operation_click);
            // 
            // four
            // 
            this.four.Location = new System.Drawing.Point(2, 280);
            this.four.Margin = new System.Windows.Forms.Padding(2);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(82, 61);
            this.four.TabIndex = 12;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = true;
            this.four.Click += new System.EventHandler(this.digit_Click);
            // 
            // five
            // 
            this.five.Location = new System.Drawing.Point(88, 280);
            this.five.Margin = new System.Windows.Forms.Padding(2);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(82, 61);
            this.five.TabIndex = 13;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.Click += new System.EventHandler(this.digit_Click);
            // 
            // six
            // 
            this.six.Location = new System.Drawing.Point(174, 280);
            this.six.Margin = new System.Windows.Forms.Padding(2);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(82, 61);
            this.six.TabIndex = 14;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = true;
            this.six.Click += new System.EventHandler(this.digit_Click);
            // 
            // subtract
            // 
            this.subtract.Location = new System.Drawing.Point(260, 280);
            this.subtract.Margin = new System.Windows.Forms.Padding(2);
            this.subtract.Name = "minus";
            this.subtract.Size = new System.Drawing.Size(82, 61);
            this.subtract.TabIndex = 15;
            this.subtract.Text = "-";
            this.subtract.UseVisualStyleBackColor = true;
            this.subtract.Click += new System.EventHandler(this.operation_click);
            // 
            // one
            // 
            this.one.Location = new System.Drawing.Point(2, 345);
            this.one.Margin = new System.Windows.Forms.Padding(2);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(82, 61);
            this.one.TabIndex = 16;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = true;
            this.one.Click += new System.EventHandler(this.digit_Click);
            // 
            // two
            // 
            this.two.Location = new System.Drawing.Point(88, 345);
            this.two.Margin = new System.Windows.Forms.Padding(2);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(82, 61);
            this.two.TabIndex = 17;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = true;
            this.two.Click += new System.EventHandler(this.digit_Click);
            // 
            // three
            // 
            this.three.Location = new System.Drawing.Point(174, 345);
            this.three.Margin = new System.Windows.Forms.Padding(2);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(82, 61);
            this.three.TabIndex = 18;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.Click += new System.EventHandler(this.digit_Click);
            // 
            // plus
            // 
            this.plus.Location = new System.Drawing.Point(260, 345);
            this.plus.Margin = new System.Windows.Forms.Padding(2);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(82, 61);
            this.plus.TabIndex = 19;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.operation_click);
            // 
            // plusMinus
            // 
            this.plusMinus.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.plusMinus.Location = new System.Drawing.Point(2, 410);
            this.plusMinus.Margin = new System.Windows.Forms.Padding(2);
            this.plusMinus.Name = "plusMinus";
            this.plusMinus.Size = new System.Drawing.Size(82, 61);
            this.plusMinus.TabIndex = 20;
            this.plusMinus.Text = "+/-";
            this.plusMinus.UseVisualStyleBackColor = true;
            this.plusMinus.Click += new System.EventHandler(this.plusMinus_Click);
            // 
            // zero
            // 
            this.zero.Location = new System.Drawing.Point(88, 410);
            this.zero.Margin = new System.Windows.Forms.Padding(2);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(82, 61);
            this.zero.TabIndex = 21;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = true;
            this.zero.Click += new System.EventHandler(this.digit_Click);
            // 
            // dot
            // 
            this.dot.Location = new System.Drawing.Point(174, 410);
            this.dot.Margin = new System.Windows.Forms.Padding(2);
            this.dot.Name = "dot";
            this.dot.Size = new System.Drawing.Size(82, 61);
            this.dot.TabIndex = 22;
            this.dot.Text = ".";
            this.dot.UseVisualStyleBackColor = true;
            // 
            // equals
            // 
            this.equals.Location = new System.Drawing.Point(260, 410);
            this.equals.Margin = new System.Windows.Forms.Padding(2);
            this.equals.Name = "equals";
            this.equals.Size = new System.Drawing.Size(82, 61);
            this.equals.TabIndex = 23;
            this.equals.Text = "=";
            this.equals.UseVisualStyleBackColor = true;
            this.equals.Click += new System.EventHandler(this.equals_Click);
            // 
            // valueLabel
            // 
            this.valueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.valueLabel.AutoSize = true;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.Location = new System.Drawing.Point(286, 3);
            this.valueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.valueLabel.MaximumSize = new System.Drawing.Size(300, 55);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.valueLabel.Size = new System.Drawing.Size(51, 55);
            this.valueLabel.TabIndex = 24;
            this.valueLabel.Text = "0";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.valueLabel);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(340, 73);
            this.flowLayoutPanel1.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 474);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.equals);
            this.Controls.Add(this.dot);
            this.Controls.Add(this.zero);
            this.Controls.Add(this.plusMinus);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.three);
            this.Controls.Add(this.two);
            this.Controls.Add(this.one);
            this.Controls.Add(this.subtract);
            this.Controls.Add(this.six);
            this.Controls.Add(this.five);
            this.Controls.Add(this.four);
            this.Controls.Add(this.multiply);
            this.Controls.Add(this.nine);
            this.Controls.Add(this.eight);
            this.Controls.Add(this.seven);
            this.Controls.Add(this.divide);
            this.Controls.Add(this.backspace);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.clearEntry);
            this.Controls.Add(this.oneOver);
            this.Controls.Add(this.square);
            this.Controls.Add(this.squareRoot);
            this.Controls.Add(this.percent);
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.Text = "Calculator";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button percent;
        private System.Windows.Forms.Button squareRoot;
        private System.Windows.Forms.Button square;
        private System.Windows.Forms.Button oneOver;
        private System.Windows.Forms.Button clearEntry;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button backspace;
        private System.Windows.Forms.Button divide;
        private System.Windows.Forms.Button seven;
        private System.Windows.Forms.Button eight;
        private System.Windows.Forms.Button nine;
        private System.Windows.Forms.Button multiply;
        private System.Windows.Forms.Button four;
        private System.Windows.Forms.Button five;
        private System.Windows.Forms.Button six;
        private System.Windows.Forms.Button subtract;
        private System.Windows.Forms.Button one;
        private System.Windows.Forms.Button two;
        private System.Windows.Forms.Button three;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button plusMinus;
        private System.Windows.Forms.Button zero;
        private System.Windows.Forms.Button dot;
        private System.Windows.Forms.Button equals;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

