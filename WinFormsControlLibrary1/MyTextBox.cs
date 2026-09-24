using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsControlLibrary1
{
    public partial class MyTextBox : UserControl
    {
        private string _pattern = @"^\d{2}\.\d{2}\.\d{2}$";
        private ToolTip _toolTip = new ToolTip();

        public event EventHandler ValueChanged;

        public MyTextBox()
        {
            InitializeComponent();

            textBox.TextChanged += (s, e) =>
            {
                ValueChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Pattern
        {
            get => _pattern;
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                try
                {
                    Regex.IsMatch("", value);
                    _pattern = value;
                }
                catch (ArgumentException)
                {
                    throw new CustomException($"Невалидный шаблон regex: {value}");
                }
            }
        }

        public void SetExample(string example)
        {
            _toolTip.SetToolTip(textBox, $"Пример: {example}");
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Value
        {
            get
            {
                if (string.IsNullOrEmpty(_pattern))
                    throw new CustomException("Шаблон не задан");

                if (string.IsNullOrEmpty(textBox.Text))
                    throw new CustomException("Значение не может быть пустым");

                if (!Regex.IsMatch(textBox.Text, _pattern))
                    throw new CustomException($"Значение не соответствует шаблону: {_pattern}");

                DateTime dateTime;
                if (!DateTime.TryParse(textBox.Text, out dateTime))
                {
                    throw new CustomException("Несуществующая дата");
                }

                return textBox.Text;
            }
            set
            {
                if (value == null) return;

                if (string.IsNullOrEmpty(_pattern)) return;

                if (string.IsNullOrEmpty(value))
                {
                    textBox.Text = "";
                    return;
                }

                if (!Regex.IsMatch(value, _pattern))
                    throw new CustomException($"Значение не соответствует шаблону: {_pattern}");


                textBox.Text = value;
            }
        }
    }
}

