using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    public static class DbInstance
    {
        static MyBot_DBContext connection;
        static object objectLock = new object();

        public static MyBot_DBContext Get()
        {
            lock (objectLock)
            {
                if (connection == null)
                    connection = new MyBot_DBContext();
                return connection;
            }
        }
    }
}
