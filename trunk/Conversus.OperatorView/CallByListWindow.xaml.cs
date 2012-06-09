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
using System.Windows.Shapes;

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
