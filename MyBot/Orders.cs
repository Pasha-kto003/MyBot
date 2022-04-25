using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    public static class Orders
    {
        private static readonly MyBot_DBContext _dbContext = MyBot_DBContext.GetDB();
        private static List<Order> orders = new List<Order>(_dbContext.Orders);
        private static List<Drug> drugs = new List<Drug>(_dbContext.Drugs);

        public static bool HasOrder(int Id) => orders.Equals(Id);

        public static Order GetOrder(int Id) => orders[Id];

        public static Order AddOrder(int id, string title, DateTime? date, int drugID, int? doctorID)
        {
            if (HasOrder(id))
                return null;

            var order = new Order { Id = id, NumberOfOrder = title, DateOrder = date, DrugId = drugID, DoctorId = doctorID };

            _dbContext.Add(order);
            _dbContext.SaveChanges();
            orders = new List<Order>(_dbContext.Orders);
            return order;
        }
    }
}
