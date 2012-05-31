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
        private const int SHOW_NEXT_BTN_CHAR_COUNT = 5;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navService.Navigate(new HomePage());
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var targetSender = (Button)e.OriginalSource;

            switch (targetSender.Name)
            {
                case "nextButton":
                    navService.Navigate(new PrintPage());
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
                    appendChar(targetSender.Content.ToString());
                    break;
            }
        }

        private void appendChar(string charText)
        {
            nameInputBox.Text += (IsShiftPressed) ? charText.ToUpper() : charText.ToLower();

            if (nameInputBox.Text.Length > SHOW_NEXT_BTN_CHAR_COUNT - 1)
                nextButton.Visibility = System.Windows.Visibility.Visible;
            else
                nextButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void deleteChar()
        {
            if (!string.IsNullOrEmpty(nameInputBox.Text))
                nameInputBox.Text = nameInputBox.Text.Remove(nameInputBox.Text.Length - 1);

            if (nameInputBox.Text.Length < SHOW_NEXT_BTN_CHAR_COUNT)
                nextButton.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
