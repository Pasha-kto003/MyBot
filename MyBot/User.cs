using MyBot.db;
using MyBot.UserStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public StateMachine State { get; set; }
        public Drug Drug { get; set; }
        public int DrugId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        public User()
        {
            State = new StateMachine(this, new DefaultState());
        }
    }
}
