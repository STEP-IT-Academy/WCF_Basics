using System;
using System.ServiceModel;
using TicTacToeService.Services;

namespace TicTacToeConsoleHost
{
    static class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(TicTacToeMainService));
            host.Open();
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
            host.Close();
        }
    }
}