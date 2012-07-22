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
                    serviceStoped("Клиенту " + currentVisitorTextBox.Text + " отмечена неявка");
                    break;
                case "postponedButton":
                    serviceStoped("Клиент " + currentVisitorTextBox.Text + " добавлен в список отложенных");
                    break;
                case "servedButton":
                    serviceStoped("Обслуживание клиента " + currentVisitorTextBox.Text + " завершено");
                    break;
                case "repeatButton":
                    if (_client != null)
                        ServiceHelper.Instance.ClientService.CallClient(_user.Id);
                    break;
                case "redirectButton":
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
                    var callByListWindow = new CallByListWindow(queueCollection, this);
                    callByListWindow.Show();
                    break;
                case "pauseButton":
                    servedTimer.Stop();
                    
                    ServiceHelper.Instance.UserService.PauseMaintenance(_user.Id);

                    MessageBoxResult pauseMaintenanceMessBoxResult = MessageBox.Show("Система переведена в режим Перерыва, для возврата в рабочее состояние - закройте это окно или нажмите ОК", "Перерыв в работе", MessageBoxButton.OK);
                    if (pauseMaintenanceMessBoxResult == MessageBoxResult.OK || pauseMaintenanceMessBoxResult == MessageBoxResult.None)
                    {
                        ServiceHelper.Instance.UserService.ReopenMaintenance(_user.Id);
                    }
                    break;
            }
        }

        private void serviceStoped(String message)
        {
            servedTimer.Stop();
            MessageBox.Show(message);
            currentVisitorTextBox.Text = CLEAN_CURRENT_VISITOR_TEXT;
            toggleButtonsEnable(false);
            if (!pauseButton.IsEnabled)
                pauseButton.IsEnabled = true;
        }

        public void refreshTimer()
        {
            _client = ServiceHelper.Instance.ClientService.CallNextClient(_user.Queue.Type);

            if (_client != null)
            {

                initTimer();
                pauseButton.IsEnabled = false;

                timerLabel.Content = TIMER_ZERO_TEXT;
                startTime = DateTime.Now;
                servedTimer.Start();

                queueTypeTextBox.Text = _client.Queue.Title;
                currentVisitorTextBox.Text = _client.Ticket;

                toggleButtonsEnable(true);
            }
            else
            {
                MessageBox.Show("Ожидающих по текущей очереди нет");
            }
        }

        private void toggleButtonsEnable(bool enable)
        {
            absenceButton.IsEnabled = enable;
            postponedButton.IsEnabled = enable;
            servedButton.IsEnabled = enable;
            repeatButton.IsEnabled = enable;
            redirectButton.IsEnabled = enable;
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