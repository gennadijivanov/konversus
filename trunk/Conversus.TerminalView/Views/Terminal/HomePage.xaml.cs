using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static RoutedEvent LinkClickedEvent;
        
        public HomePage()
        {
            InitializeComponent();
        }

        private NavigationService navService = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var borderSender = (Border)sender;
            Page navigatePage = null;

            switch (borderSender.Name)
            {
                case "TakingLink" :
                case "ApprovementLink" :
                    navigatePage = new Input_Name();
                    break;
                case "HasPinLink" :
                    navigatePage = new InputPIN();
                    break;
                case "ToAnotherDayLink":
                    navigatePage = new OnlineRegisterPage();
                    break;
            }

            navService.Navigate(navigatePage);
        }

    }
}
