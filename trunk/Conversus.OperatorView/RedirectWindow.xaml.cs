using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private readonly OperatorWindow _operatorWindow;

        public RedirectWindow(OperatorWindow operatorWindow)
        {
            InitializeComponent();

            _operatorWindow = operatorWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ICollection<QueueInfo> queues = ServiceHelper.Instance.QueueService.GetQueues();
            queueList.DisplayMemberPath = "Title";
            queueList.ItemsSource = queues;

            employeeList.DisplayMemberPath = "Name";
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            var button = (ButtonBase)e.OriginalSource;

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
                var operatorId = ((OperatorInfo)employeeList.SelectedItem).Id;
                SortPriority sortPriority = SortPriority.Common;

                if (CommonPriorityBtn.IsChecked.Value)
                    sortPriority = SortPriority.Common;
                else if(BeginPriorityBtn.IsChecked.Value)
                    sortPriority = SortPriority.LowerCommon;
                else if (EndPriorityBtn.IsChecked.Value)
                    sortPriority = SortPriority.HigherVip;

                ServiceHelper.Instance.ClientService.ChangeQueue(_operatorWindow.Client.Id, operatorId, sortPriority);

                _operatorWindow.Client = null;
                _operatorWindow.refreshTimer();

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

            redirectButton.IsEnabled = redirectAndReturnButton.IsEnabled = false;

            var selectedQueueType = ((QueueInfo)listBox.SelectedItem).Type;
            employeeList.ItemsSource = ServiceHelper.Instance.OperatorService.GetUsersByQueue(selectedQueueType);
        }

        private void employeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            redirectButton.IsEnabled = redirectAndReturnButton.IsEnabled = true;
        }
    }
}