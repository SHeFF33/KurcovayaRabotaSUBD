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
    public partial class AddServices : Form
    {
        DataBase dataBase = new DataBase();
        public AddServices()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();

            var category = comboBoxCategories.Text;
            var title = textBoxTitle.Text;
            int price;
            if (int.TryParse(textBoxPrice.Text, out price))
            {
                var addQuery = $"insert into Services (Category, Title, Price) values ('{category}', '{title}','{price}')";
                var command = new SqlCommand(addQuery, dataBase.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Запись не создана!", "Провал!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataBase.CloseConnection();
        }

    }
}
