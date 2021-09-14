using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HomeChat.Core
{
    public static class DBHelper
    {
        public static string GetConnectionString(string name = "HomeChatDatabase")
        {
            return "Server=(localdb)\\MSSQLLocalDB;Database=HomeChatDatabase;Trusted_Connection=True;"; 
        }
    }
}
