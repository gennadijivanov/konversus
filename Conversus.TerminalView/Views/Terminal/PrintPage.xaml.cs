using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Page
    {
        public PrintPage()
        {
            InitializeComponent();
        }

        private Timer backHomeTimer = new Timer();
        private NavigationService navService = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);

            backHomeTimer.Elapsed += new ElapsedEventHandler(goHomePage);
            backHomeTimer.Interval = 2000;
            backHomeTimer.Start();
        }

        private void goHomePage(object sender, ElapsedEventArgs e)
        {
            backHomeTimer.Elapsed -= new ElapsedEventHandler(goHomePage);

            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate(){ navService.Navigate(new HomePage()); });
        }
    }
}
