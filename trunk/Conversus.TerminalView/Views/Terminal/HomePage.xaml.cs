using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    navigationURI = "http://www.google.ru";
                    break;
            }

            navService.Navigate(new Uri(navigationURI, UriKind.RelativeOrAbsolute));
        }


    }
}
