namespace LibraryHost;

partial class ExtensionReportsForm
{
    private System.ComponentModel.IContainer? components = null;
    private GroupBox groupText = null!;
    private GroupBox groupPie = null!;
    private GroupBox groupTable = null!;
    private ComboBox comboText = null!;
    private ComboBox comboPie = null!;
    private ComboBox comboTable = null!;
    private Button buttonText = null!;
    private Button buttonPie = null!;
    private Button buttonTable = null!;
    private Label labelText = null!;
    private Label labelPie = null!;
    private Label labelTable = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        groupText = new GroupBox();
        groupPie = new GroupBox();
        groupTable = new GroupBox();
        comboText = new ComboBox();
        comboPie = new ComboBox();
        comboTable = new ComboBox();
        buttonText = new Button();
        buttonPie = new Button();
        buttonTable = new Button();
        labelText = new Label();
        labelPie = new Label();
        labelTable = new Label();
        groupText.SuspendLayout();
        groupPie.SuspendLayout();
        groupTable.SuspendLayout();
        SuspendLayout();

        groupText.Controls.Add(labelText);
        groupText.Controls.Add(comboText);
        groupText.Controls.Add(buttonText);
        groupText.Location = new Point(20, 20);
        groupText.Name = "groupText";
        groupText.Size = new Size(650, 120);
        groupText.TabIndex = 0;
        groupText.TabStop = false;
        groupText.Text = "Отчет 1";

        labelText.AutoSize = true;
        labelText.Location = new Point(20, 28);
        labelText.Name = "labelText";
        labelText.Size = new Size(323, 20);
        labelText.Text = "Бесплатные книги: название и описание";

        comboText.DropDownStyle = ComboBoxStyle.DropDownList;
        comboText.Location = new Point(20, 62);
        comboText.Name = "comboText";
        comboText.Size = new Size(250, 28);
        comboText.TabIndex = 0;

        buttonText.Location = new Point(300, 58);
        buttonText.Name = "buttonText";
        buttonText.Size = new Size(190, 36);
        buttonText.TabIndex = 1;
        buttonText.Text = "Сформировать";
        buttonText.UseVisualStyleBackColor = true;
        buttonText.Click += ButtonText_Click;

        groupPie.Controls.Add(labelPie);
        groupPie.Controls.Add(comboPie);
        groupPie.Controls.Add(buttonPie);
        groupPie.Location = new Point(20, 155);
        groupPie.Name = "groupPie";
        groupPie.Size = new Size(650, 120);
        groupPie.TabIndex = 1;
        groupPie.TabStop = false;
        groupPie.Text = "Отчет 2";

        labelPie.AutoSize = true;
        labelPie.Location = new Point(20, 28);
        labelPie.Name = "labelPie";
        labelPie.Size = new Size(371, 20);
        labelPie.Text = "Круговая диаграмма: бесплатные книги по жанрам";

        comboPie.DropDownStyle = ComboBoxStyle.DropDownList;
        comboPie.Location = new Point(20, 62);
        comboPie.Name = "comboPie";
        comboPie.Size = new Size(250, 28);
        comboPie.TabIndex = 0;

        buttonPie.Location = new Point(300, 58);
        buttonPie.Name = "buttonPie";
        buttonPie.Size = new Size(190, 36);
        buttonPie.TabIndex = 1;
        buttonPie.Text = "Сформировать";
        buttonPie.UseVisualStyleBackColor = true;
        buttonPie.Click += ButtonPie_Click;

        groupTable.Controls.Add(labelTable);
        groupTable.Controls.Add(comboTable);
        groupTable.Controls.Add(buttonTable);
        groupTable.Location = new Point(20, 290);
        groupTable.Name = "groupTable";
        groupTable.Size = new Size(650, 120);
        groupTable.TabIndex = 2;
        groupTable.TabStop = false;
        groupTable.Text = "Отчет 3";

        labelTable.AutoSize = true;
        labelTable.Location = new Point(20, 28);
        labelTable.Name = "labelTable";
        labelTable.Size = new Size(357, 20);
        labelTable.Text = "Все книги: идентификатор, название, жанр, стоимость";

        comboTable.DropDownStyle = ComboBoxStyle.DropDownList;
        comboTable.Location = new Point(20, 62);
        comboTable.Name = "comboTable";
        comboTable.Size = new Size(250, 28);
        comboTable.TabIndex = 0;

        buttonTable.Location = new Point(300, 58);
        buttonTable.Name = "buttonTable";
        buttonTable.Size = new Size(190, 36);
        buttonTable.TabIndex = 1;
        buttonTable.Text = "Сформировать";
        buttonTable.UseVisualStyleBackColor = true;
        buttonTable.Click += ButtonTable_Click;

        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(690, 435);
        Controls.Add(groupTable);
        Controls.Add(groupPie);
        Controls.Add(groupText);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ExtensionReportsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Расширения отчетов";
        groupText.ResumeLayout(false);
        groupText.PerformLayout();
        groupPie.ResumeLayout(false);
        groupPie.PerformLayout();
        groupTable.ResumeLayout(false);
        groupTable.PerformLayout();
        ResumeLayout(false);
    }
}
