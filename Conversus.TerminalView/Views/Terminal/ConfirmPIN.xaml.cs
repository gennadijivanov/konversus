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
    /// Interaction logic for ConfirmPIN.xaml
    /// </summary>
    public partial class ConfirmPIN : Page
    {
        public ConfirmPIN()
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

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var targetSender = (Button)e.OriginalSource;

            switch (targetSender.Name)
            {
                case "cancelButton":
                    navService.Navigate(new Uri("Views/Terminal/InputPIN.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "approveButton":
                    navService.Navigate(new Uri("Views/Terminal/PrintPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
            }
        }
    }
}
