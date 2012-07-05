using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;
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

        private string VIDEO_DIRECTORY_PATH = Path.GetDirectoryName("Content/video/");
        private string[] fileEntries;
        private int currentIndex = 0;
        private DateTime lastChangeDirectory;

        private readonly List<ClientInfo> _queue = new List<ClientInfo>();

        public void CallClient(ClientInfo client)
        {
            _queue.Add(client);

            callSound.Play();

            var storyBoard = (Storyboard) TryFindResource("newVisitorAnimation");
            if (storyBoard != null) storyBoard.Begin();

            callPopupLabel.Content = client.Ticket +" => "+ client.Window;

            clientWindowListView.ItemsSource =
                _queue.OrderByDescending(c => c.PerformStart).Select(c => new {Name = c.Name, OperatorWindow = c.Window}).Take(7);
        }

        public void RemovePerformed(ClientInfo client)
        {
            _queue.Remove(client);

            clientWindowListView.ItemsSource =
                _queue.OrderByDescending(c => c.PerformStart).Select(c => new { Name = c.Name, OperatorWindow = c.Window }).Take(7);
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
                System.Windows.MessageBox.Show("Второй монитор не подключен");
            }

            var r = System.Windows.Forms.Screen.AllScreens;
            checkeForModificationVideoDirectory();

            CallClient(new ClientInfo());
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

            if (lastChangeDirectory == null || lastChangeDirectory != currentChangeDirectory)
            {
                lastChangeDirectory = Directory.GetLastWriteTime(VIDEO_DIRECTORY_PATH);
                fileEntries = Directory.GetFiles(VIDEO_DIRECTORY_PATH, "*.wmv");
                currentIndex = 0;
            }

            if (fileEntries.Length > 0)
            {
                backgroundVideo.Source = new Uri(fileEntries[currentIndex], UriKind.Relative);
                backgroundVideo.Play();

                if (currentIndex < fileEntries.Length - 1)
                    currentIndex++;
                else
                    currentIndex = 0;
            }
            else
            {
                System.Windows.MessageBox.Show("Директория video пуста или отсутствует");
            }
        }

        private void backgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            checkeForModificationVideoDirectory();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {        
            if (e != null && e.Key != Key.Escape) return;

            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStyle = System.Windows.WindowStyle.ToolWindow;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                this.WindowStyle = System.Windows.WindowStyle.None;
            }
        }
    }
}
