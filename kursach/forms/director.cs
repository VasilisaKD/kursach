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
    public partial class director : Form
    {
        string connString;
        public director()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string surname = textBox1.Text;
            string name = textBox2.Text;
            string middlename = textBox3.Text;
            string pasport = maskedTextBox1.Text;
            string date = maskedTextBox3.Text;
            string phone = maskedTextBox2.Text;
            int post = comboBox1.SelectedIndex+1;

            if (string.IsNullOrEmpty(surname) & string.IsNullOrEmpty(name) & string.IsNullOrEmpty(middlename) & string.IsNullOrEmpty(phone) &
                string.IsNullOrEmpty(pasport) & string.IsNullOrEmpty(date))
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
                }
                catch (Exception ex)
                {
                    //Код обработки ошибок
                    MessageBox.Show("Ошибка подключения к БД");
                }
                try
                {
                    string sql = @"insert into staff(surname, name, middlename, pasport, birthday, phone_number, id_post)" +
                        $"values ('{surname}', '{name}', '{middlename}', '{pasport}', '{date}', '{phone}', {post})";
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
    }
}
