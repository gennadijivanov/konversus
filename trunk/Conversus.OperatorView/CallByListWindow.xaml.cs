using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Conversus.Core.Infrastructure;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.OperatorView
{
    public class ClientItemInfo
    {
        public Guid Id { get; set; }
        public string Ticket { get; set; }
        public string QueueTitle { get; set; }
        public string Status { get; set; }
        public string ChangeTime { get; set; }
    }

    /// <summary>
    /// Interaction logic for CallByListWindow.xaml
    /// </summary>
    public partial class CallByListWindow : Window
    {
        private readonly IEnumerable<ClientItemInfo> _clientInfos;
        private readonly OperatorWindow _operatorWindow;
        private readonly OperatorInfo _user;

        public CallByListWindow(IEnumerable<ClientInfo> queueCollection, OperatorWindow operatorWindow, OperatorInfo user)
        {
            InitializeComponent();

            _operatorWindow = operatorWindow;
            _clientInfos = queueCollection.Select(c => new ClientItemInfo
                                                           {
                                                               Id = c.Id,
                                                               Ticket = c.Ticket,
                                                               Status = Constants.ClientStatusTitles[c.Status],
                                                               ChangeTime = c.ChangeTime.ToShortTimeString(),
                                                               QueueTitle = c.Queue.Title
                                                           });
            _user = user;
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
            var selectedClient = postponedGrid.SelectedItem;

            if (selectedClient == null)
                return;

            _operatorWindow.Client = ServiceHelper.Instance.ClientService.CallClient(((ClientItemInfo)selectedClient).Id, _user.Id);
            _operatorWindow.refreshTimer();
                
            Close();
        }
    }
}
