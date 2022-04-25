using System;
using System.Collections.Generic;

#nullable disable

namespace MyBot.db
{
    public partial class Doctor
    {
        public Doctor()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
