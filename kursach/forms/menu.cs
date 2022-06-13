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
    public partial class menu : Form
    {
        string connString;
        DataSet dataSet1;
        NpgsqlConnection nc;
        public menu()
        {
            InitializeComponent();
            //запись в бд
            connString = "Host=localhost;Username=postgres;Password=root;Database=kursach";
            nc = new NpgsqlConnection(connString);
            try
            {
                //Открываем соединение.
                nc.Open();
            }
            catch (Exception ex)
            {
                //Код обработки ошибок
                MessageBox.Show("Ошибка подключения к БД");
            }

            try
            {
                string sql = @"select menu.name, menu.description, menu.price, pizza_length.name as length from menu inner join pizza_length
on menu.id_length = pizza_length.id";
                dataSet1 = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, nc);
                adapter.Fill(dataSet1);
                nc.Close(); //Закрываем соединение.
                dataGridView1.DataSource = dataSet1.Tables[0];
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    comboBox1.Items.Add(dataGridView1[0, i].Value.ToString());
                }
                comboBox1.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Ошибка записи данных");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            order order = new order();
            dataSet1.Reset();
            order.textBox3.Text = comboBox1.Text;
            order.label5.Text = (comboBox1.SelectedIndex+1).ToString();
            string sql = @"select menu.price from menu where name = '"+comboBox1.Text+"'";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, nc);
            adapter.Fill(dataSet1);
            nc.Close(); //Закрываем соединение.
            dataGridView1.DataSource = dataSet1.Tables[0];
            order.label9.Text = (dataGridView1[0,0].Value).ToString();
            order.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cashier cashier = new cashier();
            cashier.Show();
            this.Hide();
        }
    }
}
