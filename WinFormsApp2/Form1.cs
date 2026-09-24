using WinFormsControlLibrary1;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            myComboBox.AddItem("Иван");
            myComboBox.AddItem("Пётр");
            myComboBox.AddItem("Сергей");

            myTextBox.Pattern = @"^\d{2}\.\d{2}\.\d{2}$";
            myTextBox.SetExample("25.12.2025");

            myTextBox.ValueChanged += (s, e) =>
            {
                try
                {
                    string val = myTextBox.Value;
                    Text = $"Дата: {val}";
                }
                catch (CustomException)
                {
                    return;
                }
            };
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            myComboBox.AddItem(textBox.Text);
            textBox.Text = "";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            myComboBox.ClearItems();
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Выбрано: {myComboBox.SelIteme}", "Selected");
        }

        private void buttonSetDate_Click(object sender, EventArgs e)
        {
            try
            {
                myTextBox.Value = "25.12.2025";
                MessageBox.Show("Дата установлена!");
            }
            catch (CustomException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonCheckDate_Click(object sender, EventArgs e)
        {
            try
            {
                string val = myTextBox.Value;
                MessageBox.Show($"Дата верная: {val}");
            }
            catch (CustomException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
