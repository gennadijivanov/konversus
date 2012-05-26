using System;
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
            var navigationURI = string.Empty;
            var borderSender = (Border)sender;

            switch (borderSender.Name)
            {
                case "TakingLink" :
                case "ApprovementLink" :
                    navigationURI = "Views/Terminal/InputName.xaml";
                    break;
                case "HasPinLink" :
                    navigationURI = "Views/Terminal/InputPIN.xaml";
                    break;
                case "ToAnotherDayLink":
                    navigationURI = "http://000-sp3.mog.ent.local/_layouts/LoginPersonalCabinet.aspx?ReturnUrl=%2fClientOrders%2fList.aspx";
                    break;
            }

            navService.Navigate(new Uri(navigationURI, UriKind.RelativeOrAbsolute));
        }

    }
}
