using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private List<UserInfo> allUsersList = null;

        public AdminWindow()
        {
            InitializeComponent();

            allUsersList = (List<UserInfo>) ServiceHelper.Instance.UserService.GetAllUsers();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Вниз надо передать:
            //nameTextBox.Text;
            //loginTextBox.Text;
            //passTextBox.Text;
            //roleComboBox.SelectedIndex;
        }

        private void buildReportButton_Click(object sender, RoutedEventArgs e)
        {
            //reportTypeComboBox.SelectedIndex;
            //fromDateTime.Value;
            //toDateTime.Value;
            //на колбэк заполнить гридДату - reportGrid
            //или через биндинг
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Тут хз. Надо посоветоваться с Геной.
        }

        private void dellButton_Click(object sender, RoutedEventArgs e)
        {
            if(operatorListGrid.SelectedItem != null)
                ServiceHelper.Instance.UserService.Delete(((UserInfo) operatorListGrid.SelectedItem).Id);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var insertOperatorWindow = new InsertOperatorWindow();
            insertOperatorWindow.Show();
        }

        private void operatorListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)e.OriginalSource;

            if (!deleteButton.IsEnabled)
                deleteButton.IsEnabled = true;
        }

        private void AdminWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            operatorListGrid.ItemsSource = allUsersList;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveItems = (operatorListGrid.Items as List<UserInfo>);
        }
    }
}
