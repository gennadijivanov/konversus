using System.Windows;

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
            OperatorWindow operatorWorkWin = new OperatorWindow();
            operatorWorkWin.Show();
            this.Close();
        }
    }
}
