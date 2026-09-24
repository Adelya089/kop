namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button buttonCheckDate;
            myComboBox = new WinFormsControlLibrary1.MyComboBox();
            userControl11 = new WinFormsControlLibrary1.UserControl1();
            buttonClear = new Button();
            buttonGet = new Button();
            buttonAdd = new Button();
            textBox = new TextBox();
            myTextBox = new WinFormsControlLibrary1.MyTextBox();
            buttonSetDate = new Button();
            buttonCheckDate = new Button();
            SuspendLayout();
            // 
            // buttonCheckDate
            // 
            buttonCheckDate.Location = new Point(697, 294);
            buttonCheckDate.Name = "buttonCheckDate";
            buttonCheckDate.Size = new Size(184, 29);
            buttonCheckDate.TabIndex = 9;
            buttonCheckDate.Text = "Проверить дату";
            buttonCheckDate.UseVisualStyleBackColor = true;
            buttonCheckDate.Click += buttonCheckDate_Click;
            // 
            // myComboBox
            // 
            myComboBox.Location = new Point(33, 330);
            myComboBox.Name = "myComboBox";
            myComboBox.SelIteme = "";
            myComboBox.Size = new Size(398, 243);
            myComboBox.TabIndex = 0;
            // 
            // userControl11
            // 
            userControl11.Avatar = null;
            userControl11.Location = new Point(103, 50);
            userControl11.Name = "userControl11";
            userControl11.Size = new Size(293, 220);
            userControl11.TabIndex = 1;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(111, 579);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(109, 48);
            buttonClear.TabIndex = 2;
            buttonClear.Text = "Очистить";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonGet
            // 
            buttonGet.Location = new Point(248, 579);
            buttonGet.Name = "buttonGet";
            buttonGet.Size = new Size(183, 48);
            buttonGet.TabIndex = 3;
            buttonGet.Text = "Выбраное значение";
            buttonGet.UseVisualStyleBackColor = true;
            buttonGet.Click += buttonGet_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(290, 518);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(106, 44);
            buttonAdd.TabIndex = 4;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textBox
            // 
            textBox.Location = new Point(111, 527);
            textBox.Name = "textBox";
            textBox.Size = new Size(142, 27);
            textBox.TabIndex = 5;
            // 
            // myTextBox
            // 
            myTextBox.Location = new Point(629, 36);
            myTextBox.Name = "myTextBox";
            myTextBox.Pattern = "^\\d{2}\\.\\d{2}\\.\\d{4}$";
            myTextBox.Size = new Size(446, 325);
            myTextBox.TabIndex = 6;
            // 
            // buttonSetDate
            // 
            buttonSetDate.Location = new Point(697, 219);
            buttonSetDate.Name = "buttonSetDate";
            buttonSetDate.Size = new Size(184, 29);
            buttonSetDate.TabIndex = 7;
            buttonSetDate.Text = "Установить дату";
            buttonSetDate.UseVisualStyleBackColor = true;
            buttonSetDate.Click += buttonSetDate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1149, 658);
            Controls.Add(buttonCheckDate);
            Controls.Add(buttonSetDate);
            Controls.Add(myTextBox);
            Controls.Add(textBox);
            Controls.Add(buttonAdd);
            Controls.Add(buttonGet);
            Controls.Add(buttonClear);
            Controls.Add(userControl11);
            Controls.Add(myComboBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WinFormsControlLibrary1.MyComboBox myComboBox;
        private WinFormsControlLibrary1.UserControl1 userControl11;
        private Button buttonClear;
        private Button buttonGet;
        private Button buttonAdd;
        private TextBox textBox;
        private WinFormsControlLibrary1.MyTextBox myTextBox;
        private Button buttonSetDate;
    }
}
