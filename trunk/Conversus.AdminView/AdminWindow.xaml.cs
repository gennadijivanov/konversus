﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            var insertOperatorWindow = new InsertOperatorWindow();
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
                ServiceHelper.Instance.UserService.Delete(((UserInfo)operatorListGrid.SelectedItem).Id);
        }

        private void operatorListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)e.OriginalSource;

            if (!deleteButton.IsEnabled)
                deleteButton.IsEnabled = true;
        }

        private void AdminWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ReloadOperatorsList();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //var saveItems = (operatorListGrid.Items as List<UserInfo>);
        }

        private void ReloadOperatorsList()
        {
            ICollection<UserInfo> users = ServiceHelper.Instance.UserService.GetAllUsers();
            operatorListGrid.ItemsSource =
                users.Select( u => new {Name = u.Name, Login = u.Login, QueueType = u.Queue.Title, QWindow = u.CurrentWindow} ).ToList();
        }
    }
}
