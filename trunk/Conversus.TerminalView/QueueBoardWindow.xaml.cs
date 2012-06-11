using System;
using System.Windows;
using System.IO;

namespace TerminalView
{
    /// <summary>
    /// Interaction logic for QueueBoardWindow.xaml
    /// </summary>
    public partial class QueueBoardWindow : Window
    {
        public QueueBoardWindow()
        {
            InitializeComponent();
        }

        private string VIDEO_DIRECTORY_PATH = Path.GetDirectoryName("Content/video/");
        private string[] fileEntries;
        private int currentIndex = 0;
        private DateTime lastChangeDirectory;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkeForModificationVideoDirectory();
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
                MessageBox.Show("Директория video пуста или отсутствует");
            }
        }

        private void backgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            checkeForModificationVideoDirectory();
        }
    }
}
