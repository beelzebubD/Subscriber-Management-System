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
    public partial class ComplaintForm : Form
    {
        public ComplaintForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1().fileComplaint(textBox1.Text);
            MessageBox.Show("Complaint Filed");
            this.Dispose();
        }
    }
}
