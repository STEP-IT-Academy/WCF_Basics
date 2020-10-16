using System;
using System.ServiceModel;
using System.Windows.Forms;
using WindowsFormsTicTacToeClient.CallbackHandlers;
using WindowsFormsTicTacToeClient.ServiceReference;

namespace WindowsFormsTicTacToeClient
{
    public partial class MainForm : Form
    {
        private readonly TicTacToeMainContractClient _proxyClient;

        public MainForm()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler(this));
            _proxyClient = new TicTacToeMainContractClient(instanceContext);
            _proxyClient.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            Buttons[0, 0] = button1;
            Buttons[0, 1] = button4;
            Buttons[0, 2] = button5;
            Buttons[1, 0] = button2;
            Buttons[1, 1] = button3;
            Buttons[1, 2] = button6;
            Buttons[2, 0] = button7;
            Buttons[2, 1] = button8;
            Buttons[2, 2] = button9;
        }

        public readonly Button[,] Buttons = new Button[3, 3];
        public Label Label => label3;

        private new void Move(int x, int y)
        {
            string res = _proxyClient.TryMove(x, y);

            if (res != null)
            {
                label3.Text = res;
            }
            else
            {
                _proxyClient.SendMove(x, y);
                label3.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Move(0, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Move(0, 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Move(0, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Move(1, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Move(1, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Move(1, 2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Move(2, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Move(2, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Move(2, 2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            panel3.Visible = true;
            string inf;
            int n = _proxyClient.Join(textBox1.Text, out inf);
            label2.Text = inf;
            Text += "  " + textBox1.Text;
            _proxyClient.ShowAllSteps();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _proxyClient.Leave();
            _proxyClient.Close();
        }
    }
}