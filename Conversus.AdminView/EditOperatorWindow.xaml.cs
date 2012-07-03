using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        private UserInfo _client;

        public EditOperatorWindow(UserInfo client)
        {
            InitializeComponent();

            _client = client;

            ICollection<QueueInfo> queues = ServiceHelper.Instance.QueueService.GetQueues();

            queueTypeComboBox.DisplayMemberPath = "Title";
            queueTypeComboBox.ItemsSource = queues;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = "";
            loginTextBox.Text = "";
            windowTextBox.Text = "";
            queueTypeComboBox.SelectedItem = _client.Queue.Title;
            windowTextBox.Text = _client.CurrentWindow;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceHelper.Instance.UserService.Save(_client.Id,
                    nameTextBox.Text, loginTextBox.Text, passTextBox.Text, windowTextBox.Text,
                    (QueueType)((QueueInfo)queueTypeComboBox.SelectedItem).Type);

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
