using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Conversus.Core.DomainModel;

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
            var borderSender = (Image)sender;
            Page navigatePage = null;

            switch (borderSender.Name)
            {
                case "TakingLink" :
                    navigatePage = new Input_Name(QueueType.Taking);
                    break;
                case "ApprovementLink" :
                    navigatePage = new Input_Name(QueueType.Approvement);
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
