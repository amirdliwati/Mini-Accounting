using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using JbsaPrintDataGridView;

namespace montaser
{
    public partial class bill_details : Form
    {
        public bill_details()
        {
            InitializeComponent();
        }
        string total_bill;
        string balance;
        private void bill_details_Load(object sender, EventArgs e)
        {


            try
            {


            

               
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * from bill_details where (bill_id = @bill_id)", con);
                SqlParameter p = new SqlParameter("@bill_id", Convert.ToInt32(Class1.y));
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    
                }
                else
                {
                    while (myreader.Read())
                    {



                        dataGridView3.Rows.Add(Convert.ToString(myreader[8]), Convert.ToString(myreader[7]), Convert.ToString(myreader[6]), Convert.ToString(myreader[5]), Convert.ToString(myreader[4]), Convert.ToString(myreader[3]), Convert.ToString(myreader[9]), Convert.ToString(myreader[2]), Convert.ToString(myreader[10]));

                    }

                }
            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con4 = new SqlConnection(Class1.x);
                con4.Open();
                SqlCommand com4 = new SqlCommand("select total_cost_bill from bill where bill_id = @bill_id ", con4);
                SqlParameter p10 = new SqlParameter("@bill_id", Convert.ToInt32(Class1.y));
                com4.CommandType = CommandType.Text;
                com4.Parameters.Add(p10);
                SqlDataReader reder4 = com4.ExecuteReader();
                while (reder4.Read())
                {

                    total_bill = Convert.ToString(reder4[0]);




                }
                con4.Close();


                SqlConnection mycon3 = new SqlConnection(Class1.x);
                mycon3.Open();
                SqlCommand mycom3 = new SqlCommand("select balance  from custmers where custmer_name = @custmer_name ", mycon3);
                SqlParameter p3 = new SqlParameter("@custmer_name", Convert.ToString(dataGridView3.Rows[0].Cells[7].Value));
                mycom3.CommandType = CommandType.Text;
                mycom3.Parameters.Add(p3);
                SqlDataReader myreder3 = mycom3.ExecuteReader();
                while (myreder3.Read())
                {
                    balance = Convert.ToString(myreder3[0]);



                }
                mycon3.Close();




                int x = Convert.ToInt32(Class1.y);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("total_cost_item", typeof(string));
                dt.Columns.Add("bill_date", typeof(string));
                dt.Columns.Add("item_price", typeof(string));
                dt.Columns.Add("item_piece", typeof(string));
                dt.Columns.Add("item_id", typeof(string));
                dt.Columns.Add("item_name", typeof(string));
                dt.Columns.Add("custmer_name", typeof(string));
                dt.Columns.Add("item_box", typeof(string));
                foreach (DataGridViewRow dgv in dataGridView3.Rows)
                {
                    dt.Rows.Add(dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value, dgv.Cells[8].Value);
                }
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("Sample.xml");

                menu ff = new menu();
                print_data_gridviwe p_bill = new print_data_gridviwe();
                p_bill.SetDataSource(ds);
                p_bill.SetParameterValue("bill_id", x);
                p_bill.SetParameterValue("total_cost_bill", total_bill );
                p_bill.SetParameterValue("balance", balance);
                p_bill.SetParameterValue("discount",0);
                p_bill.SetParameterValue("cost_in", 0);
                p_bill.SetParameterValue("date_bill",dataGridView3.Rows[0].Cells[2].Value);
                f_print_bill f = new f_print_bill();
                
                f.crystalReportViewer1.ReportSource = p_bill;
                f.crystalReportViewer1.Refresh();
                f.ShowDialog();

            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }



    }
}
