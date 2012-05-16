using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for Input_Name.xaml
    /// </summary>
    public partial class Input_Name : Page
    {
        public Input_Name()
        {
            InitializeComponent();
        }

        private NavigationService navService = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navService.Navigate(new Uri("Views/Terminal/HomePage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
