using System;
using System.ServiceModel;
using System.Windows;
using Conversus.TerminalView.Service;

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
            Type serviceType = typeof (TerminalService);
            host = new ServiceHost(serviceType);
            host.Open();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.Close();
            base.OnExit(e);
        }
    }
}
