using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPFExercise.DesktopClient.Helper;
using WPFExercise.ServiceFoundation.Enum;
using WPFExercise.Windows;


namespace WPFExercise.DesktopClient.ViewModels
{
    /// <summary>
    /// Filterviewmodel class implemented by ViewModel abstract class
    /// </summary>
    public class FilterViewModel : ViewModel
    {
        #region Fields

        private int? _filteredNumber;
        private bool _isEnableInput;
        private bool _isEnableFilter;
        private string _outputValueList;
        private string _selectedFilterValue;
        private ICommand _enterCommand;
        private ICommand _updateCommand;
        private ObservableCollection<int> _inputValueList;
        private const string ValidateMessage = "Input value can not be greater than integer max value";

        #endregion

        #region Properties
        /// <summary>
        /// WPF supports two way databinding so
        /// The filternumber input value  textbox that I defined in the UI is bind to the viewmodel
        /// When the user enters a value smaller or equal 0 input value textbox disables(IsEnableInput=false) and filter combobox is enable(IsEnableFilter)
        /// </summary>
        public int? FilteredNumber
        {
            get { return _filteredNumber; }
            set
            {
                _filteredNumber = value;
                if (_filteredNumber <= 0)
                {
                    _isEnableFilter = true;
                    _isEnableInput = false;
                    NotifyPropertyChanged("IsEnableFilter");
                    NotifyPropertyChanged("IsEnableInput");
                }

                NotifyPropertyChanged("FilteredNumber");
            }
        }

        public string SelectedFilterValue
        {
            get { return _selectedFilterValue; }
            set
            {
                _selectedFilterValue = value;
                NotifyPropertyChanged("SelectedFilterValue");
            }
        }

        public bool IsEnableInput
        {
            get { return _isEnableInput; }
            set
            {
                _isEnableInput = value;
                NotifyPropertyChanged("IsEnableInput");
            }
        }

        public bool IsEnableFilter
        {
            get { return _isEnableFilter; }
            set
            {
                _isEnableFilter = value;
                NotifyPropertyChanged("IsEnableFilter");
            }
        }
        /// <summary>
        /// This is the list of data to be added to the listbox.
        /// </summary>
        public ObservableCollection<int> InputValueList
        {
            get { return _inputValueList; }
            set
            {
                _inputValueList = value;
                NotifyPropertyChanged("InputValueList");
            }
        }
        /// <summary>
        /// The output of the list filtered by the selected filter type.
        /// </summary>
        public string OutputValueList
        {
            get { return _outputValueList; }
            set
            {
                _outputValueList = value;
                NotifyPropertyChanged("OutputValueList");
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// _inputValueList and _isEnableInput is initialized 
        /// </summary>
        public FilterViewModel()
        {
            _inputValueList = new ObservableCollection<int>();
            _isEnableInput = true;
        }

        #endregion

        #region Commands
        /// <summary>
        /// ICommand interface, that defines an event and two methods: Execute() and CanExecute(). The first one is for performing
        /// the actual action, while the second one is for determining whether the action is currently available.
        /// </summary>
        public ICommand EnterCommand
        {
            get
            {
                return _enterCommand ??
                       (_enterCommand = new HandlerCommand(() => EnterCommandExecute(), () => CanExecute));
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand =
                           new HandlerCommand(() => UpdateCommandExecute(), () => CanUpdateExecute));
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Ok button to be enabled or disabled depending on input value   
        /// </summary>
        public bool CanExecute
        {
            get { return FilteredNumber != null && FilteredNumber > 0 && FilteredNumber <= int.MaxValue; }
        }
        /// <summary>
        /// Update button to be enabled or disabled depending on SelectedFilterValue and InputValueList 
        /// </summary>
        public bool CanUpdateExecute
        {
            get { return SelectedFilterValue != null && InputValueList.Any(); }
        }
        /// <summary>
        /// This method is called by clicking the ok button and add valid input value to InputValueList.
        /// Listbox which is screen fills from input Value List
        /// </summary>
        public void EnterCommandExecute()
        {
            if (FilteredNumber != null)
                InputValueList.Add(FilteredNumber.Value);
            FilteredNumber = null;
        }
        /// <summary>
        /// This method is called by clicking the update button and filtered by the selected filter type
        /// Calling the corresponding filtering method according to the selected enum value
        /// </summary>
        public void UpdateCommandExecute()
        {
            int filteredValue = (int)(FilterOptionsEnum)Enum.Parse(typeof(FilterOptionsEnum), SelectedFilterValue);

            switch (filteredValue)
            {
                case 1:
                    FilterByIncreaseOrder();
                    break;
                case 2:
                    FilterByDecreaseOrder();
                    break;
                case 3:
                    FilterBySumInput();
                    break;
                case 4:
                    FilterByOddInput();
                    break;
                case 5:
                    FilterByEvenInput();
                    break;
            }
        }
        /// <summary>
        /// Filterviewmodel class implemented by ViewModel abstract class so
        /// OnValide method which defined in the ViewModel abstract class was override in FilterViewModel class.
        /// Derived classes of the abstract class must be implemented all abstract method 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string OnValidate(string propertyName)
        {
            if (FilteredNumber != null && FilteredNumber >= int.MaxValue)
            {
                return ValidateMessage;
            }

            return base.OnValidate(propertyName);
        }

        /// <summary>
        /// Sorted input values in listbox  in increasing order .duplicate values were skipped by distinct method
        /// </summary>
        private void FilterByIncreaseOrder()
        {
            var filteredData = InputValueList.ToList()
                                                .Distinct()
                                                .OrderBy(x => x);

            _outputValueList = string.Join(",", filteredData);
            NotifyPropertyChanged("OutputValueList");
        }
        /// <summary>
        /// Sorted input values in listbox by decreasing order.Duplicate values were skipped by distinct method
        /// </summary>
        private void FilterByDecreaseOrder()
        {
            var filteredData = InputValueList.ToList()
                                            .Distinct()
                                            .OrderByDescending(x => x);

            _outputValueList = string.Join(",", filteredData);
            NotifyPropertyChanged("OutputValueList");
        }

        /// <summary>
        /// the input values were summed.
        /// </summary>
        private void FilterBySumInput()
        {
            var sumData = InputValueList.ToList()
                                        .Distinct()
                                        .Sum();

            _outputValueList = Convert.ToString(sumData);
            NotifyPropertyChanged("OutputValueList");
        }

        /// <summary>
        /// Filtered odd input values by "Where(x => x % 2 == 1)"
        /// </summary>
        private void FilterByOddInput()
        {
            var oddData = InputValueList.ToList()
                                    .Distinct()
                                    .Where(x => x % 2 == 1)
                                    .OrderBy(t => t);

            _outputValueList = string.Join(",", oddData);
            NotifyPropertyChanged("OutputValueList");
        }
        /// <summary>
        /// Filtered even input values Where(x => x % 2 == 0)
        /// </summary>
        private void FilterByEvenInput()
        {
            var evenData = InputValueList.ToList()
                                         .Distinct()
                                         .Where(x => x % 2 == 0)
                                         .OrderBy(t => t);

            _outputValueList = string.Join(",", evenData);
            NotifyPropertyChanged("OutputValueList");
        }

        #endregion
    }
}
