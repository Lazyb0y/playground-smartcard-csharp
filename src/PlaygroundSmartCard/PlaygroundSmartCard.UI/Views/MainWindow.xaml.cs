using System;
using System.Windows;
using SmartCard.Core;

namespace PlaygroundSmartCard.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            SmartCardMonitor.Instance.StopAllMonitoring();
        }
    }
}