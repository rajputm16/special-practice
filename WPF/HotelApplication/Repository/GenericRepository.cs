using HotelApplication.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.Repository
{
    public class GenericRepository<T>: IRepository<T> where T: class
    {
        HotelDbContext dbContext = new HotelDbContext();
        public void Add(T obj)
        {
            dbContext.Add<T>(obj);
            dbContext.SaveChanges();
        }
        public List<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> clients = new List<T>();
            try
            {
                IQueryable<T> dbQuery = dbContext.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                var queryResult = dbQuery.AsNoTracking();
                if (queryResult != null && queryResult.Count() > 0)
                    clients = queryResult.ToList<T>();
            }
            catch (Exception)
            {
                return new List<T>();
            }
            return clients;
        }
        public T Find(int id)
        {
            T client = dbContext.Find<T>(id);
            return client;
        }

        public void Remove(T obj)
        {
            dbContext.Add<T>(obj);
            dbContext.SaveChanges();
        }

        public void UpdateDetails(T obj)
        {
            dbContext.Add<T>(obj);
            dbContext.SaveChanges();
        }
        public void AddRange(List<T> obj)
        {
            dbContext.AddRange(obj);
            dbContext.SaveChanges();
        }
        public List<string> GetRoomDetails()
        {
            return dbContext.Client.Select(x => x.RoomNumber).ToList();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
