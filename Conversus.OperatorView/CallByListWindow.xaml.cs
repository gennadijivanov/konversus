using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for CallByListWindow.xaml
    /// </summary>
    public partial class CallByListWindow : Window
    {
        private ICollection<ClientInfo> _clientInfos;
        private OperatorWindow _operatorWindow;

        public CallByListWindow(ICollection<ClientInfo> queueCollection, OperatorWindow operatorWindow)
        {
            InitializeComponent();

            _operatorWindow = operatorWindow;
            _clientInfos = queueCollection;
        }

        private void postponedGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)e.OriginalSource;

            if (!callButton.IsEnabled)
                callButton.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            postponedGrid.ItemsSource = _clientInfos;
        }

        private void callButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = (ClientInfo)postponedGrid.SelectedItem;

            if (selectedClient != null)
            {
                ServiceHelper.Instance.ClientService.CallClient(selectedClient.Id);
                _operatorWindow.refreshTimer();
                
                Close();
            }
        }
    }
}
