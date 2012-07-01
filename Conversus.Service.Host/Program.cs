using System;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Service.Impl;
using Conversus.Storage;

namespace Conversus.Service.Host
{
    class Program
    {
        static readonly Type clientServiceType = typeof(ClientService);
        static readonly Type queueServiceType = typeof(QueueService);
        static readonly Type userServiceType = typeof(UserService);

        private static ServiceHost clientHost;
        private static ServiceHost queueHost;
        private static ServiceHost userHost;

        static void Main(string[] args)
        {
            StorageLogicInitializer.Initialize();
            BusinessLogicInitializer.Initialize();

            using (clientHost = new ServiceHost(clientServiceType))
            using (queueHost = new ServiceHost(queueServiceType))
            using (userHost = new ServiceHost(userServiceType))
            {
                clientHost.Open();
                queueHost.Open();
                userHost.Open();

                Console.WriteLine();
                Console.WriteLine("Press <ENTER> to terminate Host");
                Console.ReadLine();

                clientHost.Close();
                queueHost.Close();
                userHost.Close();
            }
        }
    }
}
