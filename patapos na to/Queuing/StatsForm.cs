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
    public partial class StatsForm : Form
    {
        public StatsForm()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private string connectionString = "server=localhost;user=root;database=db_queue;port=3306";

        private (double pharmacyPercentage, double cashierPercentage) FetchStatistics()
        {
            int totalRecords = 0;
            int pharmacyCount = 0;
            int cashierCount = 0;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                
                con.Open();

                // Query to fetch total number of records
                string totalQuery = "SELECT COUNT(*) FROM tbl_queue";
                MySqlCommand totalCmd = new MySqlCommand(totalQuery, con);
                totalRecords = Convert.ToInt32(totalCmd.ExecuteScalar());

                // Query to fetch count of records for pharmacy
                string pharmacyQuery = "SELECT COUNT(*) FROM tbl_queue WHERE serviceName = 'Pharmacy'";
                MySqlCommand pharmacyCmd = new MySqlCommand(pharmacyQuery, con);
                pharmacyCount = Convert.ToInt32(pharmacyCmd.ExecuteScalar());

                // Query to fetch count of records for cashier
                string cashierQuery = "SELECT COUNT(*) FROM tbl_queue WHERE serviceName = 'Cashier'";
                MySqlCommand cashierCmd = new MySqlCommand(cashierQuery, con);
                cashierCount = Convert.ToInt32(cashierCmd.ExecuteScalar());

                con.Close();
            }

            // Calculate percentages
            double pharmacyPercentage = Math.Round((pharmacyCount / (double)totalRecords) * 100,2);
            double cashierPercentage = Math.Round((cashierCount / (double)totalRecords) * 100, 2);

            return (pharmacyPercentage, cashierPercentage);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            (double pharmacyCount, double cashierCount) = FetchStatistics();

           
            label4.Text = pharmacyCount.ToString() + "%";
            label5.Text = cashierCount.ToString() + "%";
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
