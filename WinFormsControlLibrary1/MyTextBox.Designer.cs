namespace WinFormsControlLibrary1
{
    partial class MyTextBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            textBox = new TextBox();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Location = new Point(53, 102);
            textBox.Name = "textBox";
            textBox.Size = new Size(240, 27);
            textBox.TabIndex = 0;
            // 
            // MyTextBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox);
            Name = "MyTextBox";
            Size = new Size(357, 260);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox;
    }
}
