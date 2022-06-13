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
    public partial class addclient : Form
    {
        string connString;

        public addclient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string surname = textBox1.Text;
            string name = textBox2.Text;
            string middlename = textBox3.Text;   
            string phone = maskedTextBox2.Text;

            if (string.IsNullOrEmpty(surname) & string.IsNullOrEmpty(name) & string.IsNullOrEmpty(middlename) & string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                //запись в бд
                connString = "Host=localhost;Username=postgres;Password=root;Database=kursach";
                NpgsqlConnection nc = new NpgsqlConnection(connString);
                try
                {
                    //Открываем соединение.
                    nc.Open();
                    DataSet dataSet1 = new DataSet();

                }
                catch (Exception ex)
                {
                    //Код обработки ошибок
                    MessageBox.Show("Ошибка подключения к БД");
                }
                try
                {
                    string sql = @"insert into clients(surname, name, middlename, phone_number)" +
                        $"values ('{surname}', '{name}', '{middlename}', '{phone}')";
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

        private void button2_Click(object sender, EventArgs e)
        {
            cashier cashier = new cashier();
            cashier.Show();
            this.Hide();
        }
    }
}
