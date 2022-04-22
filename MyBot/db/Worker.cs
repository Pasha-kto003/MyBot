using System;
using System.Collections.Generic;

#nullable disable

namespace MyBot.db
{
    public partial class Worker
    {
        public Worker()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronimyc { get; set; }
        public int? PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
