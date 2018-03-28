using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace SubscriberManagement
{
    public partial class Form1 : Form
    {
        private string server;
        private string database;
        private string uid;
        private string password;
        private int adminID;
        public Boolean member;
        private int x;
        private int y;
        private int z;
        private int w;
        private string adr;

        public Form1()
        {
            InitializeComponent();
            server = "localhost";
            database = "sms";
            uid = "root";
            password = "nithin14";
            loadTables();
            member = false;
        }

        public Form1(int a)
        {
            InitializeComponent();
            server = "localhost";
            database = "sms";
            uid = "root";
            password = "nithin14";
            loadTables();
            adminID = a;
            member = false;
        }

        public Form1(int a,Boolean g)
        {
            InitializeComponent();
            server = "localhost";
            database = "sms";
            uid = "root";
            password = "nithin14";
            loadTables();
            adminID = a;
            member = true;
        }

        public void loadTables()
        {
            SubscriberBindGrid();
            ServiceBindGrid();
            FeedbackBindGrid();
            ComplaintBindGrid();
            PopularityBindGrid();
            SubscriptionBindGrid();
        }


        public void email(String recipient,string subject,string body)
        {
            string your_id = "prithvinithinc@gmail.com";
            string your_password = "miit35y01";
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(your_id, your_password),
                    Timeout = 10000,
                };
                MailMessage mm = new MailMessage(your_id, recipient, subject, body);
                client.Send(mm);
                Console.WriteLine("Email Sent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not end email\n\n" + e.ToString());
            }
        }





     
        public bool adminAuth(String email,String pass)
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("Select * from admin where email='"+email+"' and password='"+pass+"'", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count != 0)
                            {
                                return true;

                            }
                            else return false;
                        }
                    }
                }
            }
        }

        public Boolean userAuth(String email, String pass)
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("Select * from subscriber where email='" + email + "' and password='" + pass + "'", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count != 0) return true;
                            else return false;
                        }
                    }
                }
            }
        }

        public void insertSubscriber(String email, String pass, String name, String address, String contact)
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("addSubscriber", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@nam", name);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void insertService(String name, String descr)
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("addService", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nam", name);
                    cmd.Parameters.AddWithValue("@description", descr);
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void insertSubscription()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("addSubscription", con))
                {
                    try
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sid", y);
                        cmd.Parameters.AddWithValue("@uid", x);
                        cmd.ExecuteNonQuery();
                        loadTables();
                        con.Close();
                    }
                    catch(Exception e) { MessageBox.Show("Duplicate Subscription"); }

                }
            }
        }

        public void resolve()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("resolve", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@admin", adminID);
                    cmd.Parameters.AddWithValue("@cid", x);
                    cmd.Parameters.AddWithValue("@com", "");
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void giveFeedback(String f,int t)
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("giveFeedback", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sid", y);
                    cmd.Parameters.AddWithValue("@uid", x);
                    cmd.Parameters.AddWithValue("@fd", f);
                    cmd.Parameters.AddWithValue("@ft", t);
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void fileComplaint(String d)
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("fileComplaint", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@description", d);
                    cmd.Parameters.AddWithValue("@uid", x);
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void removeFeedback()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from feedback where feedback_id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }


        public void setSolved()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("update complaint set solved='Yes' where complaint_id="+w, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void setUnsolved()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("update complaint set solved='No' where complaint_id=" + w, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void delComplaint()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from complaint where complaint_id=" + w, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        public void removeSubscriber()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            int subID = Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from subscriber where subscriber_id="+subID, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();
                 
                }
            }
        }

        public void removeService()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            int serID = Int32.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString());

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from service where service_id=" + serID, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    loadTables();
                    con.Close();

                }
            }
        }

        private void SubscriberBindGrid()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT subscriber_id as ID,name as Name,email as EMail,address as Address,contact as Contact FROM Subscriber", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        private void SubscriptionBindGrid()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT x.service_id as ServID, y.subscriber_id as SubID ,y.email as EMail, z.name as Service from subscription x,subscriber y,service z where x.subscriber_id=y.subscriber_id and x.service_id=z.service_id", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView6.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        private void ServiceBindGrid()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT service_id as ID,name as Name,description as Description FROM Service", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView3.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        private void FeedbackBindGrid()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT feedback_id as ID,feedback as Feedback_Content,ftype as Type FROM feedback", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView4.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        private void ComplaintBindGrid()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT complaint_id as  ComplaintID, description as Description ,solved as isSolved FROM complaint", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        private void PopularityBindGrid()
        {
            string conString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT service_id as ServiceID,name as Name,percentage as Percentage, ratingscore as Rating from service natural join popularity", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView5.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadTables();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadTables();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadTables();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadTables();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadTables();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            setUnsolved();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new SubscriberForm().Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            removeSubscriber();
            MessageBox.Show("Subscriber Removed!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            removeService();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new ServiceForm().Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            insertSubscription();
            MessageBox.Show("Done!");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new FeedbackForm().Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            removeFeedback();
            MessageBox.Show("Deleted!");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            x = Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            MessageBox.Show(x + " Chosen");

        }

        private void button16_Click(object sender, EventArgs e)
        {
            y = Int32.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString());
            MessageBox.Show(y + " Chosen");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            z = Int32.Parse(dataGridView4.CurrentRow.Cells[0].Value.ToString());
            MessageBox.Show(z + " Chosen");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            new ComplaintForm().Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            w= Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            MessageBox.Show(w + " Chosen");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            setSolved();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            delComplaint();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            adr = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            new mailform(adr).Show();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
