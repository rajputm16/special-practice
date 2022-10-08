using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace HotelApplication.Model
{
    public class HotelRooms
    {
        [Key]
        public string RoomNumber { get; set; }
        [DataMember]
        public RoomType RoomType { get; set; }        
    }
    public enum RoomType
    {
        PresedentialSuite,
        Standard,
        JuniorSuite
    }
}
