using System.Windows;
using System.Windows.Controls;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for RedirectWindow.xaml
    /// </summary>
    public partial class RedirectWindow : Window
    {
        public RedirectWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: Запросить списоки для перенаправления
            //либо передать в конструктор окна при открытии
        }
    }
}