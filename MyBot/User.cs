﻿using MyBot.UserStates;
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

        public User()
        {
            State = new StateMachine(this, new DefaultState());
        }
    }
}
