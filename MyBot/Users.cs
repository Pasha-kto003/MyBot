using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    public class Users
    {
        static Dictionary<long, User> users = new Dictionary<long, User>();

        public static bool HasUser(long Id) => users.ContainsKey(Id);

        public static User GetUser(long Id) => users[Id];

        public static User AddUser(long Id, string name)
        {
            if (HasUser(Id))
                return null;
            var user = new User { Id = Id, UserName = name };
            users.Add(Id, user);
            return user;
        }
    }
}
