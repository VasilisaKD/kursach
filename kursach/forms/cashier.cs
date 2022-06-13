using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach.forms
{
    public partial class cashier : Form
    {
        public cashier()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            forms.addclient addclient = new forms.addclient();
            addclient.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu order = new menu();
            order.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
