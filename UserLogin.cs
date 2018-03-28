using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubscriberManagement
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text;
            String pass = textBox2.Text;
            Form1 F = new Form1();
            if (F.userAuth(email, pass))
            {
                F.Dispose();
                new Form1().Show();
                this.Dispose();
            }

            else MessageBox.Show("Invalid Credentials");
        }
    }
}
