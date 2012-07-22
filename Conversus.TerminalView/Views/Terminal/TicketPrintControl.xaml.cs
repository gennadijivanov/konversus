using System.Windows.Controls;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for TicketPrintControl.xaml
    /// </summary>
    public partial class TicketPrintControl : UserControl
    {
        public TicketPrintControl()
        {
            InitializeComponent();
        }

        public void setLabels(string serviceNameText, string ticketNumber, string registerDateTime)
        {
            serviceNameTextBox.Text = serviceNameText;
            ticketNumberLabel.Content = ticketNumber;
            registerDateTimeLabel.Content = registerDateTime;
        }
    }
}
