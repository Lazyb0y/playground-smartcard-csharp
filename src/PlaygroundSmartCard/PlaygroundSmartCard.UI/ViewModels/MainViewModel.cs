using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PlaygroundSmartCard.UI.MVVM;
using SmartCard.Core;

namespace PlaygroundSmartCard.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Decleration(s)

        private readonly SmartCardReader _cardReader;
        private string _selectedReader;

        #endregion

        #region Property(s)

        public ObservableCollection<string> ReaderList { get; set; }

        public string SelectedReader
        {
            get => _selectedReader;
            set => SetProperty(ref _selectedReader, value);
        }

        #endregion

        #region Command(s)

        public ICommand RefreshReaderListCommand { get; }

        #endregion

        #region Constructor(s)

        public MainViewModel()
        {
            _cardReader = new SmartCardReader(SmartCardScope.User);

            ReaderList = new ObservableCollection<string>();

            RefreshReaderListCommand =
                new RelayCommand(ExecuteRefreshReaderListCommand, CanExecuteRefreshReaderListCommand);
        }

        #endregion

        #region Command Action(s)

        private void ExecuteRefreshReaderListCommand(object parameter)
        {
            var result = _cardReader.GetReaderNames();
            if (!result.Success)
            {
                MessageBox.Show($"Unable to get the connected reader list. [{result.ErrorCode}] {result.ErrorMessage}");
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

        private bool CanExecuteRefreshReaderListCommand(object parameter)
        {
            return true;
        }

        #endregion
    }
}