using GalaSoft.MvvmLight;
using HotelApplication.Helper;
using HotelApplication.Model;
using HotelApplication.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelApplication.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private IRepository<Employee> _employeeRepo = new EmployeeRepository();
        HelperClass _helper = HelperClass.HelerClassInstance;

        #region Properties
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }
        private Visibility _isFilterPressed;
        public Visibility IsFilterPressed
        {
            get { return _isFilterPressed; }
            set
            {
                _isFilterPressed = value;
                FilterDetails(this);
                RaisePropertyChanged(nameof(IsFilterPressed));
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        private int _employeeId;
        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                RaisePropertyChanged(nameof(EmployeeId));
            }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }
        private string _birthDate;
        public string BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged(nameof(BirthDate));
            }
        }
        private string _joiningDate;
        public string JoiningDate
        {
            get { return _joiningDate; }
            set
            {
                _joiningDate = value;
                RaisePropertyChanged(nameof(JoiningDate));
            }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                RaisePropertyChanged(nameof(Gender));
            }
        }
        private int _filterEmployeeId;
        public int FilterEmployeeId
        {
            get { return _filterEmployeeId; }
            set
            {
                _filterEmployeeId = value;
                RaisePropertyChanged(nameof(FilterEmployeeId));
            }
        }
        private string _filterName;
        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                RaisePropertyChanged(nameof(FilterName));
            }
        }
        private string _filterEmail;
        public string FilterEmail
        {
            get { return _filterEmail; }
            set
            {
                _filterEmail = value;
                RaisePropertyChanged(nameof(FilterEmail));
            }
        }
        private string _filterBirthDate;
        public string FilterBirthDate
        {
            get { return _filterBirthDate; }
            set
            {
                _filterBirthDate = value;
                RaisePropertyChanged(nameof(FilterBirthDate));
            }
        }
        private string _filterJoiningDate;
        public string FilterJoiningDate
        {
            get { return _filterJoiningDate; }
            set
            {
                _filterJoiningDate = value;
                RaisePropertyChanged(nameof(FilterJoiningDate));
            }
        }
        private string _filterGender;
        public string FilterGender
        {
            get { return _filterGender; }
            set
            {
                _filterGender = value;
                RaisePropertyChanged(nameof(FilterGender));
            }
        }
        private ObservableCollection<string> _genderList;
        public ObservableCollection<string> GenderList
        {
            get { return _genderList; }
            set
            {
                _genderList = value;
                RaisePropertyChanged(nameof(GenderList));
            }
        }
        
        public ICommand FiltersCollapsed { get; set; }
        public ICommand AddDetails { get; set; }
        public ICommand UpdateDetails { get; set; }
        public ICommand DeleteDetails { get; set; }
        public ICommand ResetAllFilters { get; set; }
        public ICommand FilterCommand { get; set; }

        #endregion
        public EmployeeViewModel()
        {
            GetEmployeeDetails();
            FiltersCollapsed = new RelayCommand(FilterDetails);
            AddDetails = new RelayCommand(AddClient);
            UpdateDetails = new RelayCommand(UpdateClient);
            DeleteDetails = new RelayCommand(DeleteClient);
            ResetAllFilters = new RelayCommand(ResetFilters);
            FilterCommand = new RelayCommand(Filter);
            GenderList = new ObservableCollection<string>(_helper.GetGenderList());
        }

        public void GetEmployeeDetails()
        {
            Employees = new ObservableCollection<Employee>(_employeeRepo.GetAll());
            GenderList = new ObservableCollection<string>(_helper.GetGenderList());
        }
        private void Filter(object sender)
        {
            FilterClientDetails();
            RaisePropertyChanged(nameof(Employees));
        }
        private void AddClient(object sender)
        {
            _employeeRepo.Add(GetEmployee());
            RaisePropertyChanged(nameof(Employees));
        }
        private void FilterDetails(object sender)
        {
            if (_isFilterPressed == Visibility.Visible)
                _isFilterPressed = Visibility.Collapsed;
            else
                _isFilterPressed = Visibility.Visible;

            RaisePropertyChanged(nameof(IsFilterPressed));
        }
        private void DeleteClient(object sender)
        {
            _employeeRepo.Remove(GetEmployee());
            RaisePropertyChanged(nameof(Employees));
        }
        private void UpdateClient(object sender)
        {
            _employeeRepo.UpdateDetails(GetEmployee());
            RaisePropertyChanged(nameof(Employees));
        }
        private void ResetFilters(object sender)
        {
            FilterName = string.Empty;
            FilterEmail = string.Empty;
            FilterBirthDate = string.Empty;
            FilterJoiningDate = string.Empty;
            FilterGender = string.Empty;
            FilterEmployeeId = 0;
            GetEmployeeDetails();
        }
        private void FilterClientDetails()
        {
            Gender gender;
            if (FilterGender == null)
                gender = Model.Gender.Female;
            gender = (Gender)Enum.Parse(typeof(Gender), FilterGender);
            Employees = new ObservableCollection<Employee>(Employees
                .Where(x => x.Name == FilterName ||
                x.EmployeeId == FilterEmployeeId ||
                x.Email == FilterEmail ||
                x.Gender == gender ||
                x.BirthDate == FilterBirthDate ||
                x.JoiningDate == FilterJoiningDate).Select(x => x));
        }
        private Employee GetEmployee()
        {
            Gender gender = (Gender)Enum.Parse(typeof(Gender), Gender);
            return new Employee()
            {
                Name = Name,
                Email = Email,
                BirthDate = BirthDate,
                JoiningDate = JoiningDate,
                Gender = gender,
                EmployeeId = EmployeeId,
        };
        }
    }
}
