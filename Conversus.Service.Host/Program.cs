using System;
using System.Data.SQLite;
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
        static readonly Type userServiceType = typeof(OperatorService);

        private static ServiceHost clientHost;
        private static ServiceHost queueHost;
        private static ServiceHost userHost;

        static void Main(string[] args)
        {
            string connectionString = GetConnectionString();

            StorageLogicInitializer.Initialize(connectionString);
            BusinessLogicInitializer.Initialize();

            using (clientHost = CreateServiceHost(clientServiceType, Constants.Endpoints.ClientService))
            using (queueHost = CreateServiceHost(queueServiceType, Constants.Endpoints.QueueService))
            using (userHost = CreateServiceHost(userServiceType, Constants.Endpoints.OperatorService))
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

        public static string GetConnectionString()
        {
            const string modelFileName = "ConversusDataModel";
            const string dbFileName = "conversus.db3";

            var fac = SQLiteFactory.Instance;
            var connectionStringBuilder = fac.CreateConnectionStringBuilder();
            connectionStringBuilder.Add("metadata", string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", modelFileName));
            connectionStringBuilder.Add("provider", "System.Data.SQLite");
            connectionStringBuilder.Add("provider connection string", string.Format("data source={0}", dbFileName));

            return connectionStringBuilder.ToString();
        }

        private static ServiceHost CreateServiceHost(Type serviceType, string endpoint)
        {
            var service = new ServiceHost(serviceType, new Uri(PropertyManager.Instance.ServiceHost + endpoint));
            service.AddServiceEndpoint(
                ServiceHelper.Instance.GetEndpoint(serviceType, endpoint, PropertyManager.Instance.ServiceHost));
            return service;
        }
    }
}
