using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

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

        private ClientInfo _client;
        private UserInfo _user;

        public OperatorWindow(UserInfo user)
        {
            _user = user;

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
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate()
            {
                TimeSpan timeSpan = e.SignalTime - startTime;
                timerLabel.Content = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds).ToString("g");
            });
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;

            switch (button.Name)
            {
                case "absenceButton":
                    MessageBox.Show("Клиенту " + currentVisitorTextBox.Text + " отмечена неявка");
                    servedTimer.Stop();
                    if (!pauseButton.IsEnabled)
                        pauseButton.IsEnabled = true;
                    break;
                case "postponedButton":
                    servedTimer.Stop();
                    MessageBox.Show("Клиент " + currentVisitorTextBox.Text + " добавлен в список отложенных");
                    if (!pauseButton.IsEnabled)
                        pauseButton.IsEnabled = true;
                    break;
                case "servedButton":
                    servedTimer.Stop();
                    MessageBox.Show("Обслуживание клиента " + currentVisitorTextBox.Text + " завершено");
                    currentVisitorTextBox.Text = CLEAN_CURRENT_VISITOR_TEXT;
                    if (!pauseButton.IsEnabled)
                        pauseButton.IsEnabled = true;
                    break;
                case "repeatButton":
                    if (_client != null)
                        ServiceHelper.Instance.ClientService.CallClient(_user.Id);
                    break;
                case "redirectButton":
                    //TODO: Вывести окно с доступными очередями и сотрудниками
                    var redirect = new RedirectWindow(_client);
                    redirect.Show();
                    break;
                case "callVisitorButton":
                    refreshTimer();
                    break;
                case "callByNumberButton":
                    var callByNumberWindow = new CallByNymberWindow();
                    callByNumberWindow.Show();
                    break;
                case "callByListButton":
                    var queueCollection = ServiceHelper.Instance.ClientService.GetClientsQueue(_user.Queue.Type);
                    var callByListWindow = new CallByListWindow(queueCollection);
                    callByListWindow.Show();
                    break;
                case "pauseButton":
                    servedTimer.Stop();
                    MessageBox.Show("Система переведена в режим Перерыва, для возврата в рабочее состояние - закройте это окно.");
                    break;
            }
        }

        private void refreshTimer()
        {
            if (_user != null && _client != null)
            {
                initTimer();
                pauseButton.IsEnabled = false;

                timerLabel.Content = TIMER_ZERO_TEXT;
                startTime = DateTime.Now;
                servedTimer.Start();

                _client = ServiceHelper.Instance.ClientService.CallNextClient(_user.Queue.Type);

                queueTypeTextBox.Text = _client.Queue.Title;
                currentVisitorTextBox.Text = _client.Ticket;

                absenceButton.IsEnabled = true;
                postponedButton.IsEnabled = true;
                servedButton.IsEnabled = true;
                repeatButton.IsEnabled = true;
                redirectButton.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var currentWindow = (Window)sender;

            if (MessageBox.Show("Вы действительно хотите выйти из программы?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}