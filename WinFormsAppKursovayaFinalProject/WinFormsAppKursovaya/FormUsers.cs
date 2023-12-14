using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class FormUsers : Form
    {
        DataBase dataBase = new DataBase();

        int selectedRow;
        public int selectedIdImage;
        public FormUsers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CreateColumnsPhones()
        {
            dataGridViewPhones.Columns.Add("PhoneID", "ID");
            dataGridViewPhones.Columns.Add("Brand", "Производитель");
            dataGridViewPhones.Columns.Add("Model", "Модель");
            dataGridViewPhones.Columns.Add("Specifications", "Характеристики");
            dataGridViewPhones.Columns.Add("SellingPrice", "Цена");
        }
        private void ReadSingleRowsPhones(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4));
        }
        private void SearchPhones(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchstring = $"select * from Phones where concat (PhoneID, Brand, Model, Specifications, SellingPrice) like '%" + textBoxSearchPhones.Text + "%'";
            SqlCommand cmd = new SqlCommand(searchstring, dataBase.GetConnection());
            dataBase.OpenConnection();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRowsPhones(dgw, read);
            }

            read.Close();
        }
        private void RefreshDataGridPhones(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from Phones";

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
        }
        private void ReadSingleRowsAccess(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4));
        }
        private void RefreshDataGridAccess(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from Accessories";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsAccess(dgv, reader);
            }
            reader.Close();
        }
        private void SearchAccess(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchstring = $"select * from Accessories where concat (AccessID, Kategories, Color, Title, Price) like '%" + textBoxSearchAccess.Text + "%'";
            SqlCommand cmd = new SqlCommand(searchstring, dataBase.GetConnection());
            dataBase.OpenConnection();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRowsAccess(dgw, read);
            }

            read.Close();
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

            string querystring = $"select * from Services";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsServices(dgv, reader);
            }
            reader.Close();
        }
        private void SearchServices(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchstring = $"select * from Services where concat (ServiceID, Category, Title, Price) like '%" + textBoxSearchServices.Text + "%'";
            SqlCommand cmd = new SqlCommand(searchstring, dataBase.GetConnection());
            dataBase.OpenConnection();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRowsServices(dgw, read);
            }
            read.Close();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewPhones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            CreateColumnsPhones();
            RefreshDataGridPhones(dataGridViewPhones);

            CreateColumnsAccess();
            RefreshDataGridAccess(dataGridViewAccess);

            CreateColumnsServices();
            RefreshDataGridServices(dataGridViewServices);
        }

        private void dataGridViewPhones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridViewPhones.Rows[selectedRow];
                textBoxPhoneId.Text = row.Cells[0].Value.ToString();
                textBoxPhoneBrand.Text = row.Cells[1].Value.ToString();
                textBoxPhoneModel.Text = row.Cells[2].Value.ToString();
                textBoxPhoneSpecifications.Text = row.Cells[3].Value.ToString();
                textBoxPhoneSelling.Text = row.Cells[4].Value.ToString();

                selectedIdImage = int.Parse(row.Cells[0].Value.ToString());
            }
            string searchStringImage = $"SELECT Photo FROM Phones WHERE PhoneID = '{selectedIdImage}'";

            SqlCommand command1 = new SqlCommand(searchStringImage, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader readImage = command1.ExecuteReader();

            if (readImage.Read())
            {
                byte[] byteArray = (byte[])readImage["Photo"];

                //byte[] byteArray = // Ваш ByteArray;

                using (var ms = new MemoryStream(byteArray))
                {
                    Bitmap image = new Bitmap(ms);
                    PicturePhones.Image = image;
                }
            }
            readImage.Close();
            dataBase.CloseConnection();
        }

        private void textBoxSearchPhones_TextChanged(object sender, EventArgs e)
        {
            SearchPhones(dataGridViewPhones);
        }

        private void dataGridViewAccess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridViewAccess.Rows[selectedRow];
                textBoxAccessid.Text = row.Cells[0].Value.ToString();
                textBoxAccessKategories.Text = row.Cells[1].Value.ToString();
                textBoxAccessColor.Text = row.Cells[2].Value.ToString();
                textBoxAccessTitle.Text = row.Cells[3].Value.ToString();
                textBoxAccessPrice.Text = row.Cells[4].Value.ToString();

                selectedIdImage = int.Parse(row.Cells[0].Value.ToString());
            }
            string searchStringImage = $"SELECT Photo FROM Accessories WHERE AccessID = '{selectedIdImage}'";

            SqlCommand command1 = new SqlCommand(searchStringImage, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader readImage = command1.ExecuteReader();

            if (readImage.Read())
            {
                byte[] byteArray = (byte[])readImage["Photo"];

                //byte[] byteArray = // Ваш ByteArray;

                using (var ms = new MemoryStream(byteArray))
                {
                    Bitmap image = new Bitmap(ms);
                    PictureAccessories.Image = image;
                }
            }
            readImage.Close();
            dataBase.CloseConnection();
        }

        private void textBoxSearchAccess_TextChanged(object sender, EventArgs e)
        {
            SearchAccess(dataGridViewAccess);
        }

        private void dataGridViewServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridViewServices.Rows[selectedRow];
                textBoxServiceId.Text = row.Cells[0].Value.ToString();
                textBoxServiceCategory.Text = row.Cells[1].Value.ToString();
                textBoxServiceTitle.Text = row.Cells[2].Value.ToString();
                textBoxServicePrice.Text = row.Cells[3].Value.ToString();
            }
        }

        private void textBoxSearchServices_TextChanged(object sender, EventArgs e)
        {
            SearchServices(dataGridViewServices);
        }

        private void BuyThisPhone_Click(object sender, EventArgs e)
        {
            try 
            { 
            int phoneID = Convert.ToInt32(textBoxPhoneId.Text);
            int userID = Loginform.idUser;
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("FillTransaction", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PhoneID", SqlDbType.Int).Value = phoneID;
            command.Parameters.Add("@KlientID", SqlDbType.Int).Value = userID;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка! Выберите устройство!");
            }
}

        private void BuyThisAccess_Click(object sender, EventArgs e)
        {
            try
            {
            int AccessID = Convert.ToInt32(textBoxAccessid.Text);
            int userID = Loginform.idUser;
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("FillTransaction", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AccessID", SqlDbType.Int).Value = AccessID;
            command.Parameters.Add("@KlientID", SqlDbType.Int).Value = userID;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();
        }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка! Выберите аксессуар!");
            }
}

        private void BuyThisService_Click(object sender, EventArgs e)
        {
            try
            {
                int ServiceID = Convert.ToInt32(textBoxServiceId.Text);
            int userID = Loginform.idUser;
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("FillTransaction", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = ServiceID;
            command.Parameters.Add("@KlientID", SqlDbType.Int).Value = userID;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка! Выберите услугу!");
            }
        }
    }
}
