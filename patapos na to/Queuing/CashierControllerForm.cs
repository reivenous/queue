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
    public partial class CashierControllerForm : Form
    {
        private Timer timer;
        private Form1 _form1; // Assuming you have a reference to Form1
        public CashierControllerForm(Form1 form1)
        {
            CheckBoolButton();
            
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += timer1_Tick;
            timer.Start();


            this._form1 = form1; // Assign the passed instance of Form1 to the field
            _form1 = form1;


           

        }

        private string connectionString = "server=localhost;user=root;database=db_queue;port=3306";

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            string query = "UPDATE tbl_control set cashierOpen = true  where id = @id";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", 1);
            cmd.ExecuteNonQuery();
            //button1.Enabled = false;
            //button2.Enabled = true;
            conn.Close();

            _form1.StartTimerCS();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            

            MySqlConnection conn = new MySqlConnection(connectionString);
            string query = "UPDATE tbl_control set cashierOpen = false  where id = @id";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", 1);
            cmd.ExecuteNonQuery();
            //button1.Enabled = true;
            //button2.Enabled = false;
            conn.Close();

            
            _form1.StartTimerCS();
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

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool booleanValue = CheckBoolButton();

            button2.Enabled = booleanValue;     
            button3.Enabled = booleanValue; // Enable button3 when booleanValue is true, disable otherwise
            button1.Enabled = !booleanValue;
            

        }

        private void CashierControllerForm_Load(object sender, EventArgs e)
        {
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (_form1 != null)
            {
                _form1.ProcessNextQueueNumber(); // Call ProcessNextQueueNumber method to update lblCashier.Text
                _form1.LoadCashierQueueNumbers();
                _form1.StopTimerCS();
            }

            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
