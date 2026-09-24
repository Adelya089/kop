namespace WinFormsControlLibrary1
{
    partial class MyComboBox
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
            comboBox = new ComboBox();
            SuspendLayout();
            // 
            // comboBox
            // 
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(100, 90);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(250, 28);
            comboBox.TabIndex = 0;
            // 
            // MyComboBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(comboBox);
            Name = "MyComboBox";
            Size = new Size(482, 402);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox;
    }
}
