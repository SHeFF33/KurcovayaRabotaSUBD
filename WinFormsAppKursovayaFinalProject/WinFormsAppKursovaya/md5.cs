using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WinFormsAppKursovaya
{
    // класс для шифрования паролей
    class md5
    {
        // метод шифрования паролей
        public static string hashPassword(string password)
        {
            // создаем объект класса MD5 
            MD5 md5= MD5.Create();

            //массив байт переводится в 7-ми-разрядные числа 
            byte[] b = Encoding.ASCII.GetBytes(password);
            // массив байт переводится в хэш-функцию
            byte[] hash = md5.ComputeHash(b);

            // Перевод в другую разрядность
            StringBuilder sb = new StringBuilder();
            foreach(var a in hash)
            {
                sb.Append(a.ToString("X2"));
            }
            return Convert.ToString(sb);
        }
    }
}
