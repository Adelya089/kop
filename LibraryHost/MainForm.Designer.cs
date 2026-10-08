namespace LibraryHost;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    private MenuStrip menuStrip1 = null!;
    private ToolStripMenuItem directoriesToolStripMenuItem = null!;
    private ToolStripMenuItem reportsToolStripMenuItem = null!;
    private TabControl tabControl1 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        directoriesToolStripMenuItem = new ToolStripMenuItem();
        reportsToolStripMenuItem = new ToolStripMenuItem();
        tabControl1 = new TabControl();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.ImageScalingSize = new Size(20, 20);
        menuStrip1.Items.AddRange(new ToolStripItem[] { directoriesToolStripMenuItem, reportsToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Padding = new Padding(7, 3, 0, 3);
        menuStrip1.Size = new Size(1029, 30);
        menuStrip1.TabIndex = 1;
        // 
        // directoriesToolStripMenuItem
        // 
        directoriesToolStripMenuItem.Name = "directoriesToolStripMenuItem";
        directoriesToolStripMenuItem.Size = new Size(117, 24);
        directoriesToolStripMenuItem.Text = "Справочники";
        // 
        // reportsToolStripMenuItem
        // 
        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        reportsToolStripMenuItem.Size = new Size(73, 24);
        reportsToolStripMenuItem.Text = "Отчеты";
        // 
        // tabControl1
        // 
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 30);
        tabControl1.Margin = new Padding(3, 4, 3, 4);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(1029, 703);
        tabControl1.TabIndex = 0;
        tabControl1.DoubleClick += TabControl1_DoubleClick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1029, 733);
        Controls.Add(tabControl1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Margin = new Padding(3, 4, 3, 4);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Учет книг в библиотеке";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
