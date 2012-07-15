﻿using System.IO;
using System.Windows;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text == Constants.AdminLogin && passwordTextBox.Password == Constants.DefaultAdminPassword)
            {
                var adminWin = new AdminWindow();
                adminWin.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Пароль или логин администратора введены неверно", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
