using System.Collections.Generic;
using WindowsFormsChatClient.ServiceReference;

namespace WindowsFormsChatClient.CallbackHandlers
{
    public class ChatCallbackHandler : IChatMainServiceCallback
    {
        private readonly ChatMainView _chatMainView;

        public ChatCallbackHandler(ChatMainView chatMainView) => _chatMainView = chatMainView;

        public void DisplayMessageInChat(UserMessage message)
        {
            _chatMainView.richTextBox1.Text += "\n от " + message.Login + "  " + message.DepartureTime + "\n" + message.MessageText;
        }

        public void GetAllConnectedUsers(List<string> connectedUserLogins)
        {
            _chatMainView.listBox1.DataSource = connectedUserLogins;
            _chatMainView.listBox1.SelectedIndex = -1;
        }

        public void UserNotification(string login, bool way)
        {
            _chatMainView.Notify(login, way);
        }
    }
}