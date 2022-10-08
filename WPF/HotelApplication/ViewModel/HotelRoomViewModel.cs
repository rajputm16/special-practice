using GalaSoft.MvvmLight;
using HotelApplication.Helper;
using HotelApplication.Model;
using HotelApplication.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace HotelApplication.ViewModel
{
    public class HotelRoomViewModel : ViewModelBase
    {
        HelperClass _helper = HelperClass.HelerClassInstance;
        private ObservableCollection<HotelRooms> _roomDetails;
        public ObservableCollection<HotelRooms> RoomDetails
        {
            get { return _roomDetails; }
            set
            {
                _roomDetails = value;
                RaisePropertyChanged(nameof(RoomDetails));
            }
        }
        private string _roomNumber;
        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                RaisePropertyChanged(nameof(RoomNumber));
            }
        }
        private ObservableCollection<string> _roomTypeList;
        public ObservableCollection<string> RoomTypeList
        {
            get { return _roomTypeList; }
            set
            {
                _roomTypeList = value;
                RaisePropertyChanged(nameof(RoomTypeList));
            }
        }
        private string _selectedRoomType;
        public string SelectedRoomType
        {
            get { return _selectedRoomType; }
            set
            {
                _selectedRoomType = value;
                RaisePropertyChanged(nameof(SelectedRoomType));
            }
        }
        private ObservableCollection<string> _filterRoomNumberList;
        public ObservableCollection<string> FilterRoomNumberList
        {
            get { return _filterRoomNumberList; }
            set
            {
                _filterRoomNumberList = value;
                RaisePropertyChanged(nameof(FilterRoomNumberList));
            }
        }
        private ObservableCollection<string> _filterRoomTpeList;
        public ObservableCollection<string> FilterRoomTypeList
        {
            get { return _filterRoomTpeList; }
            set
            {
                _filterRoomTpeList = value;
                RaisePropertyChanged(nameof(FilterRoomTypeList));
            }
        }
        private string _filterRoomNumber;
        public string FilterRoomNumber
        {
            get { return _filterRoomNumber; }
            set
            {
                _filterRoomNumber = value;
                RaisePropertyChanged(nameof(FilterRoomNumber));
            }
        }
        private string _filterSelectedRoomType;
        public string FilterSelectedRoomType
        {
            get { return _filterSelectedRoomType; }
            set
            {
                _filterSelectedRoomType = value;
                RaisePropertyChanged(nameof(FilterSelectedRoomType));
            }
        }

        private Visibility _filterPressed;
        public Visibility FilterPressedCollapsed
        {
            get { return _filterPressed; }
            set
            {
                _filterPressed = value;
                RaisePropertyChanged(nameof(FilterPressedCollapsed));
            }
        }
        public ICommand FiltersCollapsed{ get; set; }
        public ICommand AddDetails{ get; set; }
        public ICommand UpdateDetails{ get; set; }
        public ICommand DeleteDetails{ get; set; }
        public ICommand ResetAllFilters { get; set; }
        public ICommand FilterCommand { get; set; }
        IRepository<HotelRooms> _roomRepository = new HotelRepository();
        public HotelRoomViewModel()
        {
            GetRoomDetails();
            FiltersCollapsed = new RelayCommand(FilterDetails);
            AddDetails = new RelayCommand(AddClient);
            UpdateDetails = new RelayCommand(UpdateClient);
            DeleteDetails = new RelayCommand(DeleteClient);
            ResetAllFilters = new RelayCommand(ResetFilters);
            FilterCommand = new RelayCommand(Filter);
            RoomTypeList = new ObservableCollection<string>( _helper.GetRoomTypes());
            FilterRoomTypeList = new ObservableCollection<string>(_helper.GetRoomTypes());
            FilterRoomNumberList = new ObservableCollection<string>(_roomRepository.GetRoomDetails());
        }

        public void GetRoomDetails()
        {
            RoomDetails = new ObservableCollection<HotelRooms>(_roomRepository.GetAll());
        }

        private void AddClient(object sender)
        {
            _roomRepository.Add(GetHotelRoom());
        }
        private void Filter(object sender)
        {
            FilterData();
        }
        private void FilterDetails(object sender)
        {
            if (_filterPressed == Visibility.Visible)
                _filterPressed = Visibility.Collapsed;
            else
                _filterPressed = Visibility.Visible;
            RaisePropertyChanged(nameof(FilterPressedCollapsed));
        }
        private void DeleteClient(object sender)
        {
            _roomRepository.Remove(GetHotelRoom());
        }
        private void UpdateClient(object sender)
        {
            _roomRepository.UpdateDetails(GetHotelRoom());
        }
        private void ResetFilters(object sender)
        {
            FilterRoomNumber = string.Empty;
        }
        private void FilterData()
        {
            GetRoomDetails();
            string room = _helper.GetRoomNumber(FilterRoomNumber);
            if (FilterSelectedRoomType == null)
                FilterSelectedRoomType = RoomType.JuniorSuite.ToString();
            RoomType roomType = (RoomType)Enum.Parse(typeof(RoomType), FilterSelectedRoomType);
            RoomDetails = new ObservableCollection<HotelRooms>(RoomDetails
                .Where(x => x.RoomNumber == room || x.RoomType == roomType)
                .Select(x => x));
        }
        private HotelRooms GetHotelRoom()
        {
            return new HotelRooms()
            {
                RoomNumber = RoomNumber,
                RoomType = (RoomType)Enum.Parse(typeof(RoomType), SelectedRoomType)
            };
        }
    }
}
