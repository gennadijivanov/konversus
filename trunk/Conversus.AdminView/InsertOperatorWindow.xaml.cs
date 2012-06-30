using System.Linq;
using System.Windows;
using System;
using System.Windows.Controls;
using Conversus.Core.DomainModel;
using Conversus.Service.Helpers;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for InsertOperatorWindow.xaml
    /// </summary>
    public partial class InsertOperatorWindow : Window
    {
        public InsertOperatorWindow()
        {
            InitializeComponent();

            var queues = Enum.GetValues(typeof(QueueType)).Cast<QueueType>();

            foreach (QueueType queue in queues)
            {
                //TODO: pretty titles
                queueTypeComboBox.Items.Add(new ComboBoxItem() { Content = queue });
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceHelper.Instance.UserService.Create(
                    nameTextBox.Text, loginTextBox.Text, passTextBox.Text,
                    (QueueType)((ComboBoxItem)queueTypeComboBox.SelectedItem).Content);

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
