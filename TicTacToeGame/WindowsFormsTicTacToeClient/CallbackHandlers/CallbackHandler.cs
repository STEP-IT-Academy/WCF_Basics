using WindowsFormsTicTacToeClient.ServiceReference;

namespace WindowsFormsTicTacToeClient.CallbackHandlers
{
    internal class CallbackHandler : ITicTacToeMainContractCallback
    {
        private readonly MainForm _form;

        public CallbackHandler(MainForm form) => _form = form;

        public void ShowMove(int x, int y, char sign, string info)
        {
            _form.Label.Text = info;
            _form.Buttons[x, y].Text = sign.ToString();
        }
    }
}