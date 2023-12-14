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
    public partial class alltablesform : Form
    {
        public alltablesform()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        DataBase dataBase = new DataBase();
        private void alltablesform_Load(object sender, EventArgs e)
        {
            SyncAdminPhones();
            CreateColumnsPhones();
            RefreshDataGridPhones(dataGridViewPhones);

            SyncAdminAccess();
            CreateColumnsAccess();
            RefreshDataGridAccess(dataGridViewAccess);

            SyncAdminServices();
            CreateColumnsServices();
            RefreshDataGridServices(dataGridViewServices);

        }
        private void SyncAdminPhones()
        {
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("SyncAdminPhones", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();
        }
        private void SyncAdminAccess()
        {
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("SyncAdminAccess", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();
        }
        private void SyncAdminServices()
        {
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("SyncAdminServices", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();
        }
        private void CreateColumnsPhones()
        {
            dataGridViewPhones.Columns.Add("ID", "ID");
            dataGridViewPhones.Columns.Add("PhoneID", "ID телефона");
            dataGridViewPhones.Columns.Add("Brand", "Производитель");
            dataGridViewPhones.Columns.Add("Model", "Модель");
            dataGridViewPhones.Columns.Add("Specifications", "Характеристики");
            dataGridViewPhones.Columns.Add("PurchasePrice", "Цена приема");
            dataGridViewPhones.Columns.Add("SellingPrice", "Цена продажи");
            dataGridViewPhones.Columns.Add("Status", "Статус");
        }
        private void ReadSingleRowsPhones(DataGridView dgu, IDataRecord record)
        {
            // Проверка наличия нулевых значений и добавление их в DataGridView
            dgu.Rows.Add(
                record.IsDBNull(0) ? (int?)null : record.GetInt32(0),
                record.IsDBNull(1) ? (int?)null : record.GetInt32(1),
                record.IsDBNull(2) ? null : record.GetString(2),
                record.IsDBNull(3) ? null : record.GetString(3),
                record.IsDBNull(4) ? null : record.GetString(4),
                record.IsDBNull(5) ? (int?)null : record.GetInt32(5),
                record.IsDBNull(6) ? (int?)null : record.GetInt32(6),
                record.IsDBNull(8) ? null : record.GetString(8)
            );
        }
        private void RefreshDataGridPhones(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from AdminPhones";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsPhones(dgv, reader);
            }
            reader.Close();
        }

        private void CreateColumnsAccess()
        {
            dataGridViewAccess.Columns.Add("AccesID", "ID");
            dataGridViewAccess.Columns.Add("Kategories", "Категория");
            dataGridViewAccess.Columns.Add("Color", "Цвет");
            dataGridViewAccess.Columns.Add("Title", "Название");
            dataGridViewAccess.Columns.Add("Price", "Цена");
            dataGridViewAccess.Columns.Add("Status", "Статус");

        }
        private void ReadSingleRowsAccess(DataGridView dgu, IDataRecord record)
        {

            // Проверка наличия нулевых значений и добавление их в DataGridView
            dgu.Rows.Add(
                record.IsDBNull(0) ? (int?)null : record.GetInt32(0),
                record.IsDBNull(1) ? null : record.GetString(1),
                record.IsDBNull(2) ? null : record.GetString(2),
                record.IsDBNull(3) ? null : record.GetString(3),
                record.IsDBNull(4) ? (int?)null : record.GetInt32(4),
                record.IsDBNull(6) ? null : record.GetString(6)
                );

        }
        private void RefreshDataGridAccess(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from AdminAccess";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsAccess(dgv, reader);
            }
            reader.Close();
        }

        private void CreateColumnsServices()
        {
            dataGridViewServices.Columns.Add("ServiceID", "ID");
            dataGridViewServices.Columns.Add("Category", "Категория");
            dataGridViewServices.Columns.Add("Title", "Название");
            dataGridViewServices.Columns.Add("Price", "Цена");
        }
        private void ReadSingleRowsServices(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3));
        }
        private void RefreshDataGridServices(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from AdminServices";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsServices(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridViewPhones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
