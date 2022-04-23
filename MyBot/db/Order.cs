using System;
using System.Collections.Generic;

#nullable disable

namespace MyBot.db
{
    public partial class Order
    {
        public int Id { get; set; }
        public string NumberOfOrder { get; set; }
        public DateTime? DateOrder { get; set; }
        public int? DrugId { get; set; }

        public virtual Drug Drug { get; set; }
    }
}
