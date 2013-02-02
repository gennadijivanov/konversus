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

        public ClientInfo Client
        {
            set { Globals.CurrentClient = value; }
            get { return Globals.CurrentClient; }
        }

        public OperatorWindow()
        {
            InitializeComponent();
            initTimer();
            refreshLabels();

            Client = ServiceHelper.Instance.ClientService.Get(
                new ClientFilterParameters
                    {
                        Status = ClientStatus.Performing,
                        OperatorId = Globals.Operator.Id
                    }).FirstOrDefault();

            if (Client != null)
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
                    if (Client != null)
                        ServiceHelper.Instance.ClientService.CallClient(Client.Id, Globals.Operator.Id);
                    break;
                case "redirectButton":
                    var redirect = new RedirectWindow(this);
                    redirect.Show();
                    break;
                case "callVisitorButton":
                    callNext();
                    break;
                case "callByNumberButton":
                    var callByNumberWindow = new CallByNymberWindow();
                    callByNumberWindow.Show();
                    break;
                case "callByListButton":
                    var queueCollection = ServiceHelper.Instance.ClientService.GetClientsQueue(Globals.Operator.Queue.Type);
                    var callByListWindow = new CallByListWindow(queueCollection, this);
                    callByListWindow.Show();
                    break;
                case "pauseButton":
                    servedTimer.Stop();

                    ServiceHelper.Instance.OperatorService.PauseMaintenance(Globals.Operator.Id);

                    MessageBoxResult pauseMaintenanceMessBoxResult = MessageBox.Show("Система переведена в режим Перерыва, для возврата в рабочее состояние - закройте это окно или нажмите ОК", "Перерыв в работе", MessageBoxButton.OK);
                    if (pauseMaintenanceMessBoxResult == MessageBoxResult.OK || pauseMaintenanceMessBoxResult == MessageBoxResult.None)
                    {
                        ServiceHelper.Instance.OperatorService.ReopenMaintenance(Globals.Operator.Id);
                    }
                    break;
            }
        }

        private void serviceStoped(String message, ClientStatus clientStatus)
        {
            SetViewStopped();

            if (Client != null)
            {
                ServiceHelper.Instance.ClientService.ChangeStatus(Client.Id, clientStatus);
                MessageBox.Show(message);
            }

            Client = null;
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
            Client = ServiceHelper.Instance.ClientService.CallNextClient(Globals.Operator.Queue.Type, Globals.Operator.Id);

            if (Client != null)
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
            if (Client == null)
                return;
            
            initTimer();
            pauseButton.IsEnabled = false;

            timerLabel.Content = TIMER_ZERO_TEXT;
            startTime = DateTime.Now;
            servedTimer.Start();

            queueTypeTextBox.Text = Client.Queue.Title;
            currentVisitorTextBox.Text = Client.Ticket;

            toggleButtonsEnable(true);

            refreshLabels();
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
            if (Client != null)
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
                    ServiceHelper.Instance.OperatorService.Logout(Globals.Operator.Id);
                }
            }
        }

        private void refreshLabels()
        {
            var queueCollection = ServiceHelper.Instance.ClientService.GetClientsQueue(Globals.Operator.Queue.Type);

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