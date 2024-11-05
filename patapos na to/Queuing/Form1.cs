using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Queuing
{
    public partial class Form1 : Form
    {
        public Timer timer;
        

        public Form1()
        {
            InitializeComponent();
            LoadCashierQueueNumbers();
            LoadPharmacyQueueNumbers();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer2_Tick;
            timer1.Start();

            
        }



      
        private string connectionString = "server=localhost;user=root;database=db_queue;port=3306";

        private bool isCounterOpen = true; 


        public void UpdateCounterStatus(bool isOpen)
        {
            isCounterOpen = isOpen;
        }


        public void ResetQueueNumbersPhar()
        {

            lblPhar.Text = "Closed";
        }

        public void ResetQueueNumbersCash()
        {

            lblCashier.Text = "Closed";
        }

        private int GetQueueNumberCount()
        {
            int count = 0;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM tbl_queue";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return count;
        }


        private string qno;
    private string serviceName; // Declare as class-level variable
    private string priority; // Declare as class-level variable
    private string priorityN;
        static int number = 1000;
        private void button1_Click(object sender, EventArgs e)
        {

            string counterName = "";
            int count = GetQueueNumberCount();
            count += number; // Increment the count to generate the new queue number

            // Determine the priority based on selected radio button
            if (radReg.Checked == true)
            {
                priority = "R";
            }
            else if (radPrio.Checked == true)
            {
                priority = "P";
            }

            // Generate the queue number based on the selected service
            if (radCash.Checked == true)
            {
                serviceName = "Window 1";
                counterName = "W1-";
                qno = counterName + priority + count.ToString("D3"); // Generate the new queue number
            }
            else if (radPhar.Checked == true)
            {
                serviceName = "Window 2";
                counterName = "W2-";
                qno = counterName + priority + count.ToString("D3"); // Generate the new queue number
            }

            // Create a PrintDocument object and handle the PrintPage event
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintReceipt);

            // Try to print the document immediately (without showing a preview)
            try
            {
                pd.Print(); // This triggers the PrintReceipt method to print the receipt immediately
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing: " + ex.Message); // Display error if printing fails
            }





            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO tbl_queue (serviceName, priority, qnumber) VALUES (@ServiceName, @Priority, @QNumber)";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ServiceName", serviceName);
                cmd.Parameters.AddWithValue("@Priority", priority);
                cmd.Parameters.AddWithValue("@QNumber", qno);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadCashierQueueNumbers();
            LoadPharmacyQueueNumbers();
        }

       

        public void LoadCashierQueueNumbers()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT qnumber FROM tbl_queue WHERE serviceName = 'Window 1' AND IsQueuing = false ORDER BY priority";
                MySqlCommand cmd = new MySqlCommand(query, con);

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    listBox1.Items.Clear();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["qnumber"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load Cashier queue numbers: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }


        public void LoadPharmacyQueueNumbers()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT qnumber FROM tbl_queue WHERE serviceName = 'Window 2' AND IsQueuing = false ORDER BY priority";
                MySqlCommand cmd = new MySqlCommand(query, con);

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    listBox2.Items.Clear();  

                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader["qnumber"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load Pharmacy queue numbers: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void PrintReceipt(object sender, PrintPageEventArgs e)
        {
            // Define the font and brush for drawing text
            Font font = new Font("Arial", 10); // Reduced font size to fit a smaller receipt
            SolidBrush brush = new SolidBrush(Color.Black);

            // Define the printable area and margins
            int leftMargin = 10; // Reduced left margin for smaller paper
            int topMargin = 10;  // Reduced top margin
            int receiptWidth = 300; // Approx 3 inches wide (for 80mm receipt paper)
            int printableHeight = e.PageBounds.Height - topMargin * 2; // Set height based on page bounds

            // Define the content to be printed
            string content = "Window Name: " + serviceName + "\n" +
                             "Queue Number: " + qno + "\n" +
                             "Priority: " + priority + "\n" +
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm"); // Add timestamp to receipt

            // Measure the size of the content based on font and width
            SizeF contentSize = e.Graphics.MeasureString(content, font, receiptWidth - leftMargin * 2);

            // Adjust the font size if the content exceeds the printable area
            float fontSize = font.Size;
            while (contentSize.Height > printableHeight && fontSize > 6) // Ensure font doesn't go too small
            {
                fontSize -= 0.5f;
                font = new Font("Arial", fontSize);
                contentSize = e.Graphics.MeasureString(content, font, receiptWidth - leftMargin * 2);
            }

            // Calculate the position to draw the content
            float x = leftMargin;
            float y = topMargin;

            // Draw the content
            e.Graphics.DrawString(content, font, brush, new RectangleF(x, y, receiptWidth - leftMargin * 2, printableHeight));
        
    }

        private void radPhar_CheckedChanged(object sender, EventArgs e)
        {

        }


        

        public string QueueNumber { get; private set; } // Property to access queueNumber

        public void ProcessNextQueueNumber()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                // Query to fetch the highest priority queue number where IsQueuing is false
                string query = "SELECT qnumber FROM tbl_queue WHERE serviceName = 'Window 1' AND IsQueuing = false ORDER BY CASE WHEN priority = 'P' THEN 1 ELSE 2 END, priority DESC, qnumber ASC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, con);
                QueueNumber = cmd.ExecuteScalar()?.ToString(); // Assign the value to QueueNumber property

                if (!string.IsNullOrEmpty(QueueNumber))
                {
                    // Display the fetched queue number in the lblCashier label
                    lblCashier.Text = QueueNumber;

                    // Update IsQueuing to true for the fetched queue number
                    string updateQuery = "UPDATE tbl_queue SET IsQueuing = true WHERE qnumber = @queueNumber";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@queueNumber", QueueNumber);
                    updateCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public void ProcessNextQueueNumberPhar()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                // Query to fetch the highest priority queue number where IsQueuing is false
                string query = "SELECT qnumber FROM tbl_queue WHERE serviceName = 'Window 2' AND IsQueuing = false ORDER BY CASE WHEN priority = 'P' THEN 1 ELSE 2 END, priority DESC, qnumber ASC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, con);
                string queueNumber = cmd.ExecuteScalar()?.ToString();

                if (!string.IsNullOrEmpty(queueNumber))
                {
                    // Display the fetched queue number in the lblCashier label
                    lblPhar.Text = queueNumber;

                    // Update IsQueuing to true for the fetched queue number
                    string updateQuery = "UPDATE tbl_queue SET IsQueuing = true WHERE qnumber = @queueNumber";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@queueNumber", queueNumber);
                    updateCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }


       

        private void Form1_Load(object sender, EventArgs e)
        {
            StatsForm form = new StatsForm();
            form.Show();
        }

        public void StartTimer()
        {

            timer.Start();

        }

        public void StartTimerCS()
        {

            timer1.Start();

        }

        public void StopTimerCS()
        {
            timer1.Stop();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CustomerView form = new CustomerView(this);
            form.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            CashierControllerForm form = new CashierControllerForm(this);
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
          
            (string pharmacyStatus, string cashierStatus) = FetchNumberFromDatabase();


            lblPhar.Text = pharmacyStatus;
            


        }

        

        private (string pharmacyOpen, string cashierOpen) FetchNumberFromDatabase()
        {
            string pharmacyStatus = ""; // Initialize the pharmacy status variable
            string cashierStatus = ""; // Initialize the cashier status variable

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                // Query to fetch the pharmacyOpen and cashierOpen values from your SQL table
                string query = "SELECT pharmacyOpen, cashierOpen FROM tbl_control WHERE id = 1"; // Adjust the query according to your table structure
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Retrieve the boolean values from the reader
                    bool pharmacyOpen = Convert.ToBoolean(reader["pharmacyOpen"]);
                    bool cashierOpen = Convert.ToBoolean(reader["cashierOpen"]);

                    // Set the status strings based on the boolean values
                    pharmacyStatus = pharmacyOpen ? "Open" : "Closed";
                    cashierStatus = cashierOpen ? "Open" : "Closed";
                }

                reader.Close();
                con.Close();
            }

            return (pharmacyStatus, cashierStatus);
        }

        private void lblCashier_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            (string pharmacyStatus, string cashierStatus) = FetchNumberFromDatabase();


            lblCashier.Text = cashierStatus;
        }

       

       

        

     

        

       

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
