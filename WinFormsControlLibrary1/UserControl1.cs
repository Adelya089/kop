using System.ComponentModel;

namespace WinFormsControlLibrary1
{
    public partial class UserControl1 : UserControl
    {
        private event EventHandler? _avatarChanged;
        private event Action? _errorOccured;
        public string Error { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? Avatar
        {
            get
            {
                return pictureBoxAvatar.Image;
            }
            set
            {
                if (value == null) return;
                pictureBoxAvatar.Image = value;
            }
        }

        public event EventHandler AvatarChanged
        {
            add { _avatarChanged += value; }
            remove { _avatarChanged -= value; }
        }

        public event Action AnErrorOccurred
        {
            add { _errorOccured += value; }
            remove { _errorOccured -= value; }
        }

        public UserControl1()
        {
            InitializeComponent();
            Error = string.Empty;
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBoxAvatar.Image =
               Image.FromFile(ofd.FileName);
                    _avatarChanged?.Invoke(this, e);
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    _errorOccured?.Invoke();
                }
            }
        }
    }
}
