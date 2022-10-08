using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.Model
{
    public class Employee
    {
        public string Name { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public string JoiningDate { get; set; }
        public string BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }

    }
    public enum Gender
    {
        Female,
        Male
    }
    
}
