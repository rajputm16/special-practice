using HotelApplication.DBContext;
using HotelApplication.Model;
using HotelApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace HotelApplication.Helper
{
    public class HelperClass
    {
        private static  HelperClass _helperObject;
        private static readonly object _lockObject = new();

        public static HelperClass HelerClassInstance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_helperObject == null)
                        _helperObject = new HelperClass();
                    return _helperObject;
                }
            }
        }
        public  List<string> GenerateRoomNumbers()
        {
            List<string> roomNumbers = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = (int)RoomType.PresedentialSuite; j <= (int)RoomType.JuniorSuite; j++)
                {
                    string prefix = (((RoomType)j).ToString()[0]).ToString();
                    roomNumbers.Add($"{prefix}{i} - {(RoomType)j}");
                }
            }
            return roomNumbers;
        }
        public List<string> GetRoomTypes()
        {
            List<string> roomTypes = new List<string>();
            for (int j = (int)RoomType.PresedentialSuite; j <= (int)RoomType.JuniorSuite; j++)
            {
                roomTypes.Add($"{(RoomType)j}");
            }
            return roomTypes;
        }
        public List<string> GetGenderList()
        {
            List<string> gender = new List<string>();
            for (int j = (int)Gender.Female; j <= (int)Gender.Male; j++)
            {
                gender.Add($"{(Gender)j}");
            }
            return gender;
        }
        public string GetRoomNumber(string roomFormat)
        {
            if (string.IsNullOrEmpty(roomFormat))
                return string.Empty;
            return roomFormat.Split('-', StringSplitOptions.RemoveEmptyEntries)[0];
        }
        public string GetRoomType(string roomFormat)
        {
            if (string.IsNullOrEmpty(roomFormat))
                return string.Empty;
            return roomFormat.Split('-', StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}
