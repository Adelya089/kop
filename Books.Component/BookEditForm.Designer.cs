namespace Books.Component;

partial class BookEditForm
{
    private System.ComponentModel.IContainer? components = null;

    private Label labelTitle = null!;
    private Label labelDescription = null!;
    private Label labelGenre = null!;
    private Label labelPrice = null!;

    private TextBox textTitle = null!;
    private TextBox textDescription = null!;
    private ComboBox comboGenre = null!;
    private NumericUpDown numericPrice = null!;
    private Button buttonSave = null!;
    private Button buttonCancel = null!;

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
        labelTitle = new Label();
        labelDescription = new Label();
        labelGenre = new Label();
        labelPrice = new Label();
        textTitle = new TextBox();
        textDescription = new TextBox();
        comboGenre = new ComboBox();
        numericPrice = new NumericUpDown();
        buttonSave = new Button();
        buttonCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numericPrice).BeginInit();
        SuspendLayout();
        // 
        // labelTitle
        // 
        labelTitle.AutoSize = true;
        labelTitle.Location = new Point(23, 27);
        labelTitle.Name = "labelTitle";
        labelTitle.Size = new Size(80, 20);
        labelTitle.TabIndex = 0;
        labelTitle.Text = "Название:";
        // 
        // labelDescription
        // 
        labelDescription.AutoSize = true;
        labelDescription.Location = new Point(23, 80);
        labelDescription.Name = "labelDescription";
        labelDescription.Size = new Size(82, 20);
        labelDescription.TabIndex = 2;
        labelDescription.Text = "Описание:";
        // 
        // labelGenre
        // 
        labelGenre.AutoSize = true;
        labelGenre.Location = new Point(23, 233);
        labelGenre.Name = "labelGenre";
        labelGenre.Size = new Size(51, 20);
        labelGenre.TabIndex = 4;
        labelGenre.Text = "Жанр:";
        // 
        // labelPrice
        // 
        labelPrice.AutoSize = true;
        labelPrice.Location = new Point(23, 287);
        labelPrice.Name = "labelPrice";
        labelPrice.Size = new Size(86, 20);
        labelPrice.TabIndex = 6;
        labelPrice.Text = "Стоимость:";
        // 
        // textTitle
        // 
        textTitle.Location = new Point(137, 23);
        textTitle.Margin = new Padding(3, 4, 3, 4);
        textTitle.Name = "textTitle";
        textTitle.Size = new Size(365, 27);
        textTitle.TabIndex = 1;
        // 
        // textDescription
        // 
        textDescription.Location = new Point(137, 76);
        textDescription.Margin = new Padding(3, 4, 3, 4);
        textDescription.Multiline = true;
        textDescription.Name = "textDescription";
        textDescription.Size = new Size(365, 132);
        textDescription.TabIndex = 3;
        // 
        // comboGenre
        // 
        comboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
        comboGenre.FormattingEnabled = true;
        comboGenre.Location = new Point(137, 229);
        comboGenre.Margin = new Padding(3, 4, 3, 4);
        comboGenre.Name = "comboGenre";
        comboGenre.Size = new Size(365, 28);
        comboGenre.TabIndex = 5;
        // 
        // numericPrice
        // 
        numericPrice.DecimalPlaces = 2;
        numericPrice.Location = new Point(137, 283);
        numericPrice.Margin = new Padding(3, 4, 3, 4);
        numericPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numericPrice.Name = "numericPrice";
        numericPrice.Size = new Size(171, 27);
        numericPrice.TabIndex = 7;
        // 
        // buttonSave
        // 
        buttonSave.Font = new Font("Segoe UI", 10F);
        buttonSave.Location = new Point(137, 357);
        buttonSave.Margin = new Padding(3, 4, 3, 4);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(137, 60);
        buttonSave.TabIndex = 8;
        buttonSave.Text = "Сохранить";
        buttonSave.UseVisualStyleBackColor = true;
        buttonSave.Click += Save_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.DialogResult = DialogResult.Cancel;
        buttonCancel.Font = new Font("Segoe UI", 10F);
        buttonCancel.Location = new Point(313, 357);
        buttonCancel.Margin = new Padding(3, 4, 3, 4);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new Size(137, 60);
        buttonCancel.TabIndex = 9;
        buttonCancel.Text = "Отмена";
        buttonCancel.UseVisualStyleBackColor = true;
        // 
        // BookEditForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.None;
        CancelButton = buttonCancel;
        ClientSize = new Size(537, 443);
        Controls.Add(buttonCancel);
        Controls.Add(buttonSave);
        Controls.Add(numericPrice);
        Controls.Add(labelPrice);
        Controls.Add(comboGenre);
        Controls.Add(labelGenre);
        Controls.Add(textDescription);
        Controls.Add(labelDescription);
        Controls.Add(textTitle);
        Controls.Add(labelTitle);
        Margin = new Padding(3, 4, 3, 4);
        Name = "BookEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Книга";
        ((System.ComponentModel.ISupportInitialize)numericPrice).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}