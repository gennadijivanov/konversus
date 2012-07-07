using System.Windows;
using System.Windows.Controls;
using Conversus.Service.Helpers;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for CallByNymberWindow.xaml
    /// </summary>
    public partial class CallByNymberWindow : Window
    {
        public CallByNymberWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.OriginalSource;

            if (button.Name == "okButton")
            {
                string ticketString = ticketTextBox.Text;

                if (!string.IsNullOrWhiteSpace(ticketString))
                {
                    var client = ServiceHelper.Instance.ClientService.CallClientByTicket(ticketString);
                    if (client != null)
                        Close();
                    else
                    {
                        MessageBox.Show("Такого номера не существует в базе");
                    }
                }
                else
                {
                    MessageBox.Show("Некоректный тикет");
                }
            }
            else
            {
                Close();
            }

        }

    }
}
