using System;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
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
        private readonly OperatorInfo _oper;

        public ClientInfo Client
        {
            set { _client = value; }
            get { return _client; }
        }

        public OperatorWindow(OperatorInfo oper)
        {
            _oper = oper;

            InitializeComponent();
            initTimer();
            refreshLabels();

            _client = ServiceHelper.Instance.ClientService.Get(new ClientFilterParameters
                                                                   {
                                                                       Status = ClientStatus.Performing,
                                                                       OperatorId = oper.Id
                                                                   }).FirstOrDefault();

            if (_client != null)
                refreshTimer();
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
                    serviceStoped("Клиенту " + currentVisitorTextBox.Text + " отмечена неявка", ClientStatus.Absent);
                    break;
                case "postponedButton":
                    serviceStoped("Клиент " + currentVisitorTextBox.Text + " добавлен в список отложенных", ClientStatus.Postponed);
                    break;
                case "servedButton":
                    serviceStoped("Обслуживание клиента " + currentVisitorTextBox.Text + " завершено", ClientStatus.Done);
                    break;
                case "repeatButton":
                    if (_client != null)
                        ServiceHelper.Instance.ClientService.CallClient(_client.Id, _oper.Id);
                    break;
                case "redirectButton":
                    var redirect = new RedirectWindow(this);
                    redirect.Show();
                    break;
                case "callVisitorButton":
                    callNext();
                    break;
                case "callByNumberButton":
                    var callByNumberWindow = new CallByNymberWindow(_oper);
                    callByNumberWindow.Show();
                    break;
                case "callByListButton":
                    var queueCollection = ServiceHelper.Instance.ClientService.GetClientsQueue(_oper.Queue.Type);
                    var callByListWindow = new CallByListWindow(queueCollection, this, _oper);
                    callByListWindow.Show();
                    break;
                case "pauseButton":
                    servedTimer.Stop();

                    ServiceHelper.Instance.OperatorService.PauseMaintenance(_oper.Id);

                    MessageBoxResult pauseMaintenanceMessBoxResult = MessageBox.Show("Система переведена в режим Перерыва, для возврата в рабочее состояние - закройте это окно или нажмите ОК", "Перерыв в работе", MessageBoxButton.OK);
                    if (pauseMaintenanceMessBoxResult == MessageBoxResult.OK || pauseMaintenanceMessBoxResult == MessageBoxResult.None)
                    {
                        ServiceHelper.Instance.OperatorService.ReopenMaintenance(_oper.Id);
                    }
                    break;
            }
        }

        private void serviceStoped(String message, ClientStatus clientStatus)
        {
            SetViewStopped();

            if (_client != null)
            {
                ServiceHelper.Instance.ClientService.ChangeStatus(_client.Id, clientStatus);
                MessageBox.Show(message);
            }

            _client = null;
        }

        internal void SetViewStopped()
        {
            servedTimer.Stop();
            currentVisitorTextBox.Text = CLEAN_CURRENT_VISITOR_TEXT;
            toggleButtonsEnable(false);

            if (!pauseButton.IsEnabled)
                pauseButton.IsEnabled = true;
        }

        public void callNext()
        {
            _client = ServiceHelper.Instance.ClientService.CallNextClient(_oper.Queue.Type, _oper.Id);

            if (_client != null)
            {
                refreshTimer();
            }
            else
            {
                MessageBox.Show("Ожидающих по текущей очереди нет");
            }
        }

        public void refreshTimer()
        {
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

                refreshLabels();
            }
        }

        private void toggleButtonsEnable(bool enable)
        {
            absenceButton.IsEnabled = enable;
            postponedButton.IsEnabled = enable;
            servedButton.IsEnabled = enable;
            repeatButton.IsEnabled = enable;
            redirectButton.IsEnabled = enable;

            enable = !enable;

            callByListButton.IsEnabled = enable;
            callByNumberButton.IsEnabled = enable;
            callVisitorButton.IsEnabled = enable;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_client != null)
            {
                e.Cancel = true;
                MessageBox.Show("Нельзя закрыть программу, без завершения обслуживания клиента.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (MessageBox.Show("Вы действительно хотите выйти из программы?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    ServiceHelper.Instance.OperatorService.Logout(_oper.Id);
                }
            }
        }

        private void refreshLabels()
        {
            var queueCollection = ServiceHelper.Instance.ClientService.GetClientsQueue(_oper.Queue.Type);

            if (queueCollection.Count > 0)
            {
                var allQueues = ServiceHelper.Instance.QueueService.GetQueues().Count;

                var postponedCount = queueCollection.Count(q => q.Status == ClientStatus.Postponed);

                var oneHourAgo = DateTime.Now.AddHours(-1);
                var waitingLongCount = queueCollection.Count(q => DateTime.Compare(q.ChangeTime, oneHourAgo) > 0);

                waitingLabel.Content = String.Format("ОЖ:{0}/{1}", queueCollection.Count, allQueues);
                postponedLabel.Content = String.Format("ОТ:{0}/{1}", postponedCount, allQueues);
                longWaitLabel.Content = String.Format("ДЛ:{0}/{1}", waitingLongCount, allQueues);
            }
        }
    }
}