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
    public partial class SubscriberForm : Form
    {
        public SubscriberForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1().insertSubscriber(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            MessageBox.Show("Subscriber Added!");
            this.Dispose();
        }
    }
}
