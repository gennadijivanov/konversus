using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Conversus.Core.DomainModel;
using Conversus.Impl;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;
using Conversus.Core.Infrastructure;
using Microsoft.Win32;

namespace Conversus.AdminView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private struct ReportDescription
        {
            public string CreateTableSchema { get; set; }

            public string InsertSchema { get; set; }

            public string InsertDataTpl { get; set; }
        }

        private LicenseType? _license;

        private readonly Dictionary<ReportType, ReportDescription> _reportDescriptions = new Dictionary<ReportType,ReportDescription>
            {
                { ReportType.ByClients, new ReportDescription
                    {
                        CreateTableSchema = "[Date] VARCHAR(30), [Time] VARCHAR(30), [Name] VARCHAR(100), [PIN] VARCHAR(10), [Queue] VARCHAR(100), [Status] VARCHAR(30), [Operator] VARCHAR(100), [BookingDate] VARCHAR(30), [BookingTime] VARCHAR(30)",
                        InsertSchema = "[Date], [Time], [Name], [PIN], [Queue], [Status], [Operator], [BookingDate], [BookingTime]",
                        InsertDataTpl = "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'"
                    } },
                { ReportType.ByOperators, new ReportDescription
                    {
                        CreateTableSchema = "[Name] VARCHAR(100), [Queue] VARCHAR(100), [Window] VARCHAR(100), [Date] VARCHAR(30), [Time] VARCHAR(30), [Status] VARCHAR(30)",
                        InsertSchema = "[Name], [Queue], [Window], [Date], [Time], [Status]",
                        InsertDataTpl = "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}'"
                    } },
                //{ ReportType.ByQueue, new ReportDescription
                //    {
                //        CreateTableSchema = "[Date] datetime, [PerformedCount] int, [NotPerformedCount] int",
                //        InsertSchema = "[Date], [PerformedCount], [NotPerformedCount]"
                //    } }
            }; 

        public AdminWindow()
        {
            InitializeComponent();

            string company = ServiceHelper.Instance.PropertyService.GetProperty(Constants.Properties.Company);
            string licenseKey = ServiceHelper.Instance.PropertyService.GetProperty(Constants.Properties.LicenseKey);

            if (!string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(licenseKey))
                _license = EncryptionManager.TestLicense(licenseKey, company);
        }

        private void AdminWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_license.HasValue)
                ActivateApplication();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            var insertOperatorWindow = new EditOperatorWindow(null);
            insertOperatorWindow.Closed += insertOperatorWindow_Closed;
            insertOperatorWindow.Show();
        }

        private void insertOperatorWindow_Closed(object sender, EventArgs e)
        {
            ReloadOperatorsList();
        }

        private void buildReportButton_Click(object sender, RoutedEventArgs e)
        {
            if (!fromDateTime.Value.HasValue || !toDateTime.Value.HasValue)
            {
                MessageBox.Show("Проверьте правильность введенных дат", "", MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
                return;
            }

            switch ((ReportType)reportTypeComboBox.SelectedValue)
            {
                case ReportType.ByOperators:
                    reportGrid.ItemsSource = ServiceHelper.Instance.ReportService.GetReportByOperators(
                        fromDateTime.Value.Value,
                        toDateTime.Value.Value);
                    break;

                case ReportType.ByClients:
                    reportGrid.ItemsSource = ServiceHelper.Instance.ReportService.GetReportByClients(
                        fromDateTime.Value.Value,
                        toDateTime.Value.Value);
                    break;

                //case ReportType.ByQueue:
                //    reportGrid.ItemsSource = ServiceHelper.Instance.ReportService.GetReportByQueue(
                //        fromDateTime.Value.Value,
                //        toDateTime.Value.Value);
                //    break;
            }

            if (!((IEnumerable<ReportModelBase>)reportGrid.ItemsSource).Any())
            {
                MessageBox.Show("Нет данных за указанный период", "", MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            if (reportGrid.ItemsSource == null || !((IEnumerable<ReportModelBase>)reportGrid.ItemsSource).Any())
            {
                MessageBox.Show("Пожалуйста сформируйте отчет для экспорта", "", MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
                return;
            }

            var dlg = new SaveFileDialog
                          {
                              DefaultExt = ".xls",
                              Filter = "Excel documents (.xls)|*.xls"
                          };

            if (dlg.ShowDialog() != true)
                return;

            if (System.IO.File.Exists(dlg.FileName))
                System.IO.File.Delete(dlg.FileName);

            var reportType = (ReportType) reportTypeComboBox.SelectedValue;
            var repDesc = _reportDescriptions[reportType];

            using (
                var conn =
                    new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dlg.FileName +
                                        ";Extended Properties='Excel 8.0;HDR=Yes'"))
            {
                conn.Open();
                using (var cmd = new OleDbCommand("CREATE TABLE [NewSheet] (" + repDesc.CreateTableSchema + ")", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                const string insertCmdTpl = "INSERT INTO [NewSheet] ({0}) VALUES ({1});";

                switch (reportType)
                {
                    case ReportType.ByOperators:
                        {
                            var data = (IEnumerable<ReportByOperatorsModel>) reportGrid.ItemsSource;

                            foreach (ReportByOperatorsModel item in data)
                            {
                                string dataStr = string.Format(repDesc.InsertDataTpl, item.Name, item.Queue, item.Window,
                                    item.Date, item.Time, item.Status);
                                var cmd = string.Format(insertCmdTpl, repDesc.InsertSchema, dataStr);

                                using (var insertCmd = new OleDbCommand(cmd, conn))
                                {
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                        break;

                    case ReportType.ByClients:
                        {
                            var data = (IEnumerable<ReportByClientsModel>) reportGrid.ItemsSource;

                            foreach (ReportByClientsModel item in data)
                            {
                                string dataStr = string.Format(repDesc.InsertDataTpl, item.Date, item.Time, item.Name,
                                    item.PIN, item.Queue, item.Status, item.Operator, item.BookingDate, item.BookingTime);
                                var cmd = string.Format(insertCmdTpl, repDesc.InsertSchema, dataStr);

                                using (var insertCmd = new OleDbCommand(cmd, conn))
                                {
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                        break;

                    //case ReportType.ByQueue:
                    //    var data = (IEnumerable<ReportByQueueModel>)reportGrid.ItemsSource;

                    //    foreach (ReportByQueueModel item in data)
                    //    {
                    //        string dataStr = "'" + item.Date.ToShortDateString() + "',"
                    //                         + item.PerformedCount + ","
                    //                         + item.NotPerformedCount;
                    //        var cmd = string.Format(insertCmdTpl, repDesc.InsertSchema, dataStr);

                    //        using (var insertCmd = new OleDbCommand(cmd, conn))
                    //        {
                    //            insertCmd.ExecuteNonQuery();
                    //        }
                    //    }
                    //    break;
                }
            }
        }

        private void dellButton_Click(object sender, RoutedEventArgs e)
        {
            if (operatorListGrid.SelectedItem == null)
                return;

            ServiceHelper.Instance.OperatorService.Delete(((OperatorInfo) operatorListGrid.SelectedItem).Id);
            ReloadOperatorsList();
        }

        private void operatorListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid) e.OriginalSource;

            if (grid.SelectedItem != null)
                deleteButton.Visibility = editButton.Visibility = Visibility.Visible;
            else
                deleteButton.Visibility = editButton.Visibility = Visibility.Collapsed;
        }

        private void ReloadOperatorsList()
        {
            ICollection<OperatorInfo> users = ServiceHelper.Instance.OperatorService.GetAllUsers();
            operatorListGrid.ItemsSource = users;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (operatorListGrid.SelectedItem == null)
                return;

            var editWindow = new EditOperatorWindow((OperatorInfo) operatorListGrid.SelectedItem);
            editWindow.Closed += insertOperatorWindow_Closed;
            editWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LicenseType? licenseType = EncryptionManager.TestLicense(keyTextBox.Text, companyTextBox.Text);

            if (!licenseType.HasValue)
                MessageBox.Show("Ключ невалиден");
            else
            {
                _license = licenseType;

                ServiceHelper.Instance.PropertyService.SetProperty(Constants.Properties.Company, companyTextBox.Text);
                ServiceHelper.Instance.PropertyService.SetProperty(Constants.Properties.LicenseKey, keyTextBox.Text);

                keyTextBox.Text = string.Empty;

                string congratulationsText = "Продукт активирован. Доступно операторов: " + (int) licenseType.Value;
                MessageBox.Show(congratulationsText);

                ActivateApplication();
            }
        }

        private void tabActivation_GotFocus(object sender, RoutedEventArgs e)
        {
            SetActivationStatusLabel();

            if (_license.HasValue)
                companyTextBox.Text = ServiceHelper.Instance.PropertyService.GetProperty(Constants.Properties.Company);
        }

        private void ActivateApplication()
        {
            tabItem1.IsEnabled = true;
            tabItem1.IsSelected = true;
            TabItemReports.IsEnabled = true;
            ReloadOperatorsList();
            SetActivationStatusLabel();

            reportTypeComboBox.ItemsSource = new ArrayList
                                             {
                                                 new { Value = ReportType.ByClients, Title = "По клиентам" },
                                                 new { Value = ReportType.ByOperators, Title = "По операторам" },
                                             };
            reportTypeComboBox.DisplayMemberPath = "Title";
            reportTypeComboBox.SelectedValuePath = "Value";
            reportTypeComboBox.SelectedIndex = 0;
        }

        private void SetActivationStatusLabel()
        {
            if (_license.HasValue)
                statusLabel.Text = "Продукт активирован. Доступно операторов: " + (int) _license.Value;
            else
                statusLabel.Text = "Продукт не активирован.";
        }
    }
}
