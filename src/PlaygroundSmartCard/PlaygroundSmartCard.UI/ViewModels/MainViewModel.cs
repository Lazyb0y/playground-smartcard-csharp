using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PlaygroundSmartCard.UI.MVVM;
using SmartCard.Core;
using SmartCard.Core.EventArgs;

namespace PlaygroundSmartCard.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Decleration(s)

        private readonly SmartCardReader _cardReader;
        private string _currentReader;
        private string _selectedReader;

        #endregion

        #region Property(s)

        public ObservableCollection<string> ReaderList { get; set; }

        public string CurrentReader
        {
            get => _currentReader;
            set => SetProperty(ref _currentReader, value);
        }

        public string SelectedReader
        {
            get => _selectedReader;
            set => SetProperty(ref _selectedReader, value);
        }

        #endregion

        #region Command(s)

        public ICommand RefreshReaderListCommand { get; }
        public ICommand SelectReaderCommand { get; }

        #endregion

        #region Constructor(s)

        public MainViewModel()
        {
            _cardReader = new SmartCardReader(SmartCardScope.User);

            ReaderList = new ObservableCollection<string>();

            SmartCardMonitor.Instance.CardStatusChanged += SmartCardMonitor_CardStatusChanged;

            RefreshReaderListCommand = new RelayCommand(ExecuteRefreshReaderListCommand, _ => true);
            SelectReaderCommand = new RelayCommand(ExecuteSelectReaderCommand, _ => true);
        }

        ~MainViewModel()
        {
            SmartCardMonitor.Instance.CardStatusChanged -= SmartCardMonitor_CardStatusChanged;
        }

        #endregion

        #region SmartCardMonitor

        private void SmartCardMonitor_CardStatusChanged(object sender, CardStatusChangedEventArgs e)
        {
            Console.WriteLine($@"Reader: {e.ReaderName}, Card Status: {e.Status}");
        }

        #endregion

        #region Command Action(s)

        private void ExecuteRefreshReaderListCommand(object parameter)
        {
            var result = _cardReader.GetReaderNames();
            if (!result.Success)
            {
                MessageBox.Show($"Unable to get the connected reader list. [{result.ErrorCode}] {result.ErrorMessage}");
                return;
            }

            ReaderList.Clear();

            if (result.Data.Count == 0)
            {
                MessageBox.Show("No smart card reader found.");
                return;
            }

            foreach (var r in result.Data)
            {
                ReaderList.Add(r);
            }

            SelectedReader = ReaderList[0];
        }

        private void ExecuteSelectReaderCommand(object parameter)
        {
            if (_currentReader == null)
            {
                if (string.IsNullOrEmpty(_selectedReader))
                {
                    MessageBox.Show("Select a reader from the combo box first.");
                    return;
                }

                CurrentReader = _selectedReader;
                SmartCardMonitor.Instance.StartMonitoring(_currentReader);
            }
            else
            {
                SmartCardMonitor.Instance.StopMonitoring(_currentReader);
                CurrentReader = null;
            }
        }

        #endregion
    }
}