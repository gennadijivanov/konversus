using System;
using System.Collections.Generic;
using System.Windows;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for EditOperatorWindow.xaml
    /// </summary>
    public partial class EditOperatorWindow : Window
    {
        private readonly OperatorInfo _client;

        public EditOperatorWindow(OperatorInfo client)
        {
            InitializeComponent();

            _client = client;

            ICollection<QueueInfo> queues = ServiceHelper.Instance.QueueService.GetQueues();

            queueTypeComboBox.DisplayMemberPath = "Title";
            queueTypeComboBox.ItemsSource = queues;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_client == null)
                return;
            
            nameTextBox.Text = _client.Name;
            loginTextBox.Text = _client.Login;
            windowTextBox.Text = _client.CurrentWindow;
            queueTypeComboBox.SelectedItem = _client.Queue.Title;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_client == null)
                {
                    ServiceHelper.Instance.OperatorService.Create(nameTextBox.Text, loginTextBox.Text, passTextBox.Text,
                        windowTextBox.Text,
                        ((QueueInfo) queueTypeComboBox.SelectedItem).Type);
                }
                else
                {
                    ServiceHelper.Instance.OperatorService.Save(_client.Id,
                        nameTextBox.Text, loginTextBox.Text, passTextBox.Text, windowTextBox.Text,
                        ((QueueInfo)queueTypeComboBox.SelectedItem).Type);
                }

                Close();
            }
            catch (InvalidOperationException invalidOperation)
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка при создании пользователя",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при создании пользователя", "Ошибка при создании пользователя",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
