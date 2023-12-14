using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppKursovaya
{
    public partial class Loginform : Form
    {
        DataBase database = new DataBase();

        public static int idUser;
        public static int idAdmin;
        public Loginform()
        {
            InitializeComponent();
            this.PasswordBox1.AutoSize = false;
            this.PasswordBox1.Size = new Size(this.PasswordBox1.Size.Width, 41);

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginBox1.MaxLength= 50;
            PasswordBox1.MaxLength= 50;
        }

        public void login_Click(object sender, EventArgs e)
        {
            var loginUser = LoginBox1.Text;
            var passUser = md5.hashPassword(PasswordBox1.Text);
            SqlDataAdapter adapter= new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand($"select id_user, login_user, password_user, is_admin from register where login_user = '{loginUser}' and password_user = '{passUser}'", database.GetConnection());
            adapter.SelectCommand= command;
            adapter.Fill(table);
            
            if(table.Rows.Count == 1 ) 
            {
                var user = new checkUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));


                MessageBox.Show("Вы успешно вошли!", "Успешно!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (user.IsAdmin == true)
                {
                    database.OpenConnection();
                    SqlCommand command1 = new SqlCommand($"SELECT id_user FROM register WHERE login_user = '{loginUser}'", database.GetConnection());
                    var result = command1.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        idAdmin = Convert.ToInt32(result);
                    }
                    database.CloseConnection();
                    FormPhones frm1 = new FormPhones();
                    this.Hide();
                    frm1.ShowDialog();
                    this.Show();
                }
                else if(user.IsAdmin == false)
                {
                    database.OpenConnection();
                    SqlCommand command1 = new SqlCommand($"SELECT id_user FROM register WHERE login_user = '{loginUser}';", database.GetConnection());
                    var result = command1.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        idUser = Convert.ToInt32(result);
                    }
                    database.CloseConnection();
                    FormUsers frm1= new FormUsers();
                    this.Hide(); 
                    frm1.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registrationform regfrm = new Registrationform();
            regfrm.Show();
            this.Hide();
        }
    }
}