using ChatService.Services;
using System.ServiceModel;
using System.ServiceProcess;

namespace WindowsServiceChatHost
{
    public partial class ChatWindowsService : ServiceBase
    {
        internal static ServiceHost ServiceHost = null;

        public ChatWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceHost?.Close();
            ServiceHost = new ServiceHost(typeof(ChatMainService));
            ServiceHost.Open();
        }

        protected override void OnStop()
        {
            if (ServiceHost == null) return;
            ServiceHost.Close();
            ServiceHost = null;
        }
    }
}