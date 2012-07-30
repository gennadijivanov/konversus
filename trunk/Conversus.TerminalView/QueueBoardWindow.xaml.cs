using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Conversus.Service.Contract;
using ListBox = System.Windows.Controls.ListBox;
using MessageBox = System.Windows.MessageBox;

namespace Conversus.TerminalView
{
    /// <summary>
    /// Interaction logic for QueueBoardWindow.xaml
    /// </summary>
    public partial class QueueBoardWindow : Window
    {
        private static QueueBoardWindow _instance;

        public static QueueBoardWindow Instance
        {
            get { return _instance ?? (_instance = new QueueBoardWindow()); }
        }

        private QueueBoardWindow()
        {
            InitializeComponent();
        }

        private readonly string VIDEO_DIRECTORY_PATH = Path.GetDirectoryName("Content/video/");
        private string[] _fileEntries;
        private int _currentIndex = 0;
        private DateTime _lastChangeDirectory;

        private readonly List<ClientInfo> _queue = new List<ClientInfo>();

        public void CallClient(ClientInfo client)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                _queue.Add(client);

                callSound.Play();

                var storyBoard = (Storyboard)TryFindResource("newVisitorAnimation");
                if (storyBoard != null) storyBoard.Begin();

                callPopupLabel.Content = client.Ticket + " => " + client.User.CurrentWindow;

                SetQueueData();

                clientWindowListView.UpdateLayout();

                if (clientWindowListView != null)
                {
                    ClientWindowListViewSourceUpdated(clientWindowListView);
                }
            }));
        }

        private void SetQueueData()
        {
            //todo: get from service
            clientWindowListView.ItemsSource =
                _queue.Select(c => new {Ticket = c.Ticket, OperatorWindow = c.User.CurrentWindow}).Take(7);
        }

        public void RemovePerformed(ClientInfo client)
        {
            _queue.Remove(client);

            SetQueueData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var secondScreen = GetSecondaryScreen();

            this.Left = secondScreen.WorkingArea.Left;

            if (secondScreen != Screen.PrimaryScreen)
            {
                Window_KeyDown(null, null);
            }
            else
            {
                MessageBox.Show("Второй монитор не подключен");
            }

            var r = Screen.AllScreens;
            checkeForModificationVideoDirectory();
        }

        private Screen GetSecondaryScreen()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen != Screen.PrimaryScreen)
                    return screen;
            }
            return Screen.PrimaryScreen;
        }

        private void checkeForModificationVideoDirectory()
        {
            var currentChangeDirectory = Directory.GetLastWriteTime(VIDEO_DIRECTORY_PATH);

            if (_lastChangeDirectory == null || _lastChangeDirectory != currentChangeDirectory)
            {
                _lastChangeDirectory = Directory.GetLastWriteTime(VIDEO_DIRECTORY_PATH);
                _fileEntries = Directory.GetFiles(VIDEO_DIRECTORY_PATH, "*.wmv");
                _currentIndex = 0;
            }

            if (_fileEntries.Length > 0)
            {
                backgroundVideo.Source = new Uri(_fileEntries[_currentIndex], UriKind.Relative);
                backgroundVideo.Play();

                if (_currentIndex < _fileEntries.Length - 1)
                    _currentIndex++;
                else
                    _currentIndex = 0;
            }
            else
            {
                MessageBox.Show("Директория video пуста или отсутствует");
            }
        }

        private void backgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            checkeForModificationVideoDirectory();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e != null && e.Key != Key.Escape) return;

            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.ToolWindow;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
            }
        }

        private void ClientWindowListViewSourceUpdated(ListBox listBox)
        {
            if (listBox.Items.Count > 0)
            {
                var item = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(0);
                var animation = new DoubleAnimation
                                    {
                                        From = 1,
                                        To = 0,
                                        Duration = new Duration(TimeSpan.FromSeconds(2)),
                                        BeginTime = TimeSpan.FromSeconds(6),
                                        AutoReverse = true,
                                        RepeatBehavior = new RepeatBehavior(3)
                                    };

                var myStoryboard = new Storyboard();
                myStoryboard.Children.Add(animation);

                Storyboard.SetTarget(animation, item);
                Storyboard.SetTargetProperty(animation, new PropertyPath(ListBoxItem.OpacityProperty));

                myStoryboard.Begin(item);
            }
        }
    }
}
