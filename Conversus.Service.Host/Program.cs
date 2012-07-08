using System;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Core.Infrastructure;
using Conversus.Service.Impl;
using Conversus.Storage;
using Conversus.Service.Helpers;

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

            using (clientHost = CreateServiceHost(clientServiceType, Constants.Endpoints.ClientService))
            using (queueHost = CreateServiceHost(queueServiceType, Constants.Endpoints.QueueService))
            using (userHost = CreateServiceHost(userServiceType, Constants.Endpoints.UserService))
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

        private static ServiceHost CreateServiceHost(Type serviceType, string endpoint)
        {
            var service = new ServiceHost(serviceType, new Uri(ServiceHelper.Instance.ServiceHost + endpoint));
            service.AddServiceEndpoint(
                ServiceHelper.Instance.GetEndpoint(serviceType, endpoint, ServiceHelper.Instance.ServiceHost));
            return service;
        }
    }
}
