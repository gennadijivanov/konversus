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
            //TODO: Запросить списки для перенаправления
            //либо передать в конструктор окна при открытии
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.OriginalSource;

            switch (button.Name)
            {
                case "redirectButton":
                    redirectVisitor(false);
                    break;
                case "redirectAndReturnButton" :
                    redirectVisitor(true);
                    break;
                case "cancelButton" :
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void redirectVisitor(bool IsNeedReturn)
        {
            if (employeeList.SelectedItem != null && queueList.SelectedItem != null)
            {
                //TODO: метод перенаправления передать туда селектед айтемы и отправить на нижний уровень
            }
            else
            {
                MessageBox.Show("Необходимо выбрать услугу и сотрудника для перенаправления посетителя");
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!redirectButton.IsEnabled || !redirectAndReturnButton.IsEnabled) 
            {
                redirectButton.IsEnabled = redirectAndReturnButton.IsEnabled = true;
            }
        }
    }
}