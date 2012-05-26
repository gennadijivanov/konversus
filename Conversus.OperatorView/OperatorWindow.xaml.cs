using System.Windows;
using System.Windows.Controls;

namespace Conversus.OperatorView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        public OperatorWindow()
        {
            InitializeComponent();
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            
            switch(button.Name)
            {
                case "absenceButton":
                    break;
                case "delayButton":
                    break;
                case "servedButton":
                    break;
                case "repeatButton":
                    break;
                case "redirectButton":
                    break;
                case "callVisitorButton":
                    break;
                case "callByNumberButton":
                    break;
                case "callByListButton":
                    break;
                case "pauseButton":
                    break;
            }
            MessageBox.Show(button.Name);
        }
    }
}
