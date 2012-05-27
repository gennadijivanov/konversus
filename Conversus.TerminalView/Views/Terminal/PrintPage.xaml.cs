using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
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

            printTicket();
        }

        private void printTicket()
        {
            PrintDialog printDlg = new PrintDialog();

            FlowDocument doc = new FlowDocument(new Paragraph(new Run("Queue print test")));
            doc.Name = "TicketPrint";

            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Print test");
        }

        private void goHomePage(object sender, ElapsedEventArgs e)
        {
            backHomeTimer.Elapsed -= new ElapsedEventHandler(goHomePage);

            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate(){ navService.Navigate(new HomePage()); });
        }
    }
}
