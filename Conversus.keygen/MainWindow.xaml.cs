using System;
using System.Linq;
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
            LicenseType licType = (LicenseType)licenseTypeComboBox.SelectedItem;
            outputKeyTextBox.Text = EncryptionManager.GetEncryptedKey(companyTextBox.Text, licType);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            licenseTypeComboBox.ItemsSource =
                Enum.GetValues(typeof (LicenseType)).Cast<LicenseType>().Select(v => (int) v);
        }
    }
}