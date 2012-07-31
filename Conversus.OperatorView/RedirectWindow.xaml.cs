using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for RedirectWindow.xaml
    /// </summary>
    public partial class RedirectWindow : Window
    {
        private ClientInfo _client;

        public RedirectWindow(ClientInfo client)
        {
            InitializeComponent();

            _client = client;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ICollection<QueueInfo> queues = ServiceHelper.Instance.QueueService.GetQueues();
            queueList.DisplayMemberPath = "Title";
            queueList.ItemsSource = queues;
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.OriginalSource;

            switch (button.Name)
            {
                case "redirectButton":
                    redirectVisitor(false);
                    break;
                case "redirectAndReturnButton" :
                    redirectVisitor(true);
                    break;
                case "cancelButton" :
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void redirectVisitor(bool IsNeedReturn)
        {
            if (employeeList.SelectedItem != null && queueList.SelectedItem != null)
            {
                var selectedQueueType = (QueueType)((QueueInfo)queueList.SelectedItem).Type;
                //TODO: get operator Id
                ServiceHelper.Instance.ClientService.ChangeQueue(_client.Id, Guid.NewGuid(), SortPriority.Common);

                this.Close();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать услугу и сотрудника для перенаправления посетителя");
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)e.OriginalSource;

            if (!redirectButton.IsEnabled || !redirectAndReturnButton.IsEnabled && listBox != null) 
            {
                redirectButton.IsEnabled = redirectAndReturnButton.IsEnabled = true;

                var selectedQueueType = (QueueType)((QueueInfo)listBox.SelectedItem).Type;
                var usersByQueue = ServiceHelper.Instance.OperatorService.GetUsersByQueue(selectedQueueType);
            }
        }
    }
}