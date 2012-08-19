﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;

namespace Conversus.TerminalView.Views.Terminal
{
    /// <summary>
    /// Interaction logic for InputPIN.xaml
    /// </summary>
    public partial class InputPIN : Page
    {
        public InputPIN()
        {
            InitializeComponent();
        }

        private NavigationService navService = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navService.Navigate(new HomePage());
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var targetSender = (Button)e.OriginalSource;

            switch (targetSender.Name)
            {
                case "nextButton":
                    targetSender.IsEnabled = false;
                    ClientInfo client = ServiceHelper.Instance.ClientService.GetClientByPin(int.Parse(pinInputBox.Text));

                    if (client == null)
                    {
                        MessageBox.Show("Пользователь с таким пином не зарегистрирован в системе");
                        targetSender.IsEnabled = true;
                    }
                    else
                    {
                        if (client.BookingTime >= DateTime.Now)
                            navService.Navigate(new ConfirmPIN(client));
                        else
                        {
                            MessageBox.Show(
                                "Вы опоздали. Запишитесь, пожалуйста, на другой день или возьмите талон по общей очереди",
                                "Опоздание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            navService.Navigate(new HomePage());
                        }
                    }

                    break;
                case "deleteButton":
                    deleteChar();
                    break;
                default:
                    insertChar(targetSender.Content.ToString());
                    break;
            }
        }

        private void insertChar(string inputNumber)
        {
            if (pinInputBox.Text.Length < 5)
                pinInputBox.Text += inputNumber;

            nextButton.Visibility = (pinInputBox.Text.Length == 5) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void deleteChar()
        {
            if (!string.IsNullOrEmpty(pinInputBox.Text))
                pinInputBox.Text = pinInputBox.Text.Remove(pinInputBox.Text.Length - 1);

            nextButton.Visibility = (pinInputBox.Text.Length == 5) ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
