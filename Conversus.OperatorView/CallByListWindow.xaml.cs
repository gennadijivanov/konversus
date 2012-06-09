using System.Windows;
using System.Windows.Controls;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for CallByListWindow.xaml
    /// </summary>
    public partial class CallByListWindow : Window
    {
        public CallByListWindow()
        {
            InitializeComponent();
        }

        private void postponedGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)e.OriginalSource;
            
            if (!callButton.IsEnabled) 
                callButton.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: Запросить список отложенных и прибиндить к гриду
            //либо передать в конструкот окна при открытии
        }
    }
}
