using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelApplication.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoomNumber { get; set; }
        public string Account { get; set; }
        public string BirthDate { get; set; }

        public Client()
        {
            BirthDate = DateTime.Now.ToString("dd-MMM-yyyy");
        }
    }
}
