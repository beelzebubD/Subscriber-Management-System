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
    public partial class UserTypeSelection : Form
    {
        public UserTypeSelection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AdminLogin().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserLogin().Show();
            this.Hide();
        }
    }
}
