namespace BookReport.Component
{
    partial class BookReportControl
    {
        private System.ComponentModel.IContainer? components = null;

        private ComboBox comboGenre = null!;
        private Button buttonBuild = null!;
        private DataGridView dataGridView1 = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            comboGenre = new ComboBox();
            buttonBuild = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboGenre
            // 
            comboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            comboGenre.FormattingEnabled = true;
            comboGenre.Location = new Point(15, 15);
            comboGenre.Name = "comboGenre";
            comboGenre.Size = new Size(220, 28);
            comboGenre.TabIndex = 0;
            // 
            // buttonBuild
            // 
            buttonBuild.Location = new Point(250, 15);
            buttonBuild.Name = "buttonBuild";
            buttonBuild.Size = new Size(180, 44);
            buttonBuild.TabIndex = 1;
            buttonBuild.Text = "Сформировать отчет";
            buttonBuild.UseVisualStyleBackColor = true;
            buttonBuild.Click += Build_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 65);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(760, 390);
            dataGridView1.TabIndex = 2;
            // 
            // BookReportControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(dataGridView1);
            Controls.Add(buttonBuild);
            Controls.Add(comboGenre);
            Name = "BookReportControl";
            Size = new Size(800, 480);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }
    }
}