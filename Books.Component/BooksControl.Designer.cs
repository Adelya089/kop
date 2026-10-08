namespace Books.Component
{
    partial class BooksControl
    {
        private System.ComponentModel.IContainer? components = null;
        private TreeView treeView1 = null!;
        private ContextMenuStrip contextMenuStrip1 = null!;
        private ToolStripMenuItem addToolStripMenuItem = null!;
        private ToolStripMenuItem editToolStripMenuItem = null!;
        private ToolStripMenuItem deleteToolStripMenuItem = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            treeView1 = new TreeView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(3, 4, 3, 4);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(800, 533);
            treeView1.TabIndex = 0;
            treeView1.KeyDown += Tree_KeyDown;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(148, 76);
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(147, 24);
            addToolStripMenuItem.Text = "Добавить";
            addToolStripMenuItem.Click += Add_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(147, 24);
            editToolStripMenuItem.Text = "Изменить";
            editToolStripMenuItem.Click += Edit_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(147, 24);
            deleteToolStripMenuItem.Text = "Удалить";
            deleteToolStripMenuItem.Click += Delete_Click;
            // 
            // BooksControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeView1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "BooksControl";
            Size = new Size(800, 533);
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}