using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Conversus.BusinessLogic;
using Conversus.Storage;
using Conversus.Service.Helpers;

namespace TerminalView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            StorageLogicInitializer.Initialize();
            BusinessLogicInitializer.Initialize();

            ServiceHelper.Instance.ClientService.CreateFromLotus("Vasyanya", 12345);
        }
    }
}
