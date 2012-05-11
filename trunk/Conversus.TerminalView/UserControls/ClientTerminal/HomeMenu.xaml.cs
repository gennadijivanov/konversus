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

namespace TerminalView.UserControls.ClientTerminal
{
    /// <summary>
    /// Interaction logic for HomeMenu.xaml
    /// </summary>
    public partial class HomeMenu : UserControl
    {
        public HomeMenu()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var senderName = ((Border)sender).Name;

            switch (senderName)
            {
                case "ToAnotherDayLink":
                    if (MainContainer.FindName("BrowserInstance") != null)
                    {
                        UIElement oldbrowser = (UIElement)MainContainer.FindName("BrowserInstance");
                        MainContainer.Children.Remove(oldbrowser);
                    }

                    var browser = new WebBrowser();
                    browser.Name = "BrowserInstance";
                    browser.Navigate("http://yandex.ru");

                    MainContainer.Children.Add(browser);
                    break;   
            }
            
            MessageBox.Show(senderName);
        }
    }
}
