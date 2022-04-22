using Microsoft.EntityFrameworkCore;
using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    public class Drugs
    {
        private List<Drug> drugs;
        private readonly MyBot_DBContext _dbContext;
        public Drugs(MyBot_DBContext dbContext)
        {
            _dbContext = dbContext;
            drugs = new List<Drug>(_dbContext.Drugs);
        }

        public bool HasDrug(int Id) => drugs.Equals(Id);

        public Drug GetUser(int Id) => drugs[Id];

        public Drug AddUser(int id, string title, string description, int count, int cost)
        {
            if (HasDrug(id))
                return null;

            var drug = new Drug { Id = id, Title = title, Cost = cost, Count = count, Description = description };

            _dbContext.Add(drug);
            _dbContext.SaveChanges();
            drugs = new List<Drug>(_dbContext.Drugs);
            return drug;
        }
    }
}
