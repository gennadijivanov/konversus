using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Conversus.Core.DTO;
using Conversus.Service.Helpers;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for ConfirmPIN.xaml
    /// </summary>
    public partial class ConfirmPIN : Page
    {
        public ConfirmPIN(ClientData clientData)
        {
            InitializeComponent();
            pinInputBox.Text = clientData.PIN.Value.ToString();
        }

        private NavigationService navService = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientData client = ServiceHelper.Instance.ClientService.GetClientByPin(123);

            if(client.PIN == 0)
                throw new Exception();

            navService = NavigationService.GetNavigationService(this);
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navService.Navigate(new HomePage());
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var targetSender = (Button)e.OriginalSource;

            switch (targetSender.Name)
            {
                case "cancelButton":
                    navService.Navigate(new InputPIN());
                    break;
                case "approveButton":
                    navService.Navigate(new PrintPage());
                    break;
            }
        }
    }
}
