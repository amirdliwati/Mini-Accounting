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
    public partial class cost_details : Form
    {
        public cost_details()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(dateTimePicker1.Value.Date);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(dateTimePicker2.Value.Date);
        }

        private void cost_details_Load(object sender, EventArgs e)
        {
            SqlConnection mycon3 = new SqlConnection(Class1.x);
            mycon3.Open();
            SqlCommand mycom3 = new SqlCommand("select  custmer_name  from custmers  ", mycon3);
            mycom3.CommandType = CommandType.Text;
            SqlDataReader myreder3 = mycom3.ExecuteReader();
            while (myreder3.Read())
            {
                
                
                comboBox1.Items.Add(myreder3[0]);
                

            }
            mycon3.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.Rows.Clear();
                if (textBox1.Text == "" || textBox2.Text == "")
                {

                    SqlConnection con = new SqlConnection(Class1.x);
                    con.Open();
                    SqlCommand com = new SqlCommand("SELECT * from cost where (custmer_name = @custmer_name) AND (cost_in > 0)", con);
                    SqlParameter p = new SqlParameter("@custmer_name", Convert.ToString(comboBox1.Text));
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
                            dataGridView1.Rows.Add(Convert.ToString(myreader[4]), Convert.ToString(myreader[3]), Convert.ToString(myreader[2]), Convert.ToString(myreader[1]), Convert.ToString(myreader[0]));

                        }
                    }
                }

                else
                {

                    SqlConnection con1 = new SqlConnection(Class1.x);
                    con1.Open();
                    SqlCommand com1 = new SqlCommand("SELECT cost_id, custmer_name, cost_in, cost_date, other  from cost where (custmer_name = @custmer_name) AND (cost_date BETWEEN @s AND @e) ", con1);
                    SqlParameter p = new SqlParameter("@custmer_name", Convert.ToString(comboBox1.Text));
                    SqlParameter p1 = new SqlParameter("@s", Convert.ToDateTime(dateTimePicker1.Value.Day + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year));
                    SqlParameter p2 = new SqlParameter("@e", Convert.ToDateTime(dateTimePicker2.Value.Day + "/" + dateTimePicker2.Value.Month + "/" + dateTimePicker2.Value.Year));
                    com1.CommandType = CommandType.Text;
                    com1.Parameters.Add(p);
                    com1.Parameters.Add(p1);
                    com1.Parameters.Add(p2);
                    SqlDataReader myreader1 = com1.ExecuteReader();
                    if (myreader1.HasRows == false)
                    {

                    }
                    else
                    {
                        while (myreader1.Read())
                        {
                            dataGridView1.Rows.Add(Convert.ToString(myreader1[4]), Convert.ToString(myreader1[3]), Convert.ToString(myreader1[2]), Convert.ToString(myreader1[1]), Convert.ToString(myreader1[0]));

                        }
                    }

                }
                comboBox1.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";


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
    }
}
