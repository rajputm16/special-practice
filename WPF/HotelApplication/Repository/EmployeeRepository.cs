using HotelApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IRepository<Employee>
    {
    }
}
