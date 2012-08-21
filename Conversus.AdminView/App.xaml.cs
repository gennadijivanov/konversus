using System;
using System.Windows;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.AdminView
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
        }
    }
}
