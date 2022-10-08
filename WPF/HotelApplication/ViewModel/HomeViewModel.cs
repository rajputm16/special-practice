using GalaSoft.MvvmLight;
using HotelApplication.Helper;
using HotelApplication.Model;
using HotelApplication.Repository;
using System;
using System.Collections.Generic;

namespace HotelApplication.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        HelperClass _helper = HelperClass.HelerClassInstance;
        IRepository<HotelRooms> _hotelRepo = new HotelRepository();
       
        public HomeViewModel()
        {
            AddHotelRoomRange();
        }
        private void AddHotelRoomRange()
        {
            var hotel = _hotelRepo.GetAll();
            if (hotel == null || hotel.Count == 0)
            {
                var rooms = _helper.GenerateRoomNumbers();
                List<HotelRooms> hotelData = new List<HotelRooms>();
                foreach (var value in rooms)
                {
                    hotelData.Add(new HotelRooms()
                    {
                        RoomNumber = _helper.GetRoomNumber(value),
                        RoomType = (RoomType)Enum.Parse(typeof(RoomType), _helper.GetRoomType(value))
                    });
                }
                _hotelRepo.AddRange(hotelData);
            }
        }
    }
}
