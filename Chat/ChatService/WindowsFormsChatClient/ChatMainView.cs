using System;
using System.ServiceModel;
using System.Windows.Forms;
using WindowsFormsChatClient.CallbackHandlers;
using WindowsFormsChatClient.ServiceReference;

namespace WindowsFormsChatClient
{
    public partial class ChatMainView : Form
    {
        private readonly ChatMainServiceClient _mainServiceClient;
        private bool _isRegistration;

        public ChatMainView()
        {
            InitializeComponent();
            _mainServiceClient = new ChatMainServiceClient(new InstanceContext(new ChatCallbackHandler(this)));
        }

        // Регистрация
        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            _isRegistration = true;
        }

        // Ок при Регистрации/Входе
        private void button3_Click(object sender, EventArgs e)
        {
            if (_isRegistration)
            {
                if(!_mainServiceClient.UserRegistration(textBox1.Text, textBox2.Text)) MessageBox.Show("Такой пользователь уже есть");
                _isRegistration = false;
            }

            if (_mainServiceClient.LoginPasswordValidation(textBox1.Text, textBox2.Text))
            {
                button1.Text = "Выход";
                Text = "Пользователь " + textBox1.Text;
                panel1.Visible = false;
                panel2.Visible = true;
                button2.Visible = false;
                label4.Visible = true;
                listBox1.Visible = true;
                _mainServiceClient.UserLogin(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
                panel1.Visible = false;
            }
        }

        // Вход/Выход
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Вход")
            {
                panel1.Visible = true;
            }
            else
            {
                Text = "Лучший чат";
                panel2.Visible = false;
                listBox1.Visible = false;
                label4.Visible = false;
                button2.Visible = true;
                button1.Text = "Вход";
                _mainServiceClient.UserLogout();
                toolStripStatusLabel1.Text = "";
                listBox1.DataSource = null;
            }
        }

        // Отмена при Регистрации/Входе
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        // Отправить
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                _mainServiceClient.SendMessageToClient(listBox1.SelectedItem.ToString(), richTextBox2.Text);
            }
            else
            {
                _mainServiceClient.SendMessage(richTextBox2.Text);
            }

            listBox1.SelectedIndex = -1;
            richTextBox2.Text = "";
        }

        // Закрытие формы
        private void ChatMainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button1.Text == "Выход")
            {
                _mainServiceClient.UserLogout();
            }
            _mainServiceClient.Close();
        }

        public void Notify(string login, bool way)
        {
            if (way)
            {
                MessageBox.Show("К чату присоединился " + login, "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Чат покинул " + login, "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}