using System;
using System.ServiceModel;
using System.ServiceProcess;
using Conversus.BusinessLogic;
using Conversus.Core.Infrastructure;
using Conversus.Service.Helpers;
using Conversus.Service.Impl;
using Conversus.Storage;

namespace Conversus.Service.Host
{
    partial class HostService : ServiceBase
    {
        readonly Type _clientServiceType = typeof(ClientService);
        readonly Type _queueServiceType = typeof(QueueService);
        readonly Type _userServiceType = typeof(OperatorService);

        private ServiceHost _clientHost;
        private ServiceHost _queueHost;
        private ServiceHost _userHost;

        public HostService()
        {
            InitializeComponent();

            StorageLogicInitializer.Initialize(Program.GetConnectionString());
            BusinessLogicInitializer.Initialize();
        }

        protected override void OnStart(string[] args)
        {
            _clientHost = CreateServiceHost(_clientServiceType, Constants.Endpoints.ClientService);
            _queueHost = CreateServiceHost(_queueServiceType, Constants.Endpoints.QueueService);
            _userHost = CreateServiceHost(_userServiceType, Constants.Endpoints.OperatorService);
        }

        protected override void OnStop()
        {
            _clientHost.Close();
            _queueHost.Close();
            _userHost.Close();
        }

        private ServiceHost CreateServiceHost(Type serviceType, string endpoint)
        {
            var service = new ServiceHost(serviceType, new Uri(PropertyManager.Instance.ServiceHost + endpoint));
            service.AddServiceEndpoint(
                ServiceHelper.Instance.GetEndpoint(serviceType, endpoint, PropertyManager.Instance.ServiceHost));
            service.Open();
            return service;
        }
    }
}
