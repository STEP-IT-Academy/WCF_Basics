using System.ServiceProcess;

namespace WindowsServiceChatHost
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ChatWindowsService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}