using ChatService.Services;
using System;
using System.ServiceModel;

namespace ConsoleChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ChatMainService));
            host.Open();

            Console.WriteLine("Нажмите любую кнопку, чтобы закрыть хостинг");
            Console.ReadKey();
            host.Close();
        }
    }
}