using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsControlLibrary1
{
    public partial class MyComboBox : UserControl
    {
        public event EventHandler SelectedChanged;
        public MyComboBox()
        {
            InitializeComponent();
            comboBox.SelectedIndexChanged += (s, e) =>
            {
                SelectedChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        public void AddItem(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            if (!comboBox.Items.Contains(value)) comboBox.Items.Add(value);
        }
        public void ClearItems()
        {
            comboBox.Items.Clear();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SelIteme
        {
            get
            {
                if (comboBox.SelectedItem == null) return "";
                return comboBox.SelectedItem.ToString();
            }
            set
            {
                if (value == null) return;
                if (string.IsNullOrEmpty(value))
                {
                    comboBox.SelectedIndex = -1;
                    return;
                }
                if (comboBox.Items.Contains(value)) comboBox.SelectedItem = value;
            }
        }
    }
}
