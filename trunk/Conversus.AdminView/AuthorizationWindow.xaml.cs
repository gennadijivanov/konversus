using System.Windows;
using System.Windows.Input;
using Conversus.Core.Infrastructure;

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

            loginTextBox.Focus();
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

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(null, null);
        }
    }
}
