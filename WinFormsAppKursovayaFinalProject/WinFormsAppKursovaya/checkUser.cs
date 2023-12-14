using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppKursovaya
{
    public class checkUser
    {
        public string Login { get; set; }
        public bool IsAdmin { get;}

        // лямбда-выражение: тернарная операция:  при получении 1 в переменной isAdmin будет выводится Администратор, а если 0, то будет выводится Пользователь 
        public string Status => IsAdmin ? "Администратор" : "Пользователь";

        public checkUser(string login, bool isAdmin) 
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }
    }
}
