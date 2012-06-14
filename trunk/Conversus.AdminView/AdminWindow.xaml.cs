using System.Windows;
using System.Windows.Controls;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Вниз надо передать:
            //nameTextBox.Text;
            //loginTextBox.Text;
            //passTextBox.Text;
            //roleComboBox.SelectedIndex;
        }

        private void buildReportButton_Click(object sender, RoutedEventArgs e)
        {
            //reportTypeComboBox.SelectedIndex;
            //fromDateTime.Value;
            //toDateTime.Value;
            //на колбэк заполнить гридДату - reportGrid
            //или через биндинг
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Тут хз. Надо посоветоваться с Геной.
        }
    }
}
