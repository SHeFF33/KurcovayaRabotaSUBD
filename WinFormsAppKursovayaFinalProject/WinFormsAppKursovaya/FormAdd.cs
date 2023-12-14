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
using System.Drawing.Imaging;

namespace WinFormsAppKursovaya
{
    public partial class FormAdd : Form
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        DataBase dataBase = new DataBase();
        public FormAdd()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            // создаём соединение с db
            dataBase.OpenConnection();

            InsertImage();

            var brand = textBoxBrand.Text;
            var model = textBoxModel.Text;
            var specifications = textBoxSpecific.Text;
            int purchase;
            int selling;
            if (int.TryParse(textBoxPurchase.Text, out purchase) && int.TryParse(textBoxSalling.Text, out selling))
            {
                // создаём запрос на отправку данных в БД
                var querystring = "INSERT INTO Phones (Brand, Model, Specifications, PurchasePrice, SellingPrice, Photo) " +
                "VALUES (@Brand, @Model, @Specifications, @PurchasePrice, @SellingPrice, @Photo)";

                // создание экземпляра команды и передача строки запроса и соединения
                var command = new SqlCommand(querystring, dataBase.GetConnection());

                // добавление параметров в команду
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@Specifications", specifications);
                command.Parameters.AddWithValue("@PurchasePrice", purchase);
                command.Parameters.AddWithValue("@SellingPrice", selling);

                // добавление параметра изображения
                var photo = new Bitmap(pictureBoxPhones.Image);
                using (var memoryStream = new MemoryStream())
                {
                    photo.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Position = 0;
                    command.Parameters.AddWithValue("@Photo", memoryStream.ToArray());
                }

                // выполнение команды на добавление записи в БД
                command.ExecuteNonQuery();

                // закрытие соединения с базой данных

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Зарплата и id-автобуса должны иметь числовой формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataBase.CloseConnection();
        }

        private void InsertImage()
        {
            openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPhones.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
