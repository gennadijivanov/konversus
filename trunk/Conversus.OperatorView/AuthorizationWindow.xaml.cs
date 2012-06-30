﻿using System.Windows;
using Conversus.Service.Helpers;

namespace Conversus.OperatorView
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
            if (ServiceHelper.Instance.UserService.Authorize(loginTextBox.Text, passwordTextBox.Password))
            {
                OperatorWindow operatorWorkWin = new OperatorWindow();
                operatorWorkWin.Show();

                Close();
            }
            else
            {
                MessageBox.Show("Пароль или логин введены неверно", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
