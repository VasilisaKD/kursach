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
using System.Text.RegularExpressions;

namespace kursach.forms
{
    public partial class order : Form
    {
        string connString;
        NpgsqlConnection nc;
        string sql;
        NpgsqlDataAdapter adapter;
        DataSet dataSet1 = new DataSet();
        double sum;
        public order()
        {
            InitializeComponent();
            //запись в бд
            connString = "Host=localhost;Username=postgres;Password=root;Database=kursach";
            nc = new NpgsqlConnection(connString);
            try
            {
                //Открываем соединение.
                nc.Open();
            sql = @"select surname||' '||name||' '||middlename as fio from clients";
                adapter = new NpgsqlDataAdapter(sql, nc);
                adapter.Fill(dataSet1);
            comboBox2.DataSource = dataSet1.Tables[0];
            comboBox2.DisplayMember = "fio";
            comboBox2.ValueMember = "fio";

                dataSet1 = new DataSet();
                sql = @"select id, surname||' '||name||' '||middlename as fio from staff where id_post = 2";
                adapter = new NpgsqlDataAdapter(sql, nc);
                adapter.Fill(dataSet1);
                dataGridView1.DataSource = dataSet1.Tables[0];
                comboBox3.DataSource = dataSet1.Tables[0];
                comboBox3.DisplayMember = "fio";
                comboBox3.ValueMember = "fio";

                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox1.SelectedIndex = 0;
                nc.Close();
            }
            catch (Exception ex)
            {
                //Код обработки ошибок
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.Date.ToString();
            int cashier = Convert.ToInt32(label10.Text);
            int client = comboBox2.SelectedIndex + 1;
            int pizza = Convert.ToInt32(label5.Text);
            int qt = Convert.ToInt32(numericUpDown1.Value);
            string adres = textBox4.Text;
            int payment = comboBox1.SelectedIndex + 1;
            string status = "new";

            if (string.IsNullOrEmpty(date) & string.IsNullOrEmpty(adres) & qt != 0 & sum != 0)
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                try
                {
                    connString = "Host=localhost;Username=postgres;Password=root;Database=kursach";
                    nc = new NpgsqlConnection(connString);
                    nc.Open();
                    sql = @"insert into orders(date, id_cashier, id_client, id_pizza, qt, adress, id_payment, status, sum) " +
                        $"values('{date}', {cashier}, {client}, {pizza}, {qt}, '{adres}', {payment}, '{status}', '{sum}')";
                    NpgsqlCommand comm = new NpgsqlCommand(sql, nc);
                    nc.Close(); //Закрываем соединение.

                    MessageBox.Show("Успешно!");
                }
                catch
                {
                    MessageBox.Show("Ошибка записи данных");
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double sum = Convert.ToDouble(label9.Text);
            int qt = Convert.ToInt32(numericUpDown1.Value);
            sum = Math.Round(sum * qt, 2);
            textBox1.Text = sum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu cashier = new menu();
            cashier.Show();
            this.Hide();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1[1,i].Value.ToString() == comboBox3.Text)
                {
                    label10.Text = dataGridView1[0, i].Value.ToString();
                }
            }
        }
    }
}
