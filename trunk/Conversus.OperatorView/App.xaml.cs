using System;
using System.Windows;
using Conversus.Core.Infrastructure;

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
        }
    }
}
