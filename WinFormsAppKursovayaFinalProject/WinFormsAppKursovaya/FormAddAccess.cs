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
    public partial class FormAddAccess : Form
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        DataBase dataBase = new DataBase();
        public FormAddAccess()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            // создаём соединение с db
            dataBase.OpenConnection();

            InsertImage();

            var Kat = comboBoxKategories.Text;
            var Col = comboBoxColor.Text;
            var Tit = textBoxTitle.Text;
            int Pri;
            if (int.TryParse(textBoxPrice.Text, out Pri))
            {
                // создаём соединение с базой данных
                dataBase.OpenConnection();

                // создаём запрос на отправку данных в БД
                var querystring = "INSERT INTO Accessories (Kategories, Color, Title, Price, Photo) " +
                "VALUES (@Kategories, @Color, @Title, @Price, @Photo)";

                // создание экземпляра команды и передача строки запроса и соединения
                var command = new SqlCommand(querystring, dataBase.GetConnection());

                // добавление параметров в команду
                command.Parameters.AddWithValue("@Kategories", Kat);
                command.Parameters.AddWithValue("@Color", Col);
                command.Parameters.AddWithValue("@Title", Tit);
                command.Parameters.AddWithValue("@Price", Pri);

                // добавление параметра изображения
                var photo = new Bitmap(pictureBoxAccessuares.Image);
                using (var memoryStream = new MemoryStream())
                {
                    photo.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Position = 0;
                    command.Parameters.AddWithValue("@Photo", memoryStream.ToArray());
                }

                // выполнение команды на добавление записи в БД
                command.ExecuteNonQuery();

                // закрытие соединения с базой данных
                dataBase.CloseConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Зарплата и id-автобуса должны иметь числовой формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void InsertImage()
        {
            openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxAccessuares.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        //private void Save_Click(object sender, EventArgs e)
        //{
        //        dataBase.OpenConnection();

        //    var Kat =comboBoxKategories.Text;
        //    var Col = comboBoxColor.Text;
        //    var Tit = textBoxTitle.Text;
        //    int Pri;
        //    if (int.TryParse(textBoxPrice.Text, out Pri))
        //    {
        //        var addQuery = $"insert into Accessories (Kategories, Color, Title, Price) values ('{Kat}', '{Col}','{Tit}', '{Pri}')";
        //        var command = new SqlCommand(addQuery, dataBase.GetConnection());
        //        command.ExecuteNonQuery();

        //        MessageBox.Show("Запись создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Запись не создана!", "Провал!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    dataBase.CloseConnection();
        //}
    }
}
