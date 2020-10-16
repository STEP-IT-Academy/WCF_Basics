using PatientsService.Services;
using System;
using System.ServiceModel;

namespace PatientsServiceHost
{
    static class Program
    {
        static void Main()
        {
            ServiceHost host = new ServiceHost(typeof(PatientServices));
            host.Open();
            Console.WriteLine("Нажмите любую кнопку, чтобы закрыть хостинг");
            Console.ReadKey();
            host.Close();
        }
    }
}