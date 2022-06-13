using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace kursach.forms
{
    public partial class allorders : Form
    {
        string connString;
        public allorders()
        {
            InitializeComponent();
            //запись в бд
            connString = "Host=localhost;Username=postgres;Password=root;Database=kursach";
            NpgsqlConnection nc = new NpgsqlConnection(connString);
            try
            {
                //Открываем соединение.
                nc.Open();
                DataSet dataSet1 = new DataSet();
                string sql = @"select * from orders where status = 'new'";
                NpgsqlCommand comm = new NpgsqlCommand(sql, nc);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
                adapter.Fill(dataSet1);
                dataGridView1.DataSource = dataSet1.Tables[0];
                nc.Close(); //Закрываем соединение.

            }
            catch (Exception ex)
            {
                //Код обработки ошибок
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
