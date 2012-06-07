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
    /// Interaction logic for CallByNymberWindow.xaml
    /// </summary>
    public partial class CallByNymberWindow : Window
    {
        public CallByNymberWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.OriginalSource;

            if (button.Name == "okButton")
            {
                //TODO: проверка на существование такого номера в базе
            }
            
            this.Close();
        }

    }
}
