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

        private bool IsShiftPressed = true;
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
                case "nextButton":
                    navService.Navigate(new Uri("Views/Terminal/PrintPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "r_shift":
                case "l_shift" :
                    IsShiftPressed = !IsShiftPressed;
                    break;
                case "deleteButton":
                    deleteChar();
                    break;
                case "spaceButton":
                    nameInputBox.Text += " ";
                    break;
                default:
                    nameInputBox.Text += targetSender.Content.ToString();
                    break;
            }
        }

        private void deleteChar()
        {
            if (!string.IsNullOrEmpty(nameInputBox.Text))
                nameInputBox.Text = nameInputBox.Text.Remove(nameInputBox.Text.Length - 1);
        }
    }
}
