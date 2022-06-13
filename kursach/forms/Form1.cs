using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Npgsql;

namespace kursach
{
    public partial class Form1 : Form
    {
        string connString;
        public Form1()
        {
            InitializeComponent();
            connString = "Host=localhost;Username=postgres;Password=root;Database=kursach";
            NpgsqlConnection nc = new NpgsqlConnection(connString);
            try
            {
                //Открываем соединение.
                nc.Open();
                dataSet1 = new DataSet();
                
            }
            catch (Exception ex)
            {
                //Код обработки ошибок
                MessageBox.Show("Ошибка подключения к БД");
            }
            try
            {
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("select * from post", nc);
                adapter.Fill(dataSet1);
                nc.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка чтения данных");
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            var selectedUser = dataSet1.Tables[0].Select($"login = '{login}' and password = '{password}'");
            int id = 0;
            foreach (var row in selectedUser)
            {
                id = Convert.ToInt32(row[0]);
            }
            switch (id)
            {
                case 1:
                    forms.director form = new forms.director();
                    form.Show();
                    this.Hide();
                    break;
                case 2:
                    forms.cashier form2 = new forms.cashier();
                    form2.Show();
                    this.Hide();
                    break;
                case 3:
                    forms.allorders form3 = new forms.allorders();
                    form3.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Неправильный логин или пароль!");
                    break;
            }
        }
    }
}
