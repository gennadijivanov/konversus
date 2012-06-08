using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System;
using System.Windows.Threading;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private const string CLEAN_CURRENT_VISITOR_TEXT = "---";
        private const string TIMER_ZERO_TEXT = "00:00:00";
        private Timer servedTimer;
        private DateTime startTime;

        public OperatorWindow()
        {
            InitializeComponent();
            initTimer();
        }

        private void initTimer()
        {
            servedTimer = new Timer();
            servedTimer.Elapsed += new ElapsedEventHandler(servedTimer_Elapsed);
            servedTimer.Interval = 1000;
        }

        void servedTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate() {
                TimeSpan timeSpan = e.SignalTime - startTime;
                //TODO: Почему не работает timeSpan.ToString("HH:mm:ss") узнать у Гены
                timerLabel.Content = timeSpan.ToString(); 
            });
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;

            switch(button.Name)
            {
                case "absenceButton":
                    MessageBox.Show("Клиент " + currentVisitorTextBox.Text + " отмечена неявка");
                    servedTimer.Stop();
                    break;
                case "delayButton":
                    servedTimer.Stop();
                    MessageBox.Show("Клиент " + currentVisitorTextBox.Text + " добавлен в список отложенных");
                    break;
                case "servedButton":
                    servedTimer.Stop();
                    MessageBox.Show("Обслуживание клиента " + currentVisitorTextBox.Text + " завершено");
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
                    refreshTimer();
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
                    servedTimer.Stop();
                    MessageBox.Show("Система переведена в режим Перерыва");
                    break;
            }
            
        }

        private void refreshTimer()
        {
            initTimer();
            timerLabel.Content = TIMER_ZERO_TEXT;
            startTime = DateTime.Now;
            servedTimer.Start();
        }
    }
}
