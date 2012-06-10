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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkeForModificationVideoDirectory();
        }

        private void checkeForModificationVideoDirectory()
        {
            //TODO: ПОЧЕМУ тут только 1 файл! Спросить у Гены
            fileEntries = Directory.GetFiles(VIDEO_DIRECTORY_PATH);

            if (fileEntries.Length > 0)
            {
                backgroundVideo.Source = new Uri(fileEntries[0], UriKind.Relative);
                backgroundVideo.Play();
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
