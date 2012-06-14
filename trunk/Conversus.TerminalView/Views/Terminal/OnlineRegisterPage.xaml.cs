using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for OnlineRegisterPage.xaml
    /// </summary>
    public partial class OnlineRegisterPage : Page
    {
        public OnlineRegisterPage()
        {
            InitializeComponent();
        }

        private NavigationService navService = null;

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navService.Navigate(new HomePage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
        }
    }
}
