using System;
using System.Collections.Generic;
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
            
            var licType = (LicenseType)Enum.Parse(typeof(LicenseType), licenseTypeComboBox.SelectedItem.ToString());

            var stringToEncrypt = licType + companyTextBox.Text;
            outputKeyTextBox.Text = EncryptionManager.EncryptString(stringToEncrypt, "key");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: как вывести значения енума - 3 оператора, к примеру? т.е Lite - 3 оператора.
            licenseTypeComboBox.ItemsSource = Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>();
        }

    }
}