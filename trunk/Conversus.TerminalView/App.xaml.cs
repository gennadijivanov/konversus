using System;
using System.ServiceModel;
using System.Windows;
using Conversus.Core.Infrastructure;
using Conversus.Service.Helpers;
using Conversus.TerminalService.Contract;

namespace Conversus.TerminalView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceHost host;

        public App()
        {
            host = CreateServiceHost();
            host.Open();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.Close();
            base.OnExit(e);
        }

        private static ServiceHost CreateServiceHost()
        {
            var service = new ServiceHost(typeof(Service.TerminalService), new Uri(PropertyManager.Instance.TerminalServiceHost + Constants.Endpoints.TerminalService));
            service.AddServiceEndpoint(
                ServiceHelper.Instance.GetEndpoint(typeof(ITerminalService), Constants.Endpoints.TerminalService, PropertyManager.Instance.TerminalServiceHost));
            return service;
        }
    }
}
