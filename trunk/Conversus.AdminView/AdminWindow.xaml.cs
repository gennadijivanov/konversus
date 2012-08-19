using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Conversus.Impl;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;
using Conversus.Core.Infrastructure;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private LicenseType? _license;

        public AdminWindow()
        {
            InitializeComponent();

            string company = ServiceHelper.Instance.PropertyService.GetProperty(Constants.Properties.Company);
            string licenseKey = ServiceHelper.Instance.PropertyService.GetProperty(Constants.Properties.LicenseKey);

            if (!string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(licenseKey))
                _license = EncryptionManager.TestLicense(licenseKey, company);
        }

        private void AdminWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_license.HasValue)
                ActivateApplication();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            var insertOperatorWindow = new EditOperatorWindow(null);
            insertOperatorWindow.Closed += insertOperatorWindow_Closed;
            insertOperatorWindow.Show();
        }

        private void insertOperatorWindow_Closed(object sender, EventArgs e)
        {
            ReloadOperatorsList();
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
            if (operatorListGrid.SelectedItem != null)
                ServiceHelper.Instance.OperatorService.Delete(((OperatorInfo) operatorListGrid.SelectedItem).Id);
        }

        private void operatorListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid) e.OriginalSource;

            if (grid.SelectedItem != null)
                deleteButton.Visibility = editButton.Visibility = Visibility.Visible;
            else
                deleteButton.Visibility = editButton.Visibility = Visibility.Collapsed;
        }

        private void ReloadOperatorsList()
        {
            ICollection<OperatorInfo> users = ServiceHelper.Instance.OperatorService.GetAllUsers();
            operatorListGrid.ItemsSource = users;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (operatorListGrid.SelectedItem == null)
                return;

            var editWindow = new EditOperatorWindow((OperatorInfo) operatorListGrid.SelectedItem);
            editWindow.Closed += insertOperatorWindow_Closed;
            editWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LicenseType? licenseType = EncryptionManager.TestLicense(keyTextBox.Text, companyTextBox.Text);

            if (!licenseType.HasValue)
                MessageBox.Show("Ключ невалиден");
            else
            {
                _license = licenseType;

                ServiceHelper.Instance.PropertyService.SetProperty(Constants.Properties.Company, companyTextBox.Text);
                ServiceHelper.Instance.PropertyService.SetProperty(Constants.Properties.LicenseKey, keyTextBox.Text);

                keyTextBox.Text = string.Empty;

                string congratulationsText = "Продукт активирован. Доступно операторов: " + (int) licenseType.Value;
                MessageBox.Show(congratulationsText);

                ActivateApplication();
            }
        }

        private void tabActivation_GotFocus(object sender, RoutedEventArgs e)
        {
            SetActivationStatusLabel();

            if (_license.HasValue)
                companyTextBox.Text = ServiceHelper.Instance.PropertyService.GetProperty(Constants.Properties.Company);
        }

        private void ActivateApplication()
        {
            tabItem1.IsEnabled = true;
            tabItem1.IsSelected = true;
            TabItemReports.IsEnabled = true;
            ReloadOperatorsList();
            SetActivationStatusLabel();
        }

        private void SetActivationStatusLabel()
        {
            if (_license.HasValue)
                statusLabel.Text = "Продукт активирован. Доступно операторов: " + (int) _license.Value;
            else
                statusLabel.Text = "Продукт не активирован.";
        }
    }
}
