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
    public partial class mailform : Form
    {
        private static string address;
        public mailform()
        {
            InitializeComponent();
        }
        public mailform(string adr)
        {
            InitializeComponent();
            address = adr;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subject = textBox1.Text;
            string content = textBox2.Text;
            new Form1().email(address,subject,content);
            MessageBox.Show("Sent!");
            this.Dispose();

        }

        private void mailform_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
