using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for ConfirmPIN.xaml
    /// </summary>
    public partial class ConfirmPIN : Page
    {
        private NavigationService navService = null;
        private readonly ClientInfo client = null;

        public ConfirmPIN(ClientInfo clientData)
        {
            InitializeComponent();
            
            this.client = clientData;
            //TODO: Получить тип очереди
        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (client == null)
                throw new Exception();

            navService = NavigationService.GetNavigationService(this);

            visitorNameLabel.Content = client.Name;

            if (client.PIN != null) pinInputBox.Text = client.PIN.Value.ToString();
            //TODO: Подставить реальное время и записать в базу
            var nowTime = DateTime.Now;
            arrivedDateTimeLabel.Content = nowTime.ToString();
            if (client != null) serviceLabel.Content = client.Queue.Type;
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
