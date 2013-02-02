using System;
using System.Windows;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure;
using Conversus.Service.Helpers;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Log((Exception)e.ExceptionObject);

            if (Globals.Operator != null)
                ServiceHelper.Instance.OperatorService.Logout(Globals.Operator.Id);
            if (Globals.CurrentClient != null)
                ServiceHelper.Instance.ClientService.ChangeStatus(Globals.CurrentClient.Id, ClientStatus.Postponed);
        }
    }
}
