using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsAppKursovaya
{
    public partial class Registrationform : Form
    {
        DataBase dataBase = new DataBase();
        public Registrationform()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Createakk_Click(object sender, EventArgs e)
        {
            var login = UsercreateBox1.Text;
            var password = md5.hashPassword(PaccwordcreateBox1.Text);
            if (checkuser())
            {
                return;
            }

            SqlCommand command = new SqlCommand($"insert into register(login_user, password_user, is_admin) values ('{login}','{password}', 0)", dataBase.GetConnection());
            
            dataBase.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                Loginform logfrm = new Loginform();
                this.Hide();
                logfrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.CloseConnection();
        }
        private Boolean checkuser()
        {
            var loginuser = UsercreateBox1.Text;
            var passworduser = PaccwordcreateBox1.Text;

            SqlDataAdapter adapter= new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand($"select id_user, login_user, password_user is_admin from register where login_user = '{loginuser}' and password_user = '{passworduser}'", dataBase.GetConnection());

            adapter.SelectCommand= command;
            adapter.Fill(table);
            if(table.Rows.Count > 0 ) 
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
