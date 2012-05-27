using System.Windows;
using System.Windows.Input;

namespace TerminalView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Escape) return;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var queueBoard = new QueueBoardWindow();
            queueBoard.Show();
        }
    }
}
