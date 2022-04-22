using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    public class Orders
    {
        private List<Order> orders;
        private readonly MyBot_DBContext _dbContext;
        public Orders(MyBot_DBContext dbContext)
        {
            _dbContext = dbContext;
            orders = new List<Order>(_dbContext.Orders);
        }

        public bool HasOrder(int Id) => orders.Equals(Id);

        public Order GetOrder(int Id) => orders[Id];

        public Order AddUser(int id, string title, DateTime date, int drugID, int workerID)
        {
            if (HasOrder(id))
                return null;

            var order = new Order { Id = id, NumberOfOrder = title, DateOrder = date, DrugId = drugID, WorkerId = workerID };

            _dbContext.Add(order);
            _dbContext.SaveChanges();
            orders = new List<Order>(_dbContext.Orders);
            return order;
        }
    }
}
