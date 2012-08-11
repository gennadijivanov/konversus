using System;
using System.Windows;
using Conversus.Impl;

namespace Conversus.keygen
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stringToEncrypt = queueTypeComboBox.SelectedItem.ToString() + companyTextBox.Text;
            outputKeyTextBox.Text = EncryptionManager.EncryptString(stringToEncrypt, "key");
        }

    }
}