using HotelApplication.Model;
using HotelApplication.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using HotelApplication.Helper;

namespace HotelApplication.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        private ClientRepository _clientRepo = new ClientRepository();
        private HelperClass _helper = HelperClass.HelerClassInstance;

        #region Properties
        private ObservableCollection<Client> _clientsDetails;
        public ObservableCollection<Client> ClientsDetails
        {
            get { return _clientsDetails; }
            set
            {
                _clientsDetails = value;
                RaisePropertyChanged(nameof(ClientsDetails));
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
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
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
        private string _accountNumber;
        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                _accountNumber = value;
                RaisePropertyChanged(nameof(AccountNumber));
            }
        }
        private string _filterFirstName;
        public string FilterFirstName
        {
            get { return _filterFirstName; }
            set
            {
                _filterFirstName = value;
                RaisePropertyChanged(nameof(FilterFirstName));
            }
        }
        private string _filterLastName;
        public string FilterLastName
        {
            get { return _filterLastName; }
            set
            {
                _filterLastName = value;
                RaisePropertyChanged(nameof(FilterLastName));
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
        private string _filterAccountNumber;
        public string FilterAccountNumber
        {
            get { return _filterAccountNumber; }
            set
            {
                _filterAccountNumber = value;
                RaisePropertyChanged(nameof(FilterAccountNumber));
            }
        }
        private ObservableCollection<string> _roomNumber;
        public ObservableCollection<string> RoomNumberList
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                RaisePropertyChanged(nameof(RoomNumberList));
            }
        }
        private ObservableCollection<string> _editRoomNumber;
        public ObservableCollection<string> FilterRoomNumberList
        {
            get { return _editRoomNumber; }
            set
            {
                if (ClientsDetails != null && ClientsDetails.Count > 0)
                    _editRoomNumber = new ObservableCollection<string>(ClientsDetails.Select(x => x.RoomNumber));
                else
                    _editRoomNumber = value;
                RaisePropertyChanged(nameof(FilterRoomNumberList));
            }
        }
        private string _selectedRoomNumber;
        public string SelectedRoomNumber
        {
            get { return _selectedRoomNumber; }
            set
            {
                _selectedRoomNumber = value;
                RaisePropertyChanged(nameof(SelectedRoomNumber));
            }
        }
        private string _selectedFilterRoomNumber;
        public string SelectedFilterRoomNumber
        {
            get { return _selectedFilterRoomNumber; }
            set
            {
                _selectedFilterRoomNumber = value;
                RaisePropertyChanged(nameof(SelectedFilterRoomNumber));
            }
        }
        public ICommand FiltersCollapsed { get; set; }
        public ICommand AddDetails { get; set; }
        public ICommand UpdateDetails{get;set;}
        public ICommand DeleteDetails{get;set;}
        public ICommand ResetAllFilters{get;set;}
        public ICommand FilterCommand { get; set; }

        #endregion
        public ClientViewModel()
        {
            GetClientDetails();
            FiltersCollapsed = new RelayCommand(FilterDetails);
            AddDetails = new RelayCommand(AddClient);
            UpdateDetails = new RelayCommand(UpdateClient);
            DeleteDetails = new RelayCommand(DeleteClient);
            ResetAllFilters = new RelayCommand(ResetFilters);
            FilterCommand = new RelayCommand(Filter);
            FilterRoomNumberList = new ObservableCollection<string>(_clientRepo.GetRoomDetails());
        }

        public void GetClientDetails()
        {
            ClientsDetails = new ObservableCollection<Client>(_clientRepo.GetAll());
            RoomNumberList = new ObservableCollection<string>(_helper.GenerateRoomNumbers());
        }
        private void Filter(object sender)
        {
            FilterClientDetails();
            RaisePropertyChanged(nameof(ClientsDetails));
        }
        private void AddClient(object sender)
        {
            _clientRepo.Add(GetClient());
            RaisePropertyChanged(nameof(ClientsDetails));
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
            _clientRepo.Remove(GetClient());
            RaisePropertyChanged(nameof(ClientsDetails));
        }
        private void UpdateClient(object sender)
        {
            _clientRepo.UpdateDetails(GetClient());
            RaisePropertyChanged(nameof(ClientsDetails));
        }
        private void ResetFilters(object sender)
        {
            FilterFirstName = string.Empty;
            FilterLastName = string.Empty;
            FilterBirthDate = string.Empty;
            FilterAccountNumber = string.Empty;
            SelectedFilterRoomNumber = string.Empty;
            GetClientDetails();
        }
        private void FilterClientDetails()
        {
            string room = _helper.GetRoomNumber(SelectedFilterRoomNumber);
            ClientsDetails = new ObservableCollection<Client>(ClientsDetails
                .Where(x => x.FirstName == FilterFirstName ||
                x.LastName == FilterLastName ||
                x.RoomNumber == room ||
                x.BirthDate == FilterBirthDate ||
                x.Account == FilterAccountNumber).Select(x => x));
        }
        private Client GetClient()
        {
            string room = _helper.GetRoomNumber(SelectedRoomNumber);
            return new Client()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                Account = AccountNumber,
                RoomNumber = room
            };
        }

    }
}
