using System.Windows;
using System.Windows.Controls;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private const string CLEAN_CURRENT_VISITOR_TEXT = "---";

        public OperatorWindow()
        {
            InitializeComponent();
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;

            //TODO: Оживить timerLabel
            switch(button.Name)
            {
                case "absenceButton":
                    MessageBox.Show("Клиент " + currentVisitorTextBox.Text + "отмечена неявка");
                    break;
                case "delayButton":
                    MessageBox.Show("Клиент " + currentVisitorTextBox.Text + "добавлен в список отложенных");
                    break;
                case "servedButton":
                    MessageBox.Show("Обслуживание клиента " + currentVisitorTextBox.Text + "завершено");
                    currentVisitorTextBox.Text = CLEAN_CURRENT_VISITOR_TEXT;
                    break;
                case "repeatButton":
                    //TODO: Вызвать текущего клиента ещё раз
                    break;
                case "redirectButton":
                    //TODO: Вывести окно с доступными очередями
                    break;
                case "callVisitorButton":
                    //TODO: Запросить нового в очереди
                    break;
                case "callByNumberButton":
                    var callByNumberWindow = new CallByNymberWindow();
                    callByNumberWindow.Show();
                    break;
                case "callByListButton":
                    //TODO: Вывести окно со списком ожидающих
                    break;
                case "pauseButton":
                    //TODO: приостановить работу
                    //не забыть спросить что сделать с клиентом,
                    //если он ещё обслуживается
                    MessageBox.Show("Система переведена в режим Перерыва");
                    break;
            }
            
        }
    }
}
