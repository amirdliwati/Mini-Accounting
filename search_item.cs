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
    public partial class search_item : Form
    {
        public search_item()
        {
            InitializeComponent();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();

                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT DISTINCT custmer_name AS Expr1 , SUM(item_piece) AS Expr2 FROM  bill_details WHERE  (item_id = @item_id) GROUP BY custmer_name ", con);
                SqlParameter p = new SqlParameter("@item_id", Convert.ToString(comboBox6.Text));
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();
                    while (myreader.Read())
                    {
                        dataGridView1.Rows.Add(Convert.ToString(myreader[0]), Convert.ToString(myreader[1]));

                    }

                }
            
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void search_item_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Class1.x);
            con.Open();
            SqlCommand com = new SqlCommand("select item_no from items ", con);
            com.CommandType = CommandType.Text;
            SqlDataReader reder = com.ExecuteReader();
            while (reder.Read())
            {
              
                comboBox6.Items.Add(reder[0]);
               

            }
            con.Close();
        }
    }
}
