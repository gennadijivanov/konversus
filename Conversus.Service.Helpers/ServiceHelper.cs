using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Conversus.Core.Infrastructure;
using Conversus.Service.Contract;
using Conversus.TerminalService.Contract;

namespace Conversus.Service.Helpers
{
    public class ServiceHelper
    {
        private static ServiceHelper _instance;

        public static ServiceHelper Instance
        {
            get { return _instance ?? (_instance = new ServiceHelper()); }
        }

        public string ServiceHost = Constants.DefaultServiceHost;
        public string TerminalServiceHost = Constants.DefaultTerminalServiceHost;

        private readonly Binding _binding = new BasicHttpBinding();

        private IClientService _сlientService;
        public IClientService ClientService
        {
            get { return _сlientService ?? (_сlientService = GetClient<IClientService>(Constants.Endpoints.ClientService)); }
        }

        private IQueueService _queueService;
        public IQueueService QueueService
        {
            get { return _queueService ?? (_queueService = GetClient<IQueueService>(Constants.Endpoints.QueueService)); }
        }

        private IUserService _userService;
        public IUserService UserService
        {
            get { return _userService ?? (_userService = GetClient<IUserService>(Constants.Endpoints.UserService)); }
        }

        private ITerminalService _terminalService;
        public ITerminalService TerminalService
        {
            get { return _terminalService ?? (_terminalService = GetTerminalClient()); }
        }

        private ServiceHelper()
        {
        }

        public void ChangeServiceHost(string serviceHost)
        {
            ServiceHost = serviceHost;
            _queueService = null;
            _userService = null;
            _сlientService = null;
        }

        public void ChangeTerminalServiceHost(string serviceHost)
        {
            TerminalServiceHost = serviceHost;
            _terminalService = null;
        }

        public ServiceEndpoint GetEndpoint(Type contract, string endpoint, string serviceHost)
        {
            return new ServiceEndpoint(
                ContractDescription.GetContract(contract),
                _binding,
                new EndpointAddress(ServiceHost + endpoint));
        }

        private T GetClient<T>(string endpointName)
        {
            var endpoint = GetEndpoint(typeof(T), endpointName, ServiceHost);

            var factory = new ChannelFactory<T>(endpoint);
            return factory.CreateChannel();
        }

        private ITerminalService GetTerminalClient()
        {
            var endpoint = GetEndpoint(typeof(ITerminalService), Constants.Endpoints.TerminalService, TerminalServiceHost);

            var factory = new ChannelFactory<ITerminalService>(endpoint);
            return factory.CreateChannel();
        }
    }
}
