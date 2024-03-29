﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MyBot.db
{
    public partial class Drug
    {
        public Drug()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
