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
    public partial class FeedbackForm : Form
    {
        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String feedback = textBox1.Text;
            int n=0;
            if (radioButton1.Checked) n = 1;
            else if (radioButton2.Checked) n = 0;
            else if (radioButton3.Checked) n = -1;
            new Form1().giveFeedback(feedback,n);
            MessageBox.Show("Done!");
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
