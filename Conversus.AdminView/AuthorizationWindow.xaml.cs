using System.Windows;

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
            var adminWin = new AdminWindow();
            adminWin.Show();

            this.Close();
        }
    }
}
