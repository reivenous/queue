using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Queuing
{
    public partial class CustomerView : Form
    {
        private Timer timer;
        private Form1 _form1;
        public CustomerView(Form1 form1)
        {
            InitializeComponent();
            CheckPharmacyBool();

            timer=new Timer();
            timer.Interval = 100;
            timer.Tick += timer1_Tick;
            timer.Start();

            this._form1 = form1;
            _form1 = form1;

        }

        private string connectionString = "server=localhost;user=root;database=db_queue;port=3306";

        private void CustomerView_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            string query = "UPDATE tbl_control set pharmacyOpen = true  where id = @id";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", 1);
            cmd.ExecuteNonQuery();
            //button1.Enabled = false;
            //button2.Enabled = true;
            conn.Close();

            _form1.StartTimer();
        }

        private bool CheckBoolButton()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT cashierOpen FROM tbl_control WHERE id = 1";
                MySqlCommand cmd = new MySqlCommand(query, con);

                // Execute the query and get the result
                object result = cmd.ExecuteScalar();

                // Check if the result is not null and can be converted to a boolean
                if (result != null && result != DBNull.Value)
                {
                    // Convert the result to a boolean
                    return Convert.ToBoolean(result);
                }
                else
                {
                    // Handle the case where the result is null or DBNull.Value
                    // For example, return false or throw an exception
                    return false;
                }
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            string query = "UPDATE tbl_control set pharmacyOpen = false  where id = @id";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", 1);
            cmd.ExecuteNonQuery();
            //button1.Enabled = true;
            //button2.Enabled = false;
            conn.Close();

            _form1.StartTimer();
        }

        private bool CheckPharmacyBool()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT pharmacyOpen FROM tbl_Control WHERE id = 1";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Execute the query and get the result
                object result = cmd.ExecuteScalar();

                // Check if the result is not null and can be converted to a boolean
                if (result != null && result != DBNull.Value)
                {
                    // Convert the result to a boolean
                    return Convert.ToBoolean(result);
                }
                else
                {
                    // Handle the case where the result is null or DBNull.Value
                    // For example, return false or throw an exception
                    return false;
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            bool booleanValue = CheckPharmacyBool();

            button2.Enabled = booleanValue;
            button1.Enabled = !booleanValue;
            button3.Enabled = booleanValue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_form1 != null)
            {
                _form1.ProcessNextQueueNumberPhar(); // Call ProcessNextQueueNumber method to update lblCashier.Text
                _form1.LoadPharmacyQueueNumbers();
                _form1.StopTimer();
            }
        }
    }
}
