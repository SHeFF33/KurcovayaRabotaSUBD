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
    public partial class Администратор : Form
    {
        DataBase dataBase = new DataBase();
        public Администратор()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CreateColumnsUser()
        {
            dataGridViewUsers.Columns.Add("id_user", "ID");
            dataGridViewUsers.Columns.Add("Login", "Логин");
            dataGridViewUsers.Columns.Add("Password", "Пароль");
            var CheckColumn = new DataGridViewCheckBoxColumn();
            CheckColumn.HeaderText = "isAdmin";
            dataGridViewUsers.Columns.Add(CheckColumn);
         
        }
        private void ReadSingleRowUser(IDataRecord record) 
        {
            dataGridViewUsers.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetBoolean(3));
        }

        private void RefreshDataGridUser()
        {
            dataGridViewUsers.Rows.Clear();

            string querystring = $"SELECT * FROM register;";
            SqlCommand cmd = new SqlCommand(querystring, dataBase.GetConnection());
            dataBase.OpenConnection();
            
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                ReadSingleRowUser(reader);
            }
            reader.Close();

            dataBase.CloseConnection();
        }

        private void SaveUser_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();

            for(int index = 0; index < dataGridViewUsers.Rows.Count; index++)
            {
                var id = dataGridViewUsers.Rows[index].Cells[0].Value.ToString();
                var isadmin = dataGridViewUsers.Rows[index].Cells[3].Value.ToString();

                var changeQuery = $"update register set is_admin = '{isadmin}' where id_user = '{id}'";
                var cmd = new SqlCommand(changeQuery, dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
            dataBase.CloseConnection();
            RefreshDataGridUser();
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();
            
            var selectedRowIndex = dataGridViewUsers.CurrentCell.RowIndex;

            var id = Convert.ToInt32(dataGridViewUsers.Rows[selectedRowIndex].Cells[0].Value);

            var deleteQuery = $"delete from register where id_user = '{id}'";
            var cmd = new SqlCommand(deleteQuery, dataBase.GetConnection());
            cmd.ExecuteNonQuery();
            dataBase.CloseConnection();
            RefreshDataGridUser();
        }

        private void Администратор_Load(object sender, EventArgs e)
        {
            CreateColumnsUser();
            RefreshDataGridUser();
        }

        private void buttonAllTables_Click(object sender, EventArgs e)
        {
            alltablesform frm = new alltablesform();
            frm.ShowDialog();
        }
    }
}
