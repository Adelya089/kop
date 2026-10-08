namespace LibraryHost;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    private MenuStrip menuStrip1 = null!;
    private ToolStripMenuItem directoriesToolStripMenuItem = null!;
    private ToolStripMenuItem reportsToolStripMenuItem = null!;
    private TabControl tabControls = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        directoriesToolStripMenuItem = new ToolStripMenuItem();
        reportsToolStripMenuItem = new ToolStripMenuItem();
        tabControls = new TabControl();
        menuStrip1.SuspendLayout();
        SuspendLayout();

        menuStrip1.Items.AddRange(new ToolStripItem[] { directoriesToolStripMenuItem, reportsToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(900, 24);
        menuStrip1.TabIndex = 0;

        directoriesToolStripMenuItem.Name = "directoriesToolStripMenuItem";
        directoriesToolStripMenuItem.Text = "Справочники";

        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        reportsToolStripMenuItem.Text = "Отчеты";

        tabControls.Dock = DockStyle.Fill;
        tabControls.Location = new Point(0, 24);
        tabControls.Name = "tabControls";
        tabControls.Size = new Size(900, 526);
        tabControls.TabIndex = 1;
        tabControls.DoubleClick += TabControls_DoubleClick;

        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(900, 550);
        Controls.Add(tabControls);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Учет книг в библиотеке";

        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
