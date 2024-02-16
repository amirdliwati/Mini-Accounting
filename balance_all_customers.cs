using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JbsaPrintDataGridView;
using System.Data.SqlClient;

namespace montaser
{
    public partial class balance_all_customers : Form
    {
        public balance_all_customers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {



                PrintJbsaDataGridView.Print_Grid(dataGridView1);



            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void balance_all_customers_Load(object sender, EventArgs e)
        {
            SqlConnection mycon3 = new SqlConnection(Class1.x);
            mycon3.Open();
            SqlCommand mycom3 = new SqlCommand("select  custmer_name , balance   from custmers where balance > 0 ", mycon3);
            mycom3.CommandType = CommandType.Text;
            SqlDataReader myreder3 = mycom3.ExecuteReader();
            while (myreder3.Read())
            {


                dataGridView1.Rows.Add(Convert.ToString(myreder3[1]), Convert.ToString(myreder3[0]));


            }
            mycon3.Close();
        }
    }
}
