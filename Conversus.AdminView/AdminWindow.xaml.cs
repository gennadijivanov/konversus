﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Conversus.Impl;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            var insertOperatorWindow = new EditOperatorWindow(null);
            insertOperatorWindow.Closed += insertOperatorWindow_Closed;
            insertOperatorWindow.Show();
        }

        void insertOperatorWindow_Closed(object sender, EventArgs e)
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
                ServiceHelper.Instance.OperatorService.Delete(((OperatorInfo)operatorListGrid.SelectedItem).Id);
        }

        private void operatorListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)e.OriginalSource;

            if ( grid.SelectedItem != null)
            {
                deleteButton.Visibility = editButton.Visibility = Visibility.Visible;
            }
            else
            {
                deleteButton.Visibility = editButton.Visibility = Visibility.Collapsed;
            }
        }

        private void AdminWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //TODO: Если нет активации, то не грузим лист операторов и активируем вкладку с активацией
            //то есть нада tabActivation.IsSelected = true;
            ReloadOperatorsList();
        }

        private void ReloadOperatorsList()
        {
            ICollection<OperatorInfo> users = ServiceHelper.Instance.OperatorService.GetAllUsers();
            operatorListGrid.ItemsSource = users;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (operatorListGrid.SelectedItem != null)
            {
                var editWindow = new EditOperatorWindow((OperatorInfo)operatorListGrid.SelectedItem);
                editWindow.Closed += insertOperatorWindow_Closed;
                editWindow.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var licenseType = EncryptionManager.TestLicense(keyTextBox.Text ,companyTextBox.Text);

            if (licenseType != null)
            {
                tabItem1.IsEnabled = true;
                TabItemReports.IsEnabled = true;
                statusLabel.Text = "Продукт активирован. Доступно операторов: "+((int)licenseType).ToString();
            }
            else
            {
                MessageBox.Show("Ключ невалиден");
            }

        }
    }
}
