using System;
using System.Collections.ObjectModel;
using System.Windows;
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
        private SmartCard.Core.SmartCard _smartCard;
        private bool _isCardInserted;
        private SmartCardType? _cardType;

        #endregion

        #region Property(s)

        public ObservableCollection<string> ReaderList { get; set; }

        public string SelectedReader
        {
            get => _selectedReader;
            set
            {
                SetProperty(ref _selectedReader, value);
                SelectReaderCommand.RaiseCanExecuteChanged();
            }
        }

        public string CurrentReader
        {
            get => _currentReader;
            set
            {
                SetProperty(ref _currentReader, value);
                SelectReaderCommand.RaiseCanExecuteChanged();
            }
        }

        public SmartCard.Core.SmartCard SmartCard
        {
            get => _smartCard;
            set => SetProperty(ref _smartCard, value);
        }

        public bool IsCardInserted
        {
            get => _isCardInserted;
            set => SetProperty(ref _isCardInserted, value);
        }

        public SmartCardType? CardType
        {
            get => _cardType;
            set => SetProperty(ref _cardType, value);
        }

        #endregion

        #region Command(s)

        public RelayCommand GetReadersCommand { get; }
        public RelayCommand SelectReaderCommand { get; }
        public RelayCommand DeselectReaderCommand { get; }
        public RelayCommand ConnectCommand { get; }
        public RelayCommand DisconnectCommand { get; }

        #endregion

        #region Constructor(s)

        public MainViewModel()
        {
            _cardReader = new SmartCardReader(SmartCardScope.User);

            ReaderList = new ObservableCollection<string>();

            SmartCardMonitor.Instance.CardStatusChanged += SmartCardMonitor_CardStatusChanged;

            GetReadersCommand = new RelayCommand(ExecuteGetReadersCommand, _ => true);
            SelectReaderCommand = new RelayCommand(ExecuteSelectReaderCommand,
                _ => !string.IsNullOrEmpty(_selectedReader) && string.IsNullOrEmpty(_currentReader));
            DeselectReaderCommand = new RelayCommand(ExecuteDeselectReaderCommand, _ => true);
            ConnectCommand = new RelayCommand(ExecuteConnectCommand, _ => true);
            DisconnectCommand = new RelayCommand(ExecuteDisconnectCommand, _ => true);
        }

        ~MainViewModel()
        {
            SmartCardMonitor.Instance.CardStatusChanged -= SmartCardMonitor_CardStatusChanged;
        }

        #endregion

        #region Method(s)

        private void GetCardInfo()
        {
            try
            {
                var result = _smartCard.GetATR();
                if (!result.Success)
                {
                    MessageBox.Show($"Unable to get the card info: [{result.ErrorCode}] {result.ErrorMessage}");
                    return;
                }

                CardType = SmartCardIdentifier.Identify(new ATR(result.Data));
            }
            catch (Exception x)
            {
                MessageBox.Show($"Unable to get the card info: {x.Message}");
            }
        }

        #endregion

        #region SmartCardMonitor

        private void SmartCardMonitor_CardStatusChanged(object sender, CardStatusChangedEventArgs e)
        {
            Console.WriteLine($@"Reader: {e.ReaderName}, Card Status: {e.Status}");
            IsCardInserted = e.Status == SmartCardStatus.Inserted;
        }

        #endregion

        #region Command Action(s)

        private void ExecuteGetReadersCommand(object parameter)
        {
            var result = _cardReader.GetReaderNames();
            if (!result.Success)
            {
                MessageBox.Show($"Unable to get the connected reader list. [{result.ErrorCode}] {result.ErrorMessage}");
                return;
            }

            ReaderList.Clear();
            SelectedReader = null;

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
            if (!string.IsNullOrEmpty(_currentReader))
            {
                MessageBox.Show("A reader is already selected.");
                return;
            }

            if (string.IsNullOrEmpty(_selectedReader))
            {
                MessageBox.Show("Select a reader from the combo box first.");
                return;
            }

            var success = SmartCardMonitor.Instance.StartMonitoring(_selectedReader);
            if (!success)
            {
                MessageBox.Show("Unable to start the reader monitor service.");
                return;
            }

            CurrentReader = _selectedReader;
        }

        private void ExecuteDeselectReaderCommand(object parameter)
        {
            if (string.IsNullOrEmpty(_currentReader))
            {
                return;
            }

            SmartCardMonitor.Instance.StopMonitoring(_currentReader);
            CurrentReader = null;
        }

        private void ExecuteConnectCommand(object parameter)
        {
            if (_smartCard != null)
            {
                MessageBox.Show("Please disconnect the previous card first!");
                return;
            }

            try
            {
                var card = new SmartCard.Core.SmartCard(SmartCardScope.User)
                {
                    ReaderName = _currentReader
                };

                var result = card.Connect();
                if (!result.Success)
                {
                    MessageBox.Show($"Unable to connect to the card. [{result.ErrorCode}] {result.ErrorMessage}");
                    return;
                }

                SmartCard = card;
                GetCardInfo();
            }
            catch (Exception x)
            {
                MessageBox.Show($"Unable to connect to the card: {x.Message}");
            }
        }

        private void ExecuteDisconnectCommand(object parameter)
        {
            if (_smartCard == null)
            {
                MessageBox.Show("No smart card is connected");
                return;
            }

            try
            {
                var result = _smartCard.Disconnect();
                if (!result.Success)
                {
                    MessageBox.Show($"Unable to disconnect to the card. [{result.ErrorCode}] {result.ErrorMessage}");
                    return;
                }

                SmartCard = null;
                CardType = null;
            }
            catch (Exception x)
            {
                MessageBox.Show($"Unable to disconnect to the card: {x.Message}");
            }
        }

        #endregion
    }
}