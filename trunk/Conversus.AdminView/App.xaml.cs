using System.Windows;
using Conversus.BusinessLogic;
using Conversus.Storage;

namespace Conversus.AdminView
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
        }
    }
}
