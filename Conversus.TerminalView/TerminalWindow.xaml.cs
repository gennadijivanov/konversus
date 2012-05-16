﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}