namespace WinFormsControlLibrary1
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtonLoad = new Button();
            pictureBoxAvatar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).BeginInit();
            SuspendLayout();
            // 
            // ButtonLoad
            // 
            ButtonLoad.Anchor = AnchorStyles.Bottom;
            ButtonLoad.Location = new Point(114, 359);
            ButtonLoad.Name = "ButtonLoad";
            ButtonLoad.Size = new Size(285, 64);
            ButtonLoad.TabIndex = 0;
            ButtonLoad.Text = "button1";
            ButtonLoad.UseVisualStyleBackColor = true;
            ButtonLoad.Click += ButtonLoad_Click;
            // 
            // pictureBoxAvatar
            // 
            pictureBoxAvatar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxAvatar.Location = new Point(37, 27);
            pictureBoxAvatar.Name = "pictureBoxAvatar";
            pictureBoxAvatar.Size = new Size(434, 295);
            pictureBoxAvatar.TabIndex = 1;
            pictureBoxAvatar.TabStop = false;
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBoxAvatar);
            Controls.Add(ButtonLoad);
            Name = "UserControl1";
            Size = new Size(507, 450);
            ((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button ButtonLoad;
        private PictureBox pictureBoxAvatar;
    }
}
