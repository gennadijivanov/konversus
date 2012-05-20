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
    /// Interaction logic for InputPIN.xaml
    /// </summary>
    public partial class InputPIN : Page
    {
        public InputPIN()
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
