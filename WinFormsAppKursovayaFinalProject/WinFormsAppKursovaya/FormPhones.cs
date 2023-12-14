using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsAppKursovaya
{
    public enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class FormPhones : Form
    {
        DataBase dataBase = new DataBase();
        Loginform frm = new Loginform();
        int selectedRow;
        public int selectedIdImage;
        public FormPhones()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CreateColumnsPhones()
        {
            int admin = Loginform.idAdmin;
            int user = Loginform.idUser;    


            dataGridViewPhones.Columns.Add("PhoneID", "ID телефона");
            dataGridViewPhones.Columns.Add("Brand", "Производитель");
            dataGridViewPhones.Columns.Add("Model", "Модель");
            dataGridViewPhones.Columns.Add("Specifications", "Характеристики");
            dataGridViewPhones.Columns.Add("PurchasePrice", "Цена приема");
            dataGridViewPhones.Columns.Add("SellingPrice", "Цена продажи");
            dataGridViewPhones.Columns.Add("isNew", string.Empty);
        }

        private void ChangeRowPhones()
        {
            var selectedRowIndex = dataGridViewPhones.CurrentCell.RowIndex;
            var id = textBoxPhoneId.Text;
            var brand = textBoxPhoneBrand.Text;
            var model = textBoxPhoneModel.Text;
            var specific = textBoxPhoneSpecifications.Text;
            int purchase;
            int selling;
            if (dataGridViewPhones.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBoxPhonePurchase.Text, out purchase))
                {
                    if (int.TryParse(textBoxPhoneSelling.Text, out selling))
                    {
                        dataGridViewPhones.Rows[selectedRowIndex].SetValues(id, brand, model, specific, purchase, selling);
                        dataGridViewPhones.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
                    }
                    else
                    {
                        MessageBox.Show("Выберите строку, которую хотите изменить!");
                    }

                }
            }
        }
        private void UpdatePhones()
        {
            for (int index = 0; index < dataGridViewPhones.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridViewPhones.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    dataBase.OpenConnection();
                    var id = Convert.ToInt32(dataGridViewPhones.Rows[index].Cells[0].Value);
                    var command1 = new SqlCommand($"delete from Phones where PhoneID = {id}", dataBase.GetConnection());
                    command1.ExecuteNonQuery();
                    dataBase.CloseConnection();
                }
                if (rowState == RowState.Modified)
                {
                    dataBase.OpenConnection();
                    var id = dataGridViewPhones.Rows[index].Cells[0].Value.ToString();
                    var brand = dataGridViewPhones.Rows[index].Cells[1].Value.ToString();
                    var model = dataGridViewPhones.Rows[index].Cells[2].Value.ToString();
                    var specific = dataGridViewPhones.Rows[index].Cells[3].Value.ToString();
                    var purchase = dataGridViewPhones.Rows[index].Cells[4].Value.ToString();
                    var selling = dataGridViewPhones.Rows[index].Cells[5].Value.ToString();

                    var changequery = $"update Phones set Brand = '{brand}', Model = '{model}', Specifications = '{specific}', PurchasePrice = '{purchase}', SellingPrice = '{selling}' where PhoneID = '{id}'";
                    var command = new SqlCommand(changequery, dataBase.GetConnection());
                    command.ExecuteNonQuery();
                    dataBase.CloseConnection();
                }
            }
        }
        private void DeleteRowPhones()
        {
            if (dataGridViewPhones.CurrentCell != null)
            {
                int index = dataGridViewPhones.CurrentCell.RowIndex;
                dataGridViewPhones.Rows[index].Visible = false;
                if (dataGridViewPhones.Rows[index].Cells[0].Value.ToString() == string.Empty)
                {
                    dataGridViewPhones.Rows[index].Cells[6].Value = RowState.Deleted;
                    return;
                }
                dataGridViewPhones.Rows[index].Cells[6].Value = RowState.Deleted;
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления!");
            }
        }
        private void SearchPhones(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchstring = $"select * from Phones where concat (PhoneID, Brand, Model, Specifications, PurchasePrice, SellingPrice) like '%" + textBoxSearchPhones.Text + "%'";
            SqlCommand cmd = new SqlCommand(searchstring, dataBase.GetConnection());
            dataBase.OpenConnection();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRowsPhones(dgw, read);
            }

            read.Close();
        }

        private void ReadSingleRowsPhones(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),  record.GetString(3), record.GetInt32(4), record.GetInt32(5), RowState.ModifiedNew);
        }
        private void RefreshDataGridPhones(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from Phones";

            SqlCommand command= new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsPhones(dgv, reader);    
            }
            reader.Close();
        }

        private void FormPhones_Load(object sender, EventArgs e)
        {
            CreateColumnsPhones();
            RefreshDataGridPhones(dataGridViewPhones);

            CreateColumnsAccess();
            RefreshDataGridAccess(dataGridViewAccess);

            CreateColumnsServices();
            RefreshDataGridServices(dataGridViewServices);

            CreateColumnsTransactions();
            RefreshDataGridTransactions(dataGridViewTransactions);

            CreateColumnsReports();
            RefreshDataGridReports(dataGridViewReports);
            UpdateReport();

            CreateColumnsReportsPhone();
            DeletePhoneReport();

            CreateColumnsReportsService();
            DeleteServiceReport();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSearchPhones_TextChanged_1(object sender, EventArgs e)
        {
            SearchPhones(dataGridViewPhones);
        }
        private void SavePhones_Click(object sender, EventArgs e)
        {
            UpdatePhones();
        }
        private void CreatePhones_Click(object sender, EventArgs e)
        {
            FormAdd frmadd = new FormAdd();
            frmadd.Show();
        }

        private void ChangePhones_Click(object sender, EventArgs e)
        {
            ChangeRowPhones();
        }

        private void DeletePhones_Click(object sender, EventArgs e)
        {
            DeleteRowPhones();
        }

        private void RefreshPhones_Click(object sender, EventArgs e)
        {
            RefreshDataGridPhones(dataGridViewPhones);
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
                textBoxPhonePurchase.Text = row.Cells[4].Value.ToString();
                textBoxPhoneSelling.Text = row.Cells[5].Value.ToString();

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

        private void dataGridViewPhones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        // Аксессуары
        //-------------------------------------------------------------------------------------------

        private void CreateColumnsAccess()
        {
            dataGridViewAccess.Columns.Add("AccesID", "ID");
            dataGridViewAccess.Columns.Add("Kategories", "Категория");
            dataGridViewAccess.Columns.Add("Color", "Цвет");
            dataGridViewAccess.Columns.Add("Title", "Название");
            dataGridViewAccess.Columns.Add("Price", "Цена");
            dataGridViewAccess.Columns.Add("isNew", string.Empty);
        }
        private void ChangeRowAccess()
        {
            var selectedRowIndex = dataGridViewAccess.CurrentCell.RowIndex;
            var id = textBoxAccessid.Text;
            var kategories = textBoxAccessKategories.Text;
            var color = textBoxAccessColor.Text;
            var title = textBoxAccessTitle.Text;
            int price;
            if (dataGridViewAccess.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBoxAccessPrice.Text, out price))
                {
                    dataGridViewAccess.Rows[selectedRowIndex].SetValues(id, kategories, color, title, price);
                    dataGridViewAccess.Rows[selectedRowIndex].Cells[5].Value = RowState.Modified;

                }
                else
                {
                    MessageBox.Show("Выберите строку, которую хотите изменить!");
                }
            }
        }
        private void UpdateAccesss()
        {
          

            for (int index = 0; index < dataGridViewAccess.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridViewAccess.Rows[index].Cells[5].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    dataBase.OpenConnection();
                    var id = Convert.ToInt32(dataGridViewAccess.Rows[index].Cells[0].Value);
                    var deletequery = $"delete from Accessories where AccessID = {id}";

                    var command = new SqlCommand(deletequery, dataBase.GetConnection());
                    command.ExecuteNonQuery();
                    dataBase.CloseConnection();
                }
                if (rowState == RowState.Modified)
                {
                    dataBase.OpenConnection();
                    var id = dataGridViewAccess.Rows[index].Cells[0].Value.ToString();
                    var kategories = dataGridViewAccess.Rows[index].Cells[1].Value.ToString();
                    var color = dataGridViewAccess.Rows[index].Cells[2].Value.ToString();
                    var title = dataGridViewAccess.Rows[index].Cells[3].Value.ToString();
                    var price = dataGridViewAccess.Rows[index].Cells[4].Value.ToString();

                    var changequery = $"update Accessories set Kategories = '{kategories}', Color = '{color}', Title = '{title}', Price = '{price}' where AccessID = '{id}'";
                    var command = new SqlCommand(changequery, dataBase.GetConnection());
                    command.ExecuteNonQuery();
                    dataBase.CloseConnection();
                }
            }
        }
        private void DeleteRowAccess()
        {
            if (dataGridViewAccess.CurrentCell != null)
            {
                int index = dataGridViewAccess.CurrentCell.RowIndex;
                dataGridViewAccess.Rows[index].Visible = false;
                if (dataGridViewAccess.Rows[index].Cells[0].Value.ToString() == string.Empty)
                {
                    dataGridViewAccess.Rows[index].Cells[5].Value = RowState.Deleted;
                    return;
                }
                dataGridViewAccess.Rows[index].Cells[5].Value = RowState.Deleted;
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления!");
            }
        }
        private void ReadSingleRowsAccess(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), RowState.ModifiedNew);
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

        private void dataGridViewAccess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void textBoxSearchAccess_TextChanged(object sender, EventArgs e)
        {
            SearchAccess(dataGridViewAccess);
        }

        private void CreateAccess_Click(object sender, EventArgs e)
        {
            FormAddAccess faa = new FormAddAccess();
            faa.Show();
        }

        private void DeleteAccess_Click(object sender, EventArgs e)
        {
            DeleteRowAccess();
        }

        private void UpdateAccess_Click(object sender, EventArgs e)
        {
            ChangeRowAccess();
        }

        private void SaveAccess_Click(object sender, EventArgs e)
        {
            UpdateAccesss();
        }

        private void RefreshAccess_Click(object sender, EventArgs e)
        {
            RefreshDataGridAccess(dataGridViewAccess);
        }

        //Услуги
        //-------------------------------------------------------------------------------------
        private void CreateColumnsServices()
        {
            dataGridViewServices.Columns.Add("ServiceID", "ID");
            dataGridViewServices.Columns.Add("Category", "Категория");
            dataGridViewServices.Columns.Add("Title", "Название");
            dataGridViewServices.Columns.Add("Price", "Цена");
            dataGridViewServices.Columns.Add("isNew", string.Empty);
        }
        private void ReadSingleRowsServices(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), RowState.ModifiedNew);
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
        private void DeleteRowServices()
        {
            if (dataGridViewServices.CurrentCell != null)
            {
                int index = dataGridViewServices.CurrentCell.RowIndex;
                dataGridViewServices.Rows[index].Visible = false;
                if (dataGridViewServices.Rows[index].Cells[0].Value.ToString() == string.Empty)
                {
                    dataGridViewServices.Rows[index].Cells[4].Value = RowState.Deleted;
                    return;
                }
                dataGridViewServices.Rows[index].Cells[4].Value = RowState.Deleted;
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления!");
            }
        }
        private void UpdateServices()
        {
            dataBase.GetConnection();

            for (int index = 0; index < dataGridViewServices.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridViewServices.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridViewServices.Rows[index].Cells[0].Value);
                    var deletequery = $"delete from Services where ServiceID = {id}";

                    var command = new SqlCommand(deletequery, dataBase.GetConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridViewServices.Rows[index].Cells[0].Value.ToString();
                    var category = dataGridViewServices.Rows[index].Cells[1].Value.ToString();
                    var title = dataGridViewServices.Rows[index].Cells[2].Value.ToString();
                    var price = dataGridViewServices.Rows[index].Cells[3].Value.ToString();

                    var changequery = $"update Services set Category = '{category}', Title = '{title}', Price = '{price}' where ServiceID = '{id}'";
                    var command = new SqlCommand(changequery, dataBase.GetConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.CloseConnection();
        }
        private void ChangeRowServices()
        {
            var selectedRowIndex = dataGridViewServices.CurrentCell.RowIndex;
            var id = textBoxServiceId.Text;
            var category = textBoxServiceCategory.Text;
            var title = textBoxServiceTitle.Text;
            int price;
            if (dataGridViewServices.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBoxServicePrice.Text, out price))
                {
                    dataGridViewServices.Rows[selectedRowIndex].SetValues(id, category, title, price);
                    dataGridViewServices.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
                }
                else
                {
                    MessageBox.Show("Выберите строку, которую хотите изменить!");
                }
            }
        }

        private void dataGridViewServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void RefreshServices_Click(object sender, EventArgs e)
        {
            RefreshDataGridServices(dataGridViewServices);
        }

        private void CreateServices_Click(object sender, EventArgs e)
        {
            AddServices addserv = new AddServices();
            addserv.Show();
        }

        private void DeleteServices_Click(object sender, EventArgs e)
        {
            DeleteRowServices();
        }

        private void ChangeServices_Click(object sender, EventArgs e)
        {
            ChangeRowServices();
        }

        private void SaveServices_Click(object sender, EventArgs e)
        {
            UpdateServices();
        }

        private void textBoxSearchServices_TextChanged(object sender, EventArgs e)
        {
            SearchServices(dataGridViewServices);
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

        private void управлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Администратор adm = new Администратор();
            adm.Show();
        }


        // Транзакции
        //------------------------------------------------------------------------------------------
        private void CreateColumnsTransactions()
        {
            dataGridViewTransactions.Columns.Add("TransactionID", "Id транзакции");
            dataGridViewTransactions.Columns.Add("PhoneID", "Id телефона");
            dataGridViewTransactions.Columns.Add("AccessID", "Id аксессуара");
            dataGridViewTransactions.Columns.Add("ServiceID", "Id услуги");
            dataGridViewTransactions.Columns.Add("KlientID", "Id клиента");
            dataGridViewTransactions.Columns.Add("Profit", "Сумма сделки");
            dataGridViewTransactions.Columns.Add("TransactionDate", "Дата транзакции");
            dataGridViewTransactions.Columns.Add("Status","Статус транзакции");
        }
        private void ReadSingleRowsTransactions(DataGridView dgw, IDataRecord record)
        {
            int? value1 = record.IsDBNull(0) ? (int?)null : record.GetInt32(0);
            int? value2 = record.IsDBNull(1) ? (int?)null : record.GetInt32(1);
            int? value3 = record.IsDBNull(2) ? (int?)null : record.GetInt32(2);
            int? value4 = record.IsDBNull(3) ? (int?)null : record.GetInt32(3);
            int? value5 = record.IsDBNull(4) ? (int?)null : record.GetInt32(4);
            int? value6 = record.IsDBNull(5) ? (int?)null : record.GetInt32(5);
            DateTime? value7 = record.IsDBNull(6) ? (DateTime?)null : record.GetDateTime(6);
            string? value8 = record.IsDBNull(7) ? (string?)null : record.GetString(7);
            dgw.Rows.Add(value1, value2, value3, value4, value5, value6, value7, value8);
        }
        private void SearchTransaction(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchstring = $"select * from Transactions where concat (TransactionID, PhoneID, AccessID, ServiceID, KlientID, Profit, TransactionDate, Status) like '%" + textBoxSearchTransactions.Text + "%'";
            SqlCommand cmd = new SqlCommand(searchstring, dataBase.GetConnection());
            dataBase.OpenConnection();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRowsTransactions(dgw, read);
            }

            read.Close();
        }
        private void RefreshDataGridTransactions(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from Transactions";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsTransactions(dgv, reader);
            }
            reader.Close();
        }
        private void dataGridViewTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridViewTransactions.Rows[selectedRow];
                textBoxTransactionID.Text = row.Cells[0].Value.ToString();
                textBoxPhnID.Text = (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString())) ? row.Cells[1].Value.ToString() : "Знач. Null";
                textBoxAccID.Text = (row.Cells[2].Value != null && !string.IsNullOrEmpty(row.Cells[2].Value.ToString())) ? row.Cells[2].Value.ToString() : "Знач. Null";
                textBoxServID.Text = (row.Cells[3].Value != null && !string.IsNullOrEmpty(row.Cells[3].Value.ToString())) ? row.Cells[3].Value.ToString() : "Знач. Null";
                textBoxClientId.Text = row.Cells[4].Value.ToString();
                textBoxPurch.Text = row.Cells[5].Value.ToString();
                textBoxTransactData.Text = row.Cells[6].Value.ToString();
                textBoxStatus.Text = (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString())) ? row.Cells[7].Value.ToString() : "Знач. Null";
            }
        }
        private void textBoxSearchTransactions_TextChanged(object sender, EventArgs e)
        {
            SearchTransaction(dataGridViewTransactions);
        }

        private void RejectTransaction_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();
            if (sender==RejectTransaction)
            {
                try
                {
                    int TransID1 = Convert.ToInt32(textBoxTransactionID.Text);
                    string Reject1 = "Отклонено";
                    SqlCommand command2 = new SqlCommand($"UPDATE Transactions SET Status = '{Reject1}' WHERE TransactionID = '{TransID1}';", dataBase.GetConnection());
                    command2.ExecuteNonQuery();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Выберите транзакцию!");
                }
            }
            dataBase.CloseConnection();

            RefreshDataGridTransactions(dataGridViewTransactions);

        }
        // Отчеты
        //---------------------------------------------------------

        private void CreateColumnsReports()
        {
            dataGridViewReports.Columns.Add("ReportID ", "Номер отчета");
            dataGridViewReports.Columns.Add("TransactionID ", "Номер проведенной транзакции");
            dataGridViewReports.Columns.Add("ClientID ", "ID клиента");
            dataGridViewReports.Columns.Add("AdminID ", "ID сотрудника");
            dataGridViewReports.Columns.Add("Profit", "Сумма сделки");
            dataGridViewReports.Columns.Add("TransactionDate", "Дата транзакции");
            dataGridViewReports.Columns.Add("ReportDate ", "Дата создания отчета");

        }
        private void ReadSingleRowsReports(DataGridView dgw, IDataRecord record)
        {
            int? value1 = record.IsDBNull(0) ? (int?)null : record.GetInt32(0);
            int? value2 = record.IsDBNull(1) ? (int?)null : record.GetInt32(1);
            int? value3 = record.IsDBNull(2) ? (int?)null : record.GetInt32(2);
            int? value4 = record.IsDBNull(3) ? (int?)null : record.GetInt32(3);
            int? value5 = record.IsDBNull(4) ? (int?)null : record.GetInt32(4);
            DateTime? value6 = record.IsDBNull(5) ? (DateTime?)null : record.GetDateTime(5);
            DateTime? value7 = record.IsDBNull(6) ? (DateTime?)null : record.GetDateTime(6);
            dgw.Rows.Add(value1, value2, value3, value4, value5, value6, value7);
        }
        private void RefreshDataGridReports(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from Reports";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsReports(dgv, reader);
            }
            reader.Close();
        }
        private void UpdateReport()
        {
            dataBase.OpenConnection();
            SqlCommand command1 = new SqlCommand("UpdateProfit", dataBase.GetConnection());
            command1.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.Read())
            {
                textBoxProfitforReports.Text = reader["Profit"].ToString();
            }
            reader.Close();
            dataBase.CloseConnection();
        }
        private void ConfirmTransaction_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();
            if (sender==ConfirmTransaction)
            {
                try
                {
                    int TransID1 = Convert.ToInt32(textBoxTransactionID.Text);
                    string Conf1 = "Подтверждено";
                    SqlCommand command3 = new SqlCommand($"UPDATE Transactions SET Status = '{Conf1}' WHERE TransactionID = '{TransID1}';", dataBase.GetConnection());
                    command3.ExecuteNonQuery();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Выберите транзакцию!");
                }
            }
            dataBase.CloseConnection();


            int transactID = Convert.ToInt32(textBoxTransactionID.Text);
            int adminID = Loginform.idAdmin;
            dataBase.OpenConnection();
            SqlCommand command = new SqlCommand("InsertReport", dataBase.GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@TransactionID", SqlDbType.Int).Value = transactID;
            command.Parameters.Add("@AdminID", SqlDbType.Int).Value = adminID;
            command.ExecuteNonQuery();
            dataBase.CloseConnection();

            dataBase.OpenConnection();
            SqlCommand command2 = new SqlCommand("DeleteTransactions", dataBase.GetConnection());
            command2.CommandType = CommandType.StoredProcedure;
            command2.ExecuteNonQuery();
            dataBase.CloseConnection();


            RefreshDataGridTransactions(dataGridViewTransactions);
            RefreshDataGridPhones(dataGridViewPhones);
            RefreshDataGridAccess(dataGridViewAccess);
            RefreshDataGridServices(dataGridViewServices);
            RefreshDataGridReports(dataGridViewReports);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Администратор adm = new Администратор();
            adm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Администратор adm = new Администратор();
            adm.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Администратор adm = new Администратор();
            adm.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Администратор adm = new Администратор();
            adm.Show();
        }
        private void CreateColumnsReportsPhone()
        {
            dataGridViewPhonesReport.Columns.Add("PhoneReportID", "ID");
            dataGridViewPhonesReport.Columns.Add("TransactionID", "ID транзакции");
            dataGridViewPhonesReport.Columns.Add("PhoneID", "ID телефона");
            dataGridViewPhonesReport.Columns.Add("Brand", "Производитель");
            dataGridViewPhonesReport.Columns.Add("Model", "Модель");
            dataGridViewPhonesReport.Columns.Add("PurchasePrice", "Стоимость покупки");
            dataGridViewPhonesReport.Columns.Add("SellingPrice", "Стоимость продажи");
            dataGridViewPhonesReport.Columns.Add("Profit", "Прибыль");
            dataGridViewPhonesReport.Columns.Add("KlientID", "Клиент");
            dataGridViewPhonesReport.Columns.Add("ReportDate", "Дата продажи");

        }
        private void ReadSingleRowsReportsPhone(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), record.GetString(4), record.GetInt32(5), record.GetInt32(6), record.GetInt32(7), record.GetInt32(8), record.GetDateTime(9));
        }
        private void RefreshDataGridReportsPhone(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from PhonesReport";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsReportsPhone(dgv, reader);
            }
            reader.Close();
        }
        private void DataPhones()
        {
            dataBase.OpenConnection();
            SqlCommand command2 = new SqlCommand("FillPhonesReport", dataBase.GetConnection());
            command2.CommandType = CommandType.StoredProcedure;
            command2.ExecuteNonQuery();
            dataBase.CloseConnection();
        }
        private void DeletePhoneReport()
        {
            dataBase.OpenConnection();
            SqlCommand command2 = new SqlCommand($"Delete from PhonesReport", dataBase.GetConnection());
            command2.ExecuteNonQuery();
            dataBase.CloseConnection();
        }

        private void buttonPhReport_Click(object sender, EventArgs e)
        {
            DataPhones();
            RefreshDataGridReportsPhone(dataGridViewPhonesReport);

            int rowCount = dataGridViewPhonesReport.RowCount;
            textBoxCountPhones.Text = rowCount.ToString();

            int columnIndex1 = 6;
            int sum1 = 0;

            foreach (DataGridViewRow row in dataGridViewPhonesReport.Rows)
            {
                sum1 += Convert.ToInt32(row.Cells[columnIndex1].Value);
            }

            textBoxSum.Text = sum1.ToString();
            int columnIndex2 = 7;
            int sum2 = 0;

            foreach (DataGridViewRow row in dataGridViewPhonesReport.Rows)
            {
                sum2 += Convert.ToInt32(row.Cells[columnIndex2].Value);
            }

            textBoxItogoProfit.Text = sum2.ToString();
        }
        private void CreateColumnsReportsService()
        {
            dataGridViewServicesReport.Columns.Add("ServiceReportID", "ID");
            dataGridViewServicesReport.Columns.Add("TransactionID", "ID транзакции");
            dataGridViewServicesReport.Columns.Add("PhoneID", "ID услуги");
            dataGridViewServicesReport.Columns.Add("Category", "Категория");
            dataGridViewServicesReport.Columns.Add("Title", "Название");
            dataGridViewServicesReport.Columns.Add("Price", "Стоимость");
            dataGridViewServicesReport.Columns.Add("KlientID", "Клиент");
            dataGridViewServicesReport.Columns.Add("ReportDate", "Дата предоставления услуги");

        }
        private void ReadSingleRowsReportsService(DataGridView dgu, IDataRecord record)
        {
            dgu.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), record.GetString(4), record.GetInt32(5), record.GetInt32(6), record.GetDateTime(7));
        }
        private void RefreshDataGridReportsService(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string querystring = $"select * from ServicesReport";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowsReportsService(dgv, reader);
            }
            reader.Close();
        }
        private void DataServices()
        {
            dataBase.OpenConnection();
            SqlCommand command2 = new SqlCommand("FillServicesReport", dataBase.GetConnection());
            command2.CommandType = CommandType.StoredProcedure;
            command2.ExecuteNonQuery();
            dataBase.CloseConnection();
        }
        private void DeleteServiceReport()
        {
            dataBase.OpenConnection();
            SqlCommand command2 = new SqlCommand($"Delete from ServicesReport", dataBase.GetConnection());
            command2.ExecuteNonQuery();
            dataBase.CloseConnection();
        }

        private void buttonSrvReport_Click(object sender, EventArgs e)
        {
            DataServices();
            RefreshDataGridReportsService(dataGridViewServicesReport);

            int rowCount = dataGridViewServicesReport.RowCount;
            textBoxCountServices.Text = rowCount.ToString();

            int columnIndex1 = 5;
            int sum1 = 0;

            foreach (DataGridViewRow row in dataGridViewServicesReport.Rows)
            {
                sum1 += Convert.ToInt32(row.Cells[columnIndex1].Value);
            }

            textBoxSumServices.Text = sum1.ToString();
        }
    }
}
