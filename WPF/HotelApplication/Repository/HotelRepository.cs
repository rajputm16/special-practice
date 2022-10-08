using HotelApplication.DBContext;
using HotelApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HotelApplication.Repository
{
    interface IRepository<T>
    {
        public void UpdateDetails(T obj);
        public void Add(T obj);
        public void AddRange(List<T> obj);
        public T Find(int id);
        public List<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        public void Remove(T obj);
        public List<string> GetRoomDetails();
        public void Dispose();
    }
    public class HotelRepository :GenericRepository<HotelRooms>, IRepository<HotelRooms>
    {
        
    }
}
