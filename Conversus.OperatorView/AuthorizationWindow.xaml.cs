using System;
using System.Windows;
using Conversus.Service.Contract;
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
            try
            {
                var user = ServiceHelper.Instance.OperatorService.Login(loginTextBox.Text, passwordTextBox.Password);

                if (user == null)
                {
                    MessageBox.Show("Пароль или логин введены неверно", "Ошибка авторизации", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
                else
                {
                    var operatorWorkWin = new OperatorWindow(user);
                    operatorWorkWin.Show();

                    Close();
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
