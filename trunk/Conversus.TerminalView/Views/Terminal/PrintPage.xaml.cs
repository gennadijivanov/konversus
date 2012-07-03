using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Page
    {
        private Timer backHomeTimer = new Timer();
        private NavigationService navService = null;
        private readonly ClientInfo client;

        public PrintPage(ClientInfo client)
        {
            InitializeComponent();

            this.client = client;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);

            backHomeTimer.Elapsed += new ElapsedEventHandler(goHomePage);
            backHomeTimer.Interval = 2000;
            backHomeTimer.Start();

            printTicket();
        }

        private void printTicket()
        {
            var printDlg = new PrintDialog();
            ticketView.serviceNameTextBox.Text = client.Queue.Title;
            ticketView.ticketNumberLabel.Content = client.Ticket;
            ticketView.registerDateTimeLabel.Content = client.TakeTicket.Value.ToString();

            printDlg.PrintVisual(ticketView, "Ticket");
        }

        private void goHomePage(object sender, ElapsedEventArgs e)
        {
            backHomeTimer.Elapsed -= new ElapsedEventHandler(goHomePage);

            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate() { navService.Navigate(new HomePage()); });
        }
    }
}
