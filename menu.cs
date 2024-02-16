using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using JbsaPrintDataGridView;
using System.Management;

namespace montaser
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Convert.ToString(MessageBox.Show("هل تريد الخروج؟", "خروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                this.Close();
              
                              
            }
                
                          

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            system_dateandtime.Value = System.DateTime.Now;
            date.Text = Convert.ToString(system_dateandtime.Value.Date.Year) + "/" + Convert.ToString(system_dateandtime.Value.Date.Month) + "/" + Convert.ToString(system_dateandtime.Value.Date.Day);
            time.Text = Convert.ToString(system_dateandtime.Value.Hour) + ":" + Convert.ToString(system_dateandtime.Value.Minute) + ":" + Convert.ToString(system_dateandtime.Value.Second);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] byteImage = ms.ToArray();

                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox11.Text == "" || textBox10.Text == "" || comboBox2.Text == "")
                    errorProvider1.SetError(button3, "عذراً يجب عدم ترك حقل فارغ");

                else
                {
                    SqlConnection mycon = new SqlConnection(Class1.x);
                    
                    mycon.Open();
                    SqlCommand mycom2 = new SqlCommand("select  item_no from items where ((item_no = @item_no))", mycon);
                    SqlParameter p = new SqlParameter("@item_no", textBox2.Text);
                    mycom2.CommandType = CommandType.Text;
                    mycom2.Parameters.Add(p);
                    SqlDataReader myreader = mycom2.ExecuteReader();
                    if (myreader.HasRows == true)
                    {
                        errorProvider1.SetError(button3, "عذراً هذه المادة موجودة مسبقاً");
                        mycon.Close();
                    }

                    else
                    {
                        mycon.Close();
                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("INSERT INTO items  (item_no, item_name, item_box, item_piece, item_price, item_price_one, item_price_all, item_desc , pictures , item_size , item_box_pice , container_id)VALUES (@item_no,@item_name,@item_box,@item_piece,@item_price,@item_price_one,@item_price_all,@item_desc , @pictures , @item_size , @item_box_pice , @container_id)", mycon1);
                        SqlParameter p11 = new SqlParameter("@item_no", textBox2.Text);
                        SqlParameter p4 = new SqlParameter("@item_name", textBox3.Text);
                        SqlParameter p5 = new SqlParameter("@item_box", Convert.ToInt32(textBox4.Text));
                        SqlParameter p6 = new SqlParameter("@item_piece", Convert.ToInt32(textBox5.Text));
                        SqlParameter p7 = new SqlParameter("@item_price", Convert.ToInt32(textBox6.Text));
                        SqlParameter p8 = new SqlParameter("@item_price_one", Convert.ToInt32(textBox8.Text));
                        SqlParameter p9 = new SqlParameter("@item_price_all", Convert.ToInt32(textBox7.Text));
                        SqlParameter p10 = new SqlParameter("@item_desc", textBox9.Text);
                        SqlParameter p12 = new SqlParameter("@pictures", byteImage);
                        SqlParameter p13 = new SqlParameter("@item_size", textBox10.Text);
                        SqlParameter p14 = new SqlParameter("@item_box_pice", Convert.ToInt32(textBox11.Text));
                        SqlParameter p15 = new SqlParameter("@container_id", Convert.ToInt32(comboBox2.Text));
                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p11);
                        mycom1.Parameters.Add(p4);
                        mycom1.Parameters.Add(p5);
                        mycom1.Parameters.Add(p6);
                        mycom1.Parameters.Add(p7);
                        mycom1.Parameters.Add(p8);
                        mycom1.Parameters.Add(p9);
                        mycom1.Parameters.Add(p10);
                        mycom1.Parameters.Add(p12);
                        mycom1.Parameters.Add(p13);
                        mycom1.Parameters.Add(p14);
                        mycom1.Parameters.Add(p15);

                        SqlDataReader myreader1 = mycom1.ExecuteReader();
                        mycon1.Close();

                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";
                        comboBox2.Text = "";
                        pictureBox1.ImageLocation = "";


                        toolTip1.Show("تم الحفظ بنجاح", button3);

                    }
                }

            }
            catch { MessageBox.Show("حدث خطأ بالأدخال"); }
            timer2.Enabled = true;
        }

        private void menu_Load(object sender, EventArgs e)
        {

            SqlConnection mycon0 = new SqlConnection(Class1.x);
            mycon0.Open();
            SqlCommand mycom0 = new SqlCommand("select max(item_id) from items", mycon0);
            mycom0.CommandType = CommandType.Text;
            SqlDataReader myreder0 = mycom0.ExecuteReader();
            
            while (myreder0.Read() )
            {
                if (myreder0[0] != DBNull.Value)
                {
                    int m = Convert.ToInt32(myreder0[0]) + 1;
                    textBox1.Text = m.ToString();
                }
                else { textBox1.Text = "0"; }
            }
            
            mycon0.Close();

            SqlConnection con = new SqlConnection(Class1.x);
            con.Open();
            SqlCommand com = new SqlCommand("select item_no from items ", con);
            com.CommandType = CommandType.Text;
            SqlDataReader reder = com.ExecuteReader();
            while (reder.Read())
            {
                comboBox1.Items.Add(reder[0]);
                comboBox6.Items.Add(reder[0]);
                comboBox10.Items.Add(reder[0]);
                comboBox14.Items.Add(reder[0]);

            }
            con.Close();


            SqlConnection con11 = new SqlConnection(Class1.x);
            con11.Open();
            SqlCommand com11 = new SqlCommand("select container_id from container ", con11);
            com11.CommandType = CommandType.Text;
            SqlDataReader reder11 = com11.ExecuteReader();
            while (reder11.Read())
            {
                comboBox2.Items.Add(reder11[0]);
                comboBox3.Items.Add(reder11[0]);
                comboBox7.Items.Add(reder11[0]);
                comboBox11.Items.Add(reder11[0]);


            }
            con11.Close();

            SqlConnection mycon01 = new SqlConnection(Class1.x);
            mycon01.Open();
            SqlCommand mycom01 = new SqlCommand("select max(container_id) from container", mycon01);
            mycom01.CommandType = CommandType.Text;
            SqlDataReader myreder01 = mycom01.ExecuteReader();
            while (myreder01.Read())
            {
                if (myreder01[0] != DBNull.Value)
                {
                    int m = Convert.ToInt32(myreder01[0]) + 1;
                    textBox12.Text = m.ToString();
                }
                else
                { textBox12.Text = "0"; }
            }
            mycon01.Close();


            SqlConnection mycon3 = new SqlConnection(Class1.x);
            mycon3.Open();
            SqlCommand mycom3 = new SqlCommand("select max(custmer_id) , custmer_name  from custmers GROUP BY custmer_id, custmer_name ", mycon3);
            mycom3.CommandType = CommandType.Text;
            SqlDataReader myreder3 = mycom3.ExecuteReader();
            while (myreder3.Read())
            {
                if (myreder3[0] != DBNull.Value)
                {
                    int mm = Convert.ToInt32(myreder3[0]) + 1;
                    textBox15.Text = mm.ToString();
                    comboBox4.Items.Add(myreder3[1]);
                    comboBox5.Items.Add(myreder3[1]);
                    comboBox8.Items.Add(myreder3[1]);
                    comboBox9.Items.Add(myreder3[1]);
                    comboBox15.Items.Add(myreder3[1]);
                }
                else { textBox15.Text = "0" ; }

            }
            mycon3.Close();

            SqlConnection con4 = new SqlConnection(Class1.x);
            con4.Open();
            SqlCommand com4 = new SqlCommand("select MAX(bill_id) from bill ", con4);
            com4.CommandType = CommandType.Text;
            SqlDataReader reder4 = com4.ExecuteReader();
            while (reder4.Read())
            {
                if (reder4[0] != DBNull.Value)
                {
                    textBox29.Text = Convert.ToString(Convert.ToInt32(reder4[0]) + 1);
                }
                else { textBox29.Text = "1"; }

            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();

            SqlConnection mycon0 = new SqlConnection(Class1.x);
            mycon0.Open();
            SqlCommand mycom0 = new SqlCommand("select max(item_id) from items", mycon0);
            mycom0.CommandType = CommandType.Text;
            SqlDataReader myreder0 = mycom0.ExecuteReader();
            while (myreder0.Read())
            {
                if (myreder0[0] != DBNull.Value)
                {
                    int m = Convert.ToInt32(myreder0[0]) + 1;
                    textBox1.Text = m.ToString();
                }
                else { textBox1.Text = "0"; }
            }
            mycon0.Close();

            comboBox1.Items.Clear();
            comboBox6.Items.Clear();
            SqlConnection con = new SqlConnection(Class1.x);
            con.Open();
            SqlCommand com = new SqlCommand("select item_no from items ", con);
            com.CommandType = CommandType.Text;
            SqlDataReader reder = com.ExecuteReader();
            while (reder.Read())
            {
                comboBox1.Items.Add(reder[0]);
                comboBox6.Items.Add(reder[0]);

            }
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox7.Items.Clear();
            SqlConnection con11 = new SqlConnection(Class1.x);
            con11.Open();
            SqlCommand com11 = new SqlCommand("select container_id from container ", con11);
            com11.CommandType = CommandType.Text;
            SqlDataReader reder11 = com11.ExecuteReader();
            while (reder11.Read())
            {
                comboBox2.Items.Add(reder11[0]);
                comboBox3.Items.Add(reder11[0]);
                comboBox7.Items.Add(reder11[0]);

            }

            con11.Close();

            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox8.Items.Clear();
            SqlConnection mycon3 = new SqlConnection(Class1.x);
            mycon3.Open();
            SqlCommand mycom3 = new SqlCommand("select max(custmer_id) , custmer_name  from custmers GROUP BY custmer_id, custmer_name ", mycon3);
            mycom3.CommandType = CommandType.Text;
            SqlDataReader myreder3 = mycom3.ExecuteReader();
            while (myreder3.Read())
            {
                if (myreder3[0] != DBNull.Value)
                {
                    int n = Convert.ToInt32(myreder3[0]) + 1;
                    textBox15.Text = n.ToString();

                    comboBox4.Items.Add(myreder3[1]);
                    comboBox5.Items.Add(myreder3[1]);
                    comboBox8.Items.Add(myreder3[1]);
                    comboBox9.Items.Add(myreder3[1]);
                }
                else { textBox15.Text = "0" ; }

            }
            mycon3.Close();

            SqlConnection con4 = new SqlConnection(Class1.x);
            con4.Open();
            SqlCommand com4 = new SqlCommand("select MAX(bill_id) from bill ", con4);
            com4.CommandType = CommandType.Text;
            SqlDataReader reder4 = com4.ExecuteReader();
            while (reder4.Read())
            {
                if (reder4[0] != DBNull.Value)
                {
                    textBox29.Text = Convert.ToString(Convert.ToInt32(reder4[0]) + 1);
                }
                else { textBox29.Text = "0"; }


            }
            con4.Close();

            SqlConnection mycon33 = new SqlConnection(Class1.x);
            mycon33.Open();
            SqlCommand mycom33 = new SqlCommand("select balance  from custmers where custmer_name = @custmer_name ", mycon33);
            SqlParameter p133 = new SqlParameter("@custmer_name", Convert.ToString(comboBox9.Text));
            mycom33.CommandType = CommandType.Text;
            mycom33.Parameters.Add(p133);
            SqlDataReader myreder33 = mycom33.ExecuteReader();
            while (myreder33.Read())
            {
                textBox34.Text = Convert.ToString(myreder33[0]);


            }
            mycon3.Close();


            SqlConnection mycon01 = new SqlConnection(Class1.x);
            mycon01.Open();
            SqlCommand mycom01 = new SqlCommand("select max(container_id) from container", mycon01);
            mycom01.CommandType = CommandType.Text;
            SqlDataReader myreder01 = mycom01.ExecuteReader();
            while (myreder01.Read())
            {
                if (myreder01[0] != DBNull.Value)
                {
                    int m = Convert.ToInt32(myreder01[0]) + 1;
                    textBox12.Text = m.ToString();
                }
                else
                { textBox12.Text = "0"; }
            }
            mycon01.Close();

            

            toolTip1.Show("تم التحديث بنجاح", button15);

            timer2.Enabled = true;




        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox1.Text == "")
                {
                    errorProvider1.SetError(button4, "عذراً لا يوجد بيانات لحذفها");
                }

                else if ((Convert.ToString(MessageBox.Show("هل أنت متأكد من الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes"))
                {
                    SqlConnection con = new SqlConnection(Class1.x);
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "DELETE FROM items WHERE (item_id = @item_id)";
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    SqlParameter p1 = new SqlParameter("@item_id", Convert.ToInt32(textBox1.Text));
                    com.Parameters.Add(p1);
                    SqlDataReader reader;
                    reader = com.ExecuteReader();
                    reader.Close();
                    toolTip1.Show("تم الحذف بنجاح", button4);
                }
                timer2.Enabled = true;



            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();

                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * from items where (item_no = @item_no)", con);
                SqlParameter p = new SqlParameter("@item_no", Convert.ToString(comboBox1.Text));
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();

                if (myreader.HasRows == false)
                {
                    errorProvider1.SetError(button6, "عذراً لا يوجد بيانات لتعديلها");
                }
                else
                {
                    while (myreader.Read())
                    {
                        textBox1.Text = Convert.ToString(myreader[0]);
                        textBox2.Text = Convert.ToString(myreader[1]);
                        textBox3.Text = Convert.ToString(myreader[2]);
                        textBox4.Text = Convert.ToString(myreader[3]);
                        textBox5.Text = Convert.ToString(myreader[4]);
                        textBox6.Text = Convert.ToString(myreader[5]);
                        textBox7.Text = Convert.ToString(myreader[7]);
                        textBox8.Text = Convert.ToString(myreader[6]);
                        textBox9.Text = Convert.ToString(myreader[8]);
                        textBox10.Text = Convert.ToString(myreader[10]);
                        textBox11.Text = Convert.ToString(myreader[11]);
                        comboBox2.Text = Convert.ToString(myreader[12]);
                        byte[] image = (byte[])(myreader[9]);
                        MemoryStream msm = new MemoryStream(image);
                        pictureBox1.Image = Image.FromStream(msm);

                    }

                }
            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {


                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "" || comboBox2.Text == "")
                {
                    errorProvider1.SetError(button5, "عذراً يجب عدم ترك حقل فارغ");
                }

              
                 
                else
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] byteImage = ms.ToArray();

                    SqlConnection mycon11 = new SqlConnection(Class1.x);
                    mycon11.Open();
                    SqlCommand mycom11 = new SqlCommand("UPDATE items SET item_no = @item_no, item_name = @item_name, item_box = @item_box, item_piece = @item_piece, item_price = @item_price, item_price_one = @item_price_one,  item_price_all = @item_price_all, item_desc = @item_desc, pictures = @pictures , item_size = @item_size , item_box_pice = @item_box_pice, container_id = @container_id WHERE (item_id = @item_id) ", mycon11);
                    SqlParameter p13 = new SqlParameter("@item_id", Convert.ToInt32(textBox1.Text));
                    SqlParameter p11 = new SqlParameter("@item_no", textBox2.Text);
                    SqlParameter p4 = new SqlParameter("@item_name", textBox3.Text);
                    SqlParameter p5 = new SqlParameter("@item_box", Convert.ToInt32(textBox4.Text));
                    SqlParameter p6 = new SqlParameter("@item_piece", Convert.ToInt32(textBox5.Text));
                    SqlParameter p7 = new SqlParameter("@item_price", Convert.ToInt32(textBox6.Text));
                    SqlParameter p8 = new SqlParameter("@item_price_one", Convert.ToInt32(textBox8.Text));
                    SqlParameter p9 = new SqlParameter("@item_price_all", Convert.ToInt32(textBox7.Text));
                    SqlParameter p10 = new SqlParameter("@item_desc", textBox9.Text);
                    SqlParameter p12 = new SqlParameter("@pictures", byteImage);
                    SqlParameter p15 = new SqlParameter("@item_size", textBox10.Text);
                    SqlParameter p14 = new SqlParameter("@item_box_pice", Convert.ToInt32(textBox11.Text));
                    SqlParameter p16 = new SqlParameter("@container_id", Convert.ToInt32(comboBox2.Text));
                    mycom11.CommandType = CommandType.Text;
                    mycom11.Parameters.Add(p13);
                    mycom11.Parameters.Add(p11);
                    mycom11.Parameters.Add(p4);
                    mycom11.Parameters.Add(p5);
                    mycom11.Parameters.Add(p6);
                    mycom11.Parameters.Add(p7);
                    mycom11.Parameters.Add(p8);
                    mycom11.Parameters.Add(p9);
                    mycom11.Parameters.Add(p10);
                    mycom11.Parameters.Add(p12);
                    mycom11.Parameters.Add(p14);
                    mycom11.Parameters.Add(p15);
                    mycom11.Parameters.Add(p16);

                    SqlDataReader myreader11 = mycom11.ExecuteReader();
                    mycon11.Close();


                    SqlConnection mycon3 = new SqlConnection(Class1.x);
                    mycon3.Open();
                    SqlCommand mycom3 = new SqlCommand("UPDATE bill_details SET item_id = @item_id WHERE (item_id = @item_id1) ", mycon3);
                    SqlParameter p18 = new SqlParameter("@item_id", textBox2.Text);
                    SqlParameter p19 = new SqlParameter("@item_id1", comboBox1.Text);
                    mycom3.CommandType = CommandType.Text;
                    mycom3.Parameters.Add(p18);
                    mycom3.Parameters.Add(p19);
                    SqlDataReader myreader3 = mycom3.ExecuteReader();
                    mycon3.Close();



                }
                toolTip1.Show("تم التعديل بنجاح", button5);



            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

       

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
            SqlConnection mycon11 = new SqlConnection(Class1.x);
            mycon11.Open();
            SqlCommand mycom11 = new SqlCommand("INSERT INTO container  ( container_desc, other )VALUES (@container_desc,@other )", mycon11);

            SqlParameter p4 = new SqlParameter("@container_desc", textBox13.Text);
            SqlParameter p5 = new SqlParameter("@other", textBox14.Text);
            mycom11.CommandType = CommandType.Text;

            mycom11.Parameters.Add(p4);
            mycom11.Parameters.Add(p5);
            SqlDataReader myreader11 = mycom11.ExecuteReader();
            mycon11.Close();
            toolTip1.Show("تم الحفظ بنجاح", button7);
            timer2.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
            dataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(Class1.x);
            con.Open();
            SqlCommand com = new SqlCommand("SELECT * from container where (container_id = @container_id)", con);
            SqlParameter p = new SqlParameter("@container_id", Convert.ToString(comboBox3.Text));
            com.CommandType = CommandType.Text;
            com.Parameters.Add(p);
            SqlDataReader myreader = com.ExecuteReader();

            if (myreader.HasRows == false)
            {
                errorProvider1.SetError(button8, "عذراً لا يوجد بيانات ");
            }

            while (myreader.Read())
            {
                dataGridView1.Rows.Add(Convert.ToString(myreader[0]), Convert.ToString(myreader[1]), Convert.ToString(myreader[2]));
            }
            con.Close();
            timer2.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox15.Text == "" || textBox16.Text == "" || textBox17.Text == "" || textBox18.Text == "" || textBox19.Text == "" || textBox20.Text == "" || textBox21.Text == "")
                    errorProvider1.SetError(button9, "عذراً يجب عدم ترك حقل فارغ");
                else
                {
                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("INSERT INTO custmers  ( custmer_name, adrees, phone, mobile, fax, other , balance)VALUES (@custmer_name,@adrees,@phone,@mobile,@fax,@other , @balance)", mycon1);
                    SqlParameter p11 = new SqlParameter("@custmer_name", textBox16.Text);
                    SqlParameter p4 = new SqlParameter("@adrees", textBox17.Text);
                    SqlParameter p5 = new SqlParameter("@phone", Convert.ToInt64(textBox18.Text));
                    SqlParameter p6 = new SqlParameter("@mobile", Convert.ToInt64(textBox19.Text));
                    SqlParameter p7 = new SqlParameter("@fax", Convert.ToInt64(textBox20.Text));
                    SqlParameter p8 = new SqlParameter("@other", textBox21.Text);
                    SqlParameter p9 = new SqlParameter("@balance", Convert.ToInt64(textBox51.Text));

                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p11);
                    mycom1.Parameters.Add(p4);
                    mycom1.Parameters.Add(p5);
                    mycom1.Parameters.Add(p6);
                    mycom1.Parameters.Add(p7);
                    mycom1.Parameters.Add(p8);
                    mycom1.Parameters.Add(p9);

                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    mycon1.Close();

                    textBox15.Text = "";
                    textBox16.Text = "";
                    textBox17.Text = "";
                    textBox18.Text = "";
                    textBox19.Text = "";
                    textBox20.Text = "";
                    textBox21.Text = "";
                    textBox51.Text = "";

                    SqlConnection mycon3 = new SqlConnection(Class1.x);
                    mycon3.Open();
                    SqlCommand mycom3 = new SqlCommand("select max(custmer_id) , custmer_name  from custmers GROUP BY custmer_id, custmer_name ", mycon3);
                    mycom3.CommandType = CommandType.Text;
                    SqlDataReader myreder3 = mycom3.ExecuteReader();
                    while (myreder3.Read())
                    {
                        if (myreder3[0] != DBNull.Value)
                        {
                            int mm = Convert.ToInt32(myreder3[0]) + 1;
                            textBox15.Text = Convert.ToString(mm);
                        }
                        else { textBox15.Text = "0"; }

                    }
                    mycon3.Close();

                    toolTip1.Show("تم الحفظ بنجاح", button3);
                }

            }
            catch { MessageBox.Show("حدث خطأ بالأدخال"); }
            timer2.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();

                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * from custmers where (custmer_name = @custmer_name)", con);
                SqlParameter p = new SqlParameter("@custmer_name", Convert.ToString(comboBox4.Text));
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    errorProvider1.SetError(button6, "عذراً لا يوجد بيانات لتعديلها");
                }
                else
                {
                    while (myreader.Read())
                    {
                        textBox15.Text = Convert.ToString(myreader[0]);
                        textBox16.Text = Convert.ToString(myreader[1]);
                        textBox17.Text = Convert.ToString(myreader[2]);
                        textBox18.Text = Convert.ToString(myreader[3]);
                        textBox19.Text = Convert.ToString(myreader[4]);
                        textBox20.Text = Convert.ToString(myreader[5]);
                        textBox21.Text = Convert.ToString(myreader[6]);
                        textBox51.Text = Convert.ToString(myreader[7]);

                    }

                }
            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {

                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox15.Text == "")
                {
                    errorProvider1.SetError(button10, "عذراً لا يوجد بيانات لحذفها");
                }
                else if ((Convert.ToString(MessageBox.Show("هل أنت متأكد من الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes"))
                {
                    SqlConnection con = new SqlConnection(Class1.x);
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "DELETE FROM custmers WHERE (custmer_id = @custmer_id)";
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    SqlParameter p1 = new SqlParameter("@custmer_id", Convert.ToInt32(textBox15.Text));
                    com.Parameters.Add(p1);
                    SqlDataReader reader;
                    reader = com.ExecuteReader();
                    reader.Close();


                    SqlConnection con1 = new SqlConnection(Class1.x);
                    con1.Open();
                    SqlCommand com1 = new SqlCommand();
                    com1.CommandText = "DELETE FROM bill WHERE (custmer_name = @custmer_name)";
                    com1.Connection = con1;
                    com1.CommandType = CommandType.Text;
                    SqlParameter p11 = new SqlParameter("@custmer_name", textBox16.Text);
                    com1.Parameters.Add(p11);
                    SqlDataReader reader1;
                    reader1 = com1.ExecuteReader();
                    reader1.Close();

                    SqlConnection con2 = new SqlConnection(Class1.x);
                    con2.Open();
                    SqlCommand com2 = new SqlCommand();
                    com2.CommandText = "DELETE FROM bill_details WHERE (custmer_name = @custmer_name)";
                    com2.Connection = con2;
                    com2.CommandType = CommandType.Text;
                    SqlParameter p12 = new SqlParameter("@custmer_name", textBox16.Text);
                    com2.Parameters.Add(p12);
                    SqlDataReader reader2;
                    reader2 = com2.ExecuteReader();
                    reader2.Close();

                    SqlConnection con3 = new SqlConnection(Class1.x);
                    con3.Open();
                    SqlCommand com3 = new SqlCommand();
                    com3.CommandText = "DELETE FROM cost WHERE (custmer_name = @custmer_name)";
                    com3.Connection = con3;
                    com3.CommandType = CommandType.Text;
                    SqlParameter p13 = new SqlParameter("@custmer_name", textBox16.Text);
                    com3.Parameters.Add(p13);
                    SqlDataReader reader3;
                    reader3 = com3.ExecuteReader();
                    reader3.Close();
                }
                timer2.Enabled = true;
                toolTip1.Show("تم الحذف بنجاح", button10);
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox16.Text == "" || textBox17.Text == "" || textBox18.Text == "" || textBox19.Text == "" || textBox20.Text == "" || textBox21.Text == "")
                {
                    errorProvider1.SetError(button10, "عذراً يجب عدم ترك حقل فارغ");
                }
                else
                {


                    SqlConnection mycon11 = new SqlConnection(Class1.x);
                    mycon11.Open();
                    SqlCommand mycom11 = new SqlCommand("UPDATE custmers SET custmer_name = @custmer_name, adrees = @adrees, phone = @phone, mobile = @mobile, fax = @fax, other = @other , balance = @balance  WHERE (custmer_id = @custmer_id) ", mycon11);
                    SqlParameter p13 = new SqlParameter("@custmer_id", Convert.ToInt32(textBox15.Text));
                    SqlParameter p11 = new SqlParameter("@custmer_name", textBox16.Text);
                    SqlParameter p4 = new SqlParameter("@adrees", textBox17.Text);
                    SqlParameter p5 = new SqlParameter("@phone", Convert.ToInt64(textBox18.Text));
                    SqlParameter p6 = new SqlParameter("@mobile", Convert.ToInt64(textBox19.Text));
                    SqlParameter p7 = new SqlParameter("@fax", Convert.ToInt64(textBox20.Text));
                    SqlParameter p8 = new SqlParameter("@other", textBox21.Text);
                    SqlParameter p9 = new SqlParameter("@balance", Convert.ToInt64(textBox51.Text));

                    

                    mycom11.CommandType = CommandType.Text;
                    mycom11.Parameters.Add(p13);
                    mycom11.Parameters.Add(p11);
                    mycom11.Parameters.Add(p4);
                    mycom11.Parameters.Add(p5);
                    mycom11.Parameters.Add(p6);
                    mycom11.Parameters.Add(p7);
                    mycom11.Parameters.Add(p8);
                    mycom11.Parameters.Add(p9);


                    SqlDataReader myreader11 = mycom11.ExecuteReader();
                    mycon11.Close();
                    
                    
                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("UPDATE bill SET custmer_name = @custmer_name WHERE (custmer_name = @custmer_name1) ", mycon1);
                    SqlParameter p14 = new SqlParameter("@custmer_name", textBox16.Text);
                    SqlParameter p15 = new SqlParameter("@custmer_name1",comboBox4.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p14);
                    mycom1.Parameters.Add(p15);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    mycon1.Close();


                    SqlConnection mycon2 = new SqlConnection(Class1.x);
                    mycon2.Open();
                    SqlCommand mycom2 = new SqlCommand("UPDATE bill_details SET custmer_name = @custmer_name WHERE (custmer_name = @custmer_name1) ", mycon2);
                    SqlParameter p16 = new SqlParameter("@custmer_name", textBox16.Text);
                    SqlParameter p17 = new SqlParameter("@custmer_name1", comboBox4.Text);
                    mycom2.CommandType = CommandType.Text;
                    mycom2.Parameters.Add(p16);
                    mycom2.Parameters.Add(p17);
                    SqlDataReader myreader2 = mycom2.ExecuteReader();
                    mycon2.Close();

                    SqlConnection mycon3 = new SqlConnection(Class1.x);
                    mycon3.Open();
                    SqlCommand mycom3 = new SqlCommand("UPDATE cost SET custmer_name = @custmer_name WHERE (custmer_name = @custmer_name1) ", mycon3);
                    SqlParameter p18 = new SqlParameter("@custmer_name", textBox16.Text);
                    SqlParameter p19 = new SqlParameter("@custmer_name1", comboBox4.Text);
                    mycom3.CommandType = CommandType.Text;
                    mycom3.Parameters.Add(p18);
                    mycom3.Parameters.Add(p19);
                    SqlDataReader myreader3 = mycom3.ExecuteReader();
                    mycon3.Close();
                    

                }
                toolTip1.Show("تم التعديل بنجاح", button10);
            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox24.Text = Convert.ToString(dateTimePicker1.Value);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox12.Items.Clear();
            SqlConnection con = new SqlConnection(Class1.x);
            con.Open();
            SqlCommand com = new SqlCommand("select   item_box_pice , item_name from items where item_no = @item_no  ", con);
            SqlParameter p = new SqlParameter("@item_no", Convert.ToString(comboBox6.Text));
            com.CommandType = CommandType.Text;
            com.Parameters.Add(p);
            SqlDataReader reder = com.ExecuteReader();
            while (reder.Read())
            {
                textBox22.Text = Convert.ToString(reder[0]);
                textBox38.Text = Convert.ToString(reder[1]);

            }
            con.Close();


                comboBox12.Items.Clear();
                SqlConnection con1 = new SqlConnection(Class1.x);
                con1.Open();
                SqlCommand com1 = new SqlCommand("select item_price_one , item_price_all , item_price  from items where item_no = @item_no  ", con1);
                SqlParameter p1 = new SqlParameter("@item_no", Convert.ToString(comboBox6.Text));
                com1.CommandType = CommandType.Text;
                com1.Parameters.Add(p1);
                SqlDataReader reder1 = com1.ExecuteReader();
                while (reder1.Read())
                {
                    comboBox12.Items.Add(reder1[0]);
                    comboBox12.Items.Add(reder1[1]);
                    comboBox12.Items.Add(reder1[2]);

                }
                con1.Close();

            

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox23.Text == "" || textBox23.Text == Convert.ToString(0) || Convert.ToInt32(textBox23.Text) > Convert.ToInt32(textBox22.Text))
                {
                    textBox23.Text = Convert.ToString(0);

                }
                else
                {
                    int x = Convert.ToInt32(textBox23.Text);
                    int y = Convert.ToInt32(comboBox12.Text);
                    int z = x * y;
                    textBox26.Text = Convert.ToString(z);
                    textBox28.Text = Convert.ToString(z);
                }
            }
            catch
            {
                MessageBox.Show(" الرجاء ادخال الصيغة الصحيحة لعدد الأزواج");
                textBox23.Text = "";
            }
        }
            
        

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox27.Text == "")
                {
                    textBox27.Text = Convert.ToString(0);

                }
                else
                {
                    try
                    {
                        int x = Convert.ToInt32(textBox26.Text);
                        int y = Convert.ToInt32(textBox27.Text);
                        int z = x - y;
                        textBox28.Text = Convert.ToString(z);
                    }
                    catch
                    {
                        MessageBox.Show("الرجاء ادخال الصيغة الصحيحة للحسم");
                        textBox23.Text = "";
                    }
                }

            }
            catch
            {
                MessageBox.Show("الرجاء ادخال الصيغة الصحيحة للحسم");
                textBox23.Text = "";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {

                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();

                if (comboBox5.Text == "" || comboBox6.Text == "" || textBox22.Text == "" || textBox23.Text == "" || comboBox7.Text == "" || textBox24.Text == "" || comboBox12.Text == "" || textBox26.Text == "" || textBox27.Text == "" || textBox28.Text == "" || textBox38.Text == "" || textBox50.Text == "")

                { errorProvider1.SetError(button13, "عذراً يجب عدم ترك حقل فارغ"); }
                else
                {
                    int x = Convert.ToInt32(textBox28.Text);
                    int y = Convert.ToInt32(textBox30.Text);
                    int z = x + y;
                    textBox30.Text = Convert.ToString(z);
                    textBox32.Text = Convert.ToString(z);
                    textBox49.Text = Convert.ToString(z);
                    dataGridView2.Rows.Add(textBox28.Text, textBox24.Text, textBox23.Text, comboBox12.Text, Convert.ToString(comboBox6.SelectedItem), textBox38.Text, Convert.ToString(comboBox5.SelectedItem), Convert.ToString(textBox50.Text), Convert.ToString(comboBox7.SelectedItem));
                    toolTip1.Show("تمت الإضافة بنجاح", button13);

                }

            }
            catch
            { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }


            finally
            {
                comboBox6.Text = "";
                comboBox12.Text = "";
                comboBox7.Text = "";
                textBox22.Text = "";
                textBox23.Text = "";
                textBox26.Text = "";
                textBox27.Text = Convert.ToString(0);
                textBox28.Text = "";
                textBox38.Text = "";
                textBox50.Text = "";
                timer2.Enabled = true;
            }


        }

       

        private void textBox31_TextChanged(object sender, EventArgs e)
        {



            try
            {
                if (textBox31.Text == "")
                { textBox31.Text = Convert.ToString(0); }
                else
                {
                    int x = Convert.ToInt32(textBox30.Text);
                    int y = Convert.ToInt32(textBox31.Text);
                    int z = x - y;
                    textBox32.Text = Convert.ToString(z);
                }
            }
            catch
            {
                MessageBox.Show(" الرجاء ادخال الصيغة الصحيحة للدفعة");
                textBox23.Text = "";
            }


        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (comboBox5.Text == "" || textBox24.Text == "" || textBox30.Text == "" || textBox33.Text == "")
                { errorProvider1.SetError(button14, "عذراً يجب عدم ترك حقل فارغ"); }
                else
                {


                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        SqlConnection mycon11 = new SqlConnection(Class1.x);
                        mycon11.Open();
                        SqlCommand mycom11 = new SqlCommand("UPDATE items SET item_box_pice = item_box_pice - @item_box_pice WHERE (item_no = @item_no) ", mycon11);
                        SqlParameter p13 = new SqlParameter("@item_no", Convert.ToString(dataGridView2.Rows[i].Cells[4].Value));
                        SqlParameter p14 = new SqlParameter("@item_box_pice", Convert.ToInt64(dataGridView2.Rows[i].Cells[2].Value));
                        mycom11.CommandType = CommandType.Text;
                        mycom11.Parameters.Add(p13);
                        mycom11.Parameters.Add(p14);
                        SqlDataReader myreader11 = mycom11.ExecuteReader();
                        mycon11.Close();




                        SqlConnection mycon12 = new SqlConnection(Class1.x);
                        mycon12.Open();
                        SqlCommand mycom12 = new SqlCommand("INSERT INTO bill_details  (bill_id, custmer_name, item_id, item_price, item_piece, bill_date, total_cost_item , container_id , item_name , item_box ) VALUES (@bill_id, @custmer_name, @item_id, @item_price, @item_piece, @bill_date, @total_cost_item , @container_id , @item_name , @item_box )  ", mycon12);
                        SqlParameter p11 = new SqlParameter("@bill_id", Convert.ToInt32(textBox29.Text));
                        SqlParameter p4 = new SqlParameter("@custmer_name", comboBox5.Text);
                        SqlParameter p5 = new SqlParameter("@item_id", Convert.ToString(dataGridView2.Rows[i].Cells[4].Value));
                        SqlParameter p6 = new SqlParameter("@item_price", Convert.ToInt64(dataGridView2.Rows[i].Cells[3].Value));
                        SqlParameter p7 = new SqlParameter("@item_piece", Convert.ToInt64(dataGridView2.Rows[i].Cells[2].Value));
                        SqlParameter p8 = new SqlParameter("@bill_date", Convert.ToDateTime(dataGridView2.Rows[i].Cells[1].Value));
                        SqlParameter p9 = new SqlParameter("@total_cost_item", Convert.ToInt64(dataGridView2.Rows[i].Cells[0].Value));
                        SqlParameter p10 = new SqlParameter("@container_id", Convert.ToString(dataGridView2.Rows[i].Cells[8].Value));
                        SqlParameter p12 = new SqlParameter("@item_name", Convert.ToString(dataGridView2.Rows[i].Cells[5].Value));
                        SqlParameter p130 = new SqlParameter("@item_box", Convert.ToInt64(dataGridView2.Rows[i].Cells[7].Value));


                        mycom12.CommandType = CommandType.Text;
                        mycom12.Parameters.Add(p11);
                        mycom12.Parameters.Add(p4);
                        mycom12.Parameters.Add(p5);
                        mycom12.Parameters.Add(p6);
                        mycom12.Parameters.Add(p7);
                        mycom12.Parameters.Add(p8);
                        mycom12.Parameters.Add(p9);
                        mycom12.Parameters.Add(p10);
                        mycom12.Parameters.Add(p12);
                        mycom12.Parameters.Add(p130);

                        SqlDataReader myreader12 = mycom12.ExecuteReader();
                        mycon12.Close();
                    }


                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("INSERT INTO bill  (custmer_name, bill_date, total_cost_bill, other, bill_date2) VALUES ( @custmer_name, @bill_date, @total_cost_bill, @other ,@bill_date2)  ", mycon1);
                    SqlParameter p111 = new SqlParameter("@custmer_name", comboBox5.Text);
                    SqlParameter p41 = new SqlParameter("@bill_date", Convert.ToDateTime(textBox24.Text));
                    SqlParameter p51 = new SqlParameter("@total_cost_bill", Convert.ToInt64(textBox30.Text));
                    SqlParameter p61 = new SqlParameter("@other", Convert.ToString(textBox33.Text));
                    SqlParameter p15 = new SqlParameter("@bill_date2", Convert.ToString(dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year));
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p111);
                    mycom1.Parameters.Add(p41);
                    mycom1.Parameters.Add(p51);
                    mycom1.Parameters.Add(p61);
                    mycom1.Parameters.Add(p15);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();

                    mycon1.Close();

                    SqlConnection mycon113 = new SqlConnection(Class1.x);
                    mycon113.Open();
                    SqlCommand mycom113 = new SqlCommand("INSERT INTO cost  (custmer_name, cost_in, cost_date , other ,bill_date2) VALUES ( @custmer_name, @cost_in, @cost_date , @other , @bill_date2)  ", mycon113);
                    SqlParameter p1 = new SqlParameter("@custmer_name", comboBox5.Text);
                    SqlParameter p143 = new SqlParameter("@cost_in", Convert.ToInt64(textBox31.Text));
                    SqlParameter p16 = new SqlParameter("@cost_date", Convert.ToDateTime(textBox24.Text));
                    SqlParameter p17 = new SqlParameter("@other", Convert.ToString(textBox33.Text));
                    SqlParameter p18 = new SqlParameter("@bill_date2", Convert.ToString(dateTimePicker1.Value.Day + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year));
                    mycom113.CommandType = CommandType.Text;
                    mycom113.Parameters.Add(p1);
                    mycom113.Parameters.Add(p143);
                    mycom113.Parameters.Add(p16);
                    mycom113.Parameters.Add(p17);
                    mycom113.Parameters.Add(p18);
                    SqlDataReader myreader113 = mycom113.ExecuteReader();

                    mycon113.Close();

                    SqlConnection mycon1113 = new SqlConnection(Class1.x);
                    mycon1113.Open();
                    SqlCommand mycom111 = new SqlCommand(" UPDATE  custmers SET  balance = balance + @balance where custmer_name = @custmer_name ", mycon1113);
                    SqlParameter p1113 = new SqlParameter("@custmer_name", comboBox5.Text);
                    SqlParameter p141 = new SqlParameter("@balance", Convert.ToInt64(textBox32.Text));
                    mycom111.Parameters.Add(p1113);
                    mycom111.Parameters.Add(p141);
                    SqlDataReader myreader111 = mycom111.ExecuteReader();

                    mycon1113.Close();
                }
               

                toolTip1.Show("تمت الإضافة بنجاح", button14);

                SqlConnection con4 = new SqlConnection(Class1.x);
                con4.Open();
                SqlCommand com4 = new SqlCommand("select MAX(bill_id) from bill ", con4);
                com4.CommandType = CommandType.Text;
                SqlDataReader reder4 = com4.ExecuteReader();
                while (reder4.Read())
                {
                    if (reder4[0] != DBNull.Value)
                    {
                        textBox29.Text = Convert.ToString(Convert.ToInt32(reder4[0]) + 1);
                    }
                    else { textBox29.Text = "0"; }


                }
                con4.Close();


                
                    SqlConnection mycon3 = new SqlConnection(Class1.x);
                    mycon3.Open();
                    SqlCommand mycom3 = new SqlCommand("select balance  from custmers where custmer_name = @custmer_name ", mycon3);
                    SqlParameter p3 = new SqlParameter("@custmer_name", Convert.ToString(dataGridView2.Rows[0].Cells[6].Value));
                    mycom3.CommandType = CommandType.Text;
                    mycom3.Parameters.Add(p3);
                    SqlDataReader myreder3 = mycom3.ExecuteReader();
                    while (myreder3.Read())
                    {

                        customer_balance.Text = Convert.ToString(myreder3[0]);


                    }
                    mycon3.Close();

            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Rows.Clear();
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * from bill where (custmer_name = @custmer_name)", con);
                SqlParameter p = new SqlParameter("@custmer_name", Convert.ToString(comboBox8.Text));
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    errorProvider1.SetError(button17, "عذراً لا يوجد بيانات للبحث عنها");
                }
                else
                {
                    while (myreader.Read())
                    {
                        dataGridView3.Rows.Add(Convert.ToString(myreader[0]), Convert.ToString(myreader[1]), Convert.ToString(myreader[2]), Convert.ToString(myreader[3]), Convert.ToString(myreader[4]));

                    }
                    SqlConnection mycon3 = new SqlConnection(Class1.x);
                    mycon3.Open();
                    SqlCommand mycom3 = new SqlCommand("select balance  from custmers where custmer_name = @custmer_name ", mycon3);
                    SqlParameter p1 = new SqlParameter("@custmer_name", Convert.ToString(comboBox8.Text));
                    mycom3.CommandType = CommandType.Text;
                    mycom3.Parameters.Add(p1);
                    SqlDataReader myreder3 = mycom3.ExecuteReader();
                    while (myreder3.Read())
                    {
                        textBox34.Text = Convert.ToString(myreder3[0]);


                    }
                    mycon3.Close();

                }
            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;

        }



        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();

                Class1.y = Convert.ToString(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                bill_details bi = new bill_details();
                bi.Show();


            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            textBox36.Text = Convert.ToString(dateTimePicker2.Value);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (comboBox9.Text == "" || textBox35.Text == "" || textBox36.Text == "" || textBox37.Text == "")
                { errorProvider1.SetError(button19, "عذراً يجب عدم ترك حقل فارغ"); }
                else
                {
                    SqlConnection mycon11 = new SqlConnection(Class1.x);
                    mycon11.Open();
                    SqlCommand mycom11 = new SqlCommand("INSERT INTO cost  (custmer_name, cost_in, cost_date , other ,bill_date2) VALUES ( @custmer_name, @cost_in, @cost_date , @other ,@bill_date2)  ", mycon11);
                    SqlParameter p1 = new SqlParameter("@custmer_name", comboBox9.Text);
                    SqlParameter p14 = new SqlParameter("@cost_in", Convert.ToInt64(textBox35.Text));
                    SqlParameter p16 = new SqlParameter("@cost_date", Convert.ToDateTime(textBox36.Text));
                    SqlParameter p17 = new SqlParameter("@other", Convert.ToString(textBox37.Text));
                    SqlParameter p15 = new SqlParameter("@bill_date2", Convert.ToString(dateTimePicker2.Value.Day + "/" + dateTimePicker2.Value.Month + "/" + dateTimePicker2.Value.Year));
                    mycom11.CommandType = CommandType.Text;
                    mycom11.Parameters.Add(p1);
                    mycom11.Parameters.Add(p14);
                    mycom11.Parameters.Add(p16);
                    mycom11.Parameters.Add(p17);
                    mycom11.Parameters.Add(p15);
                    SqlDataReader myreader11 = mycom11.ExecuteReader();

                    mycon11.Close();

                    SqlConnection mycon111 = new SqlConnection(Class1.x);
                    mycon111.Open();
                    SqlCommand mycom111 = new SqlCommand(" UPDATE  custmers SET  balance = balance - @balance where custmer_name = @custmer_name ", mycon111);
                    SqlParameter p111 = new SqlParameter("@custmer_name", comboBox9.Text);
                    SqlParameter p141 = new SqlParameter("@balance", Convert.ToInt64(textBox35.Text));
                    mycom111.Parameters.Add(p111);
                    mycom111.Parameters.Add(p141);
                    SqlDataReader myreader111 = mycom111.ExecuteReader();

                    mycon111.Close();
                }
                textBox35.Text = "0";
                textBox36.Text = "";
                textBox37.Text = "لا يوجد";

                toolTip1.Show("تمت الإضافة بنجاح", button19);


            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;



        }

        private void button20_Click(object sender, EventArgs e)
        {
            cost_details co = new cost_details();
            co.Show();

        }

        private void button21_Click(object sender, EventArgs e)
        {
            int ccount53 = 0;
            int ccount52 = 0;
            int ccount54 = 0;
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (comboBox11.Text == "")
                {
                    SqlConnection con = new SqlConnection(Class1.x);
                    con.Open();
                    SqlCommand com = new SqlCommand("SELECT * from items where (item_no = @item_no)", con);
                    SqlParameter p = new SqlParameter("@item_no", Convert.ToString(comboBox10.Text));
                    com.CommandType = CommandType.Text;
                    com.Parameters.Add(p);
                    SqlDataReader myreader = com.ExecuteReader();

                    if (myreader.HasRows == false)
                    {
                        errorProvider1.SetError(button21, "عذراً لا يوجد بيانات للبحث عنها");
                    }
                    else
                    {
                        while (myreader.Read())
                        {
                            dataGridView4.Rows.Add(Convert.ToString(myreader[12]), Convert.ToString(myreader[11]), Convert.ToString(myreader[10]), Convert.ToString(myreader[8]), Convert.ToString(myreader[7]), Convert.ToString(myreader[6]), Convert.ToString(myreader[5]), Convert.ToString(myreader[4]), Convert.ToString(myreader[3]), Convert.ToString(myreader[2]), Convert.ToString(myreader[1]), Convert.ToString(myreader[0]));

                        }

                    }
                }


                else if (comboBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(Class1.x);
                    con1.Open();
                    SqlCommand com1 = new SqlCommand("SELECT * from items where (container_id = @container_id)AND (item_box_pice > 0) ", con1);
                    SqlParameter p1 = new SqlParameter("@container_id", Convert.ToString(comboBox11.Text));
                    com1.CommandType = CommandType.Text;
                    com1.Parameters.Add(p1);
                    SqlDataReader myreader1 = com1.ExecuteReader();

                    if (myreader1.HasRows == false)
                    {
                        errorProvider1.SetError(button21, "عذراً لا يوجد بيانات للبحث عنها");
                    }
                    else
                    {
                        while (myreader1.Read())
                        {
                            dataGridView4.Rows.Add(Convert.ToString(myreader1[12]), Convert.ToString(myreader1[11]), Convert.ToString(myreader1[10]), Convert.ToString(myreader1[8]), Convert.ToString(myreader1[7]), Convert.ToString(myreader1[6]), Convert.ToString(myreader1[5]), Convert.ToString(myreader1[4]), Convert.ToString(myreader1[3]), Convert.ToString(myreader1[2]), Convert.ToString(myreader1[1]), Convert.ToString(myreader1[0]));
                            
                        }

                        for (int i = 0; i < dataGridView4.Rows.Count; i++)
                        { dataGridView4.Rows[i].Cells[12].Value = Convert.ToString(Convert.ToInt32(dataGridView4.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView4.Rows[i].Cells[6].Value));
                        
                        ccount53 = ccount53 + Convert.ToInt32(dataGridView4.Rows[i].Cells[12].Value);
                        ccount54 = ccount54 + Convert.ToInt32(dataGridView4.Rows[i].Cells[1].Value);
                        ccount52 = ccount52 + Convert.ToInt32(dataGridView4.Rows[i].Cells[6].Value);
                          
                        }

                        textBox53.Text = Convert.ToString(ccount53);
                        textBox52.Text = Convert.ToString(ccount52);
                        textBox54.Text = Convert.ToString(ccount54);

                    }


                }





            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
            comboBox10.Text = "";
            comboBox11.Text = "";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {



                PrintJbsaDataGridView.Print_Grid(dataGridView4);



            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            
            
            int x = Convert.ToInt32(textBox29.Text);
            
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
            foreach (DataGridViewRow dgv in dataGridView2.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");

            
            print_data_gridviwe p_bill = new print_data_gridviwe();
            p_bill.SetDataSource(ds);
            p_bill.SetParameterValue("bill_id", x);
            p_bill.SetParameterValue("total_cost_bill", textBox30.Text);
            p_bill.SetParameterValue("balance", customer_balance.Text);
            p_bill.SetParameterValue("discount", textBox48.Text);
            p_bill.SetParameterValue("cost_in", textBox31.Text);
            p_bill.SetParameterValue("date_bill", dataGridView2.Rows[0].Cells[1].Value);
            f_print_bill f = new f_print_bill();

            f.crystalReportViewer1.ReportSource = p_bill;
            f.crystalReportViewer1.Refresh();
            f.ShowDialog();


            
           

        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                dataGridView5.Rows.Clear();

                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * from bill where (bill_date2 = @bill_date2)", con);
                SqlParameter p = new SqlParameter("@bill_date2",textBox39.Text);
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    errorProvider1.SetError(button24, "عذراً لا يوجد بيانات للبحث عنها");
                }
                else
                {
                    while (myreader.Read())
                    {
                        dataGridView5.Rows.Add(Convert.ToString(myreader[4]), Convert.ToString(myreader[3]), Convert.ToString(myreader[2]), Convert.ToString(myreader[1]), Convert.ToString(myreader[0]));

                    }
                }

                SqlConnection con1 = new SqlConnection(Class1.x);
                con1.Open();
                SqlCommand com1 = new SqlCommand("SELECT  SUM(total_cost_bill) AS Expr1 from bill where (bill_date2 = @bill_date2)", con1);
                SqlParameter p1 = new SqlParameter("@bill_date2", textBox39.Text);
                com1.CommandType = CommandType.Text;
                com1.Parameters.Add(p1);
                SqlDataReader myreader1 = com1.ExecuteReader();
                if (myreader1.HasRows == false)
                {
                    errorProvider1.SetError(button17, "عذراً لا يوجد بيانات للبحث عنها");
                }
                else
                {
                    while (myreader1.Read())
                    {
                        textBox40.Text = Convert.ToString(myreader1[0]);

                    }
                }
                timer2.Enabled = true;


            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox39.Text = Convert.ToString(dateTimePicker3.Value.Month + "/" + dateTimePicker3.Value.Year);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {



                PrintJbsaDataGridView.Print_Grid(dataGridView5);



            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Class1.item_no = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString());
            //Class1.item_box_pice = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
            Class1.total_cost_item = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
            //Class1.bill_details_id = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            

        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{

            //    timer2.Enabled = false;
            //    progressBar1.Increment(100);
            //    errorProvider1.Clear();




            //    if ((Convert.ToString(MessageBox.Show("هل أنت متأكد من الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes"))
            //    {
            //        SqlConnection con = new SqlConnection(Class1.x);
            //        con.Open();
            //        SqlCommand com = new SqlCommand();
            //        com.CommandText = "DELETE FROM bill_details WHERE (bill_details_id = @bill_details_id)";
            //        com.Connection = con;
            //        com.CommandType = CommandType.Text;
            //        SqlParameter p1 = new SqlParameter("@bill_details_id", Convert.ToInt32(Class1.bill_details_id));
            //        com.Parameters.Add(p1);
            //        SqlDataReader reader;
            //        reader = com.ExecuteReader();
            //        reader.Close();


            //        SqlConnection mycon11 = new SqlConnection(Class1.x);
            //        mycon11.Open();
            //        SqlCommand mycom11 = new SqlCommand("UPDATE items SET item_box_pice = item_box_pice + @item_box_pice WHERE (item_no = @item_no) ", mycon11);
            //        SqlParameter p13 = new SqlParameter("@item_no", Convert.ToString(Class1.item_no));
            //        SqlParameter p14 = new SqlParameter("@item_box_pice", Convert.ToInt32(Class1.item_box_pice));
            //        mycom11.CommandType = CommandType.Text;
            //        mycom11.Parameters.Add(p13);
            //        mycom11.Parameters.Add(p14);
            //        SqlDataReader myreader11 = mycom11.ExecuteReader();
            //        mycon11.Close();

            //        int x = Convert.ToInt32(textBox30.Text);
            //        int y = Convert.ToInt32(Class1.total_cost_item);
            //        int z = x - y;
            //        textBox30.Text = Convert.ToString(z);
            //        textBox32.Text = Convert.ToString(z);



            //    }




            //    timer2.Enabled = true;


            //    MessageBox.Show("تم الحذف بنجاح");
            //}

            //catch
            //{
            //    MessageBox.Show("الرجاء الانتباه");
            //}
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            balance_all_customers f = new balance_all_customers();
            f.ShowDialog();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            textBox25.Text = Convert.ToString(dateTimePicker4.Value.Day + "/" + dateTimePicker4.Value.Month + "/" + dateTimePicker4.Value.Year);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            
            try
            {

                

                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                dataGridView6.Rows.Clear();
                textBox43.Text = "";
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("SELECT custmer_name , cost_in from cost where (bill_date2 = @bill_date2) AND (cost_in > 0)", con);
                SqlParameter p = new SqlParameter("@bill_date2", textBox25.Text);
                com.CommandType = CommandType.Text;
                com.Parameters.Add(p);
                SqlDataReader myreader = com.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    errorProvider1.SetError(button26, "تنبيه لا يوجد اليوم أي دفعات واردة");
                }
                else
                {
                    while (myreader.Read())
                    {
                        dataGridView6.Rows.Add(Convert.ToString(myreader[0]), Convert.ToString(myreader[1]));

                    }
                }

                SqlConnection con1 = new SqlConnection(Class1.x);
                con1.Open();
                SqlCommand com1 = new SqlCommand("SELECT  SUM(cost_in) AS Expr1 from cost where (bill_date2 = @bill_date2)", con1);
                SqlParameter p1 = new SqlParameter("@bill_date2", textBox25.Text);
                com1.CommandType = CommandType.Text;
                com1.Parameters.Add(p1);
                SqlDataReader myreader1 = com1.ExecuteReader();
                if (myreader1.HasRows == false)
                {
                    errorProvider1.SetError(button26, "تنبيه لا يوجد اليوم أي دفعات واردة");
                }
                else
                {
                    while (myreader1.Read())
                    {
                        textBox41.Text = Convert.ToString(myreader1[0]);

                    }


                     SqlConnection con3 = new SqlConnection(Class1.x);
                    con3.Open();
                    SqlCommand com3 = new SqlCommand("SELECT  SUM(amount) AS Expr1 , COUNT(amount) AS Expr2 from exports where (amount_date = @amount_date)", con3);
                    SqlParameter p3 = new SqlParameter("@amount_date", textBox25.Text);
                    com3.CommandType = CommandType.Text;
                    com3.Parameters.Add(p3);
                    SqlDataReader myreader3 = com3.ExecuteReader();
                    if (myreader3.HasRows == false)
                    {
                        errorProvider1.SetError(button26, "تنبيه لا يوجد اليوم مصاريف");
                    }
                    else
                    {
                        while (myreader3.Read())
                        {
                            textBox42.Text = Convert.ToString(myreader3[0]);
                            count_amount.Text = Convert.ToString( myreader3[1]);

                        }
                    }



                    SqlConnection con2 = new SqlConnection(Class1.x);
                    con2.Open();
                    SqlCommand com2 = new SqlCommand("SELECT other , amount from exports where (amount_date = @amount_date)", con2);
                    SqlParameter p2 = new SqlParameter("@amount_date", textBox25.Text);
                    com2.CommandType = CommandType.Text;
                    com2.Parameters.Add(p2);
                    SqlDataReader myreader2 = com2.ExecuteReader();
                    if (myreader2.HasRows == false)
                    {
                        errorProvider1.SetError(button26, "تنبيه لا يوجد اليوم مصاريف");
                    }
                    else
                    {

                        for (int i = 0; i < Convert.ToInt32(count_amount.Text); i++)
                        {
                            dataGridView6.Rows.Add("", "");
                            myreader2.Read();
                            dataGridView6.Rows[i].Cells[2].Value = Convert.ToString(myreader2[0]);
                            dataGridView6.Rows[i].Cells[3].Value = Convert.ToString(myreader2[1]);
                            
                           
                        }

                    }

                   


                    if (textBox41.Text == "")
                    {
                        textBox41.Text = Convert.ToString(0);
                        
                    }
                    else if (textBox42.Text == "")
                    {
                        textBox42.Text = Convert.ToString(0);
                        
                    }

                }        
                timer2.Enabled = true;


            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            textBox46.Text = Convert.ToString(dateTimePicker5.Value.Day + "/" + dateTimePicker5.Value.Month + "/" + dateTimePicker5.Value.Year);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                SqlConnection mycon11 = new SqlConnection(Class1.x);
                mycon11.Open();
                SqlCommand mycom11 = new SqlCommand("INSERT INTO exports  ( amount, amount_date, other )VALUES (@amount,@amount_date,@other )", mycon11);

                SqlParameter p3 = new SqlParameter("@amount", textBox45.Text);
                SqlParameter p4 = new SqlParameter("@amount_date", textBox46.Text);
                SqlParameter p5 = new SqlParameter("@other", textBox44.Text);
                mycom11.CommandType = CommandType.Text;
                mycom11.Parameters.Add(p3);
                mycom11.Parameters.Add(p4);
                mycom11.Parameters.Add(p5);
                SqlDataReader myreader11 = mycom11.ExecuteReader();
                mycon11.Close();
                toolTip1.Show("تم الحفظ بنجاح", button27);
            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }

            timer2.Enabled = true;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
            dataGridView8.Rows.Clear();
            SqlConnection con2 = new SqlConnection(Class1.x);
            con2.Open();
            SqlCommand com2 = new SqlCommand("SELECT other , amount ,exports_id from exports where (amount_date = @amount_date)", con2);
            SqlParameter p2 = new SqlParameter("@amount_date", textBox47.Text);
            com2.CommandType = CommandType.Text;
            com2.Parameters.Add(p2);
            SqlDataReader myreader2 = com2.ExecuteReader();
            if (myreader2.HasRows == false)
            {
                errorProvider1.SetError(button28, "عذراً لا يوجد بيانات للبحث عنها");
            }
            else
            {
                while (myreader2.Read())
                {
                    dataGridView8.Rows.Add(Convert.ToString(myreader2[0]), Convert.ToString(myreader2[1]), Convert.ToString(myreader2[2]));

                }
            }
            timer2.Enabled = true;

            

            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            textBox47.Text = Convert.ToString(dateTimePicker6.Value.Day + "/" + dateTimePicker6.Value.Month + "/" + dateTimePicker6.Value.Year);
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView8_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد الحذف", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                try
                {

                    SqlConnection mycon = new SqlConnection(Class1.x);
                    mycon.Open();
                    SqlCommand mycom = new SqlCommand("delete from exports where (exports_id=@exports_id)", mycon);
                    SqlParameter p = new SqlParameter("@exports_id", Convert.ToInt32(dataGridView8.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    mycom.CommandType = CommandType.Text;
                    mycom.Parameters.Add(p);
                    SqlDataReader myreader = mycom.ExecuteReader();
                    myreader.Close();
                    dataGridView8.Rows.Clear();
                }

                    
                catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه");  }
                timer2.Enabled = true;


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
            try
            {

                SqlConnection mycon = new SqlConnection(Class1.x);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("delete from container where (container_id=@container_id)", mycon);
                SqlParameter p = new SqlParameter("@container_id", Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
                mycom.CommandType = CommandType.Text;
                mycom.Parameters.Add(p);
                SqlDataReader myreader = mycom.ExecuteReader();
                myreader.Close();
                dataGridView1.Rows.Clear();
            }


            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            
                
                int x = Convert.ToInt32(textBox41.Text);
                int y = Convert.ToInt32(textBox42.Text);
                int z = x - y;
                textBox43.Text = Convert.ToString(z);
            
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {



                PrintJbsaDataGridView.Print_Grid(dataGridView6);




            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button30_Click_1(object sender, EventArgs e)
        {            
            
            //SqlConnection mycon3 = new SqlConnection(Class1.x);
            //mycon3.Open();
            //SqlCommand mycom3 = new SqlCommand("select  custmer_name  from custmers  ", mycon3);
            //mycom3.CommandType = CommandType.Text;
            //SqlDataAdapter da = new SqlDataAdapter(mycom3);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "pp");
            //dataGridView8.DataSource = ds.Tables[0];            

        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox48_TextChanged_1(object sender, EventArgs e)
        {
            try
            {

                if (textBox30.Text == "")
                {
                    textBox30.Text = Convert.ToString(0);

                }
                else
                {
                    try
                    {
                        int x = Convert.ToInt32(textBox49.Text);
                        int y = Convert.ToInt32(textBox48.Text);
                        int z = x - y;
                        textBox30.Text = Convert.ToString(z);
                    }
                    catch
                    {
                        MessageBox.Show("الرجاء ادخال الصيغة الصحيحة للحسم");
                        textBox48.Text = "0";
                    }
                }

            }

            catch
            {
                MessageBox.Show("الرجاء ادخال الصيغة الصحيحة للحسم");
                textBox23.Text = "";
            }

            
        }

        private void button30_Click_2(object sender, EventArgs e)
        {
            search_item f = new search_item();
            f.ShowDialog();

        }

        private void button31_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox29.Text);
            x = x - 1;
            SqlConnection mycon3 = new SqlConnection(Class1.x);
            mycon3.Open();
            SqlCommand mycom3 = new SqlCommand("select bill_id, custmer_name, item_id, item_price, item_piece, total_cost_item, item_name , item_box  from  bill_details   ", mycon3);
            mycom3.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(mycom3);
            DataSet ds = new DataSet();
            da.Fill(ds, "pp");
            mycon3.Close();
            print_data_gridviwe p_bill = new print_data_gridviwe();
            p_bill.SetDataSource(ds.Tables[0]);
            p_bill.SetParameterValue("bill_id", x);
            p_bill.SetParameterValue("total_cost_bill", textBox30.Text);
            p_bill.SetParameterValue("balance", textBox34.Text);
            p_bill.SetParameterValue("discount", textBox48.Text);
            p_bill.SetParameterValue("cost_in", textBox31.Text);
            f_print_bill f = new f_print_bill();
            f.crystalReportViewer1.Refresh();
            f.crystalReportViewer1.ReportSource = p_bill;
            f.crystalReportViewer1.Refresh();
            f.ShowDialog();
        }


        

        private void button33_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            textBox30.Text = "0";
            textBox31.Text = "0";
            textBox32.Text = "0";
            textBox48.Text = "0";
            textBox49.Text = "0";
            textBox33.Text = "لا يوجد";
            customer_balance.Text = "0";
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int x = Convert.ToInt32(textBox30.Text);
            int y = Convert.ToInt32(Class1.total_cost_item);
            int z = x - y;
            textBox30.Text = Convert.ToString(z);
            textBox32.Text = Convert.ToString(z);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DataTable table1 = new DataTable("cost");
            table1.Columns.Add("name_customer");
            table1.Columns.Add("cost_in");
            table1.Columns.Add("name_export");
            table1.Columns.Add("amount");

            foreach (DataGridViewRow dgv in dataGridView6.Rows)
            {
                table1.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value);
            }

            // Create a DataSet and put both tables in it.
            DataSet set = new DataSet("box");
            set.Tables.Add(table1);

            set.WriteXmlSchema("Sample.xml");


            print_box print_box1 = new print_box();
            print_box1.SetDataSource(set);
            print_box1.SetParameterValue("total_all", textBox43.Text);
            print_box1.SetParameterValue("total_export", textBox42.Text);
            print_box1.SetParameterValue("total_cost", textBox41.Text);
            print_box1.SetParameterValue("date_box", textBox25.Text);
            
            f_print_bill f = new f_print_bill();

            f.crystalReportViewer1.ReportSource = print_box1;
            f.crystalReportViewer1.Refresh();
            f.ShowDialog();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد الحذف", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                try
                {

                    SqlConnection mycon = new SqlConnection(Class1.x);
                    mycon.Open();
                    SqlCommand mycom = new SqlCommand("delete from container where (container_id=@container_id)", mycon);
                    SqlParameter p = new SqlParameter("@container_id", Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    mycom.CommandType = CommandType.Text;
                    mycom.Parameters.Add(p);
                    SqlDataReader myreader = mycom.ExecuteReader();
                    myreader.Close();
                    dataGridView8.Rows.Clear();
                }


                catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
                timer2.Enabled = true;


            }
        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            try
            {



                PrintJbsaDataGridView.Print_Grid(dataGridView3);



            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            comboBox13.Items.Clear();
            SqlConnection con1 = new SqlConnection(Class1.x);
            con1.Open();
            SqlCommand com1 = new SqlCommand("select item_price_one , item_price_all , item_price  from items where item_no = @item_no  ", con1);
            SqlParameter p1 = new SqlParameter("@item_no", Convert.ToString(comboBox14.Text));
            com1.CommandType = CommandType.Text;
            com1.Parameters.Add(p1);
            SqlDataReader reder1 = com1.ExecuteReader();
            while (reder1.Read())
            {
                comboBox13.Items.Add(reder1[0]);
                comboBox13.Items.Add(reder1[1]);
                comboBox13.Items.Add(reder1[2]);

            }
            con1.Close();
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            textBox56.Text = Convert.ToString(dateTimePicker7.Value.Day + "/" + dateTimePicker7.Value.Month + "/" + dateTimePicker7.Value.Year);
        }

        private void textBox57_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox57.Text == "" || textBox57.Text == Convert.ToString(0))
                {
                    textBox57.Text = Convert.ToString(0);

                }
                else
                {
                    int x = Convert.ToInt32(textBox57.Text);
                    int y = Convert.ToInt32(comboBox13.Text);
                    int z = x * y;
                    textBox55.Text = Convert.ToString(z);
                   
                }
            }
            catch
            {
                MessageBox.Show(" الرجاء ادخال الصيغة الصحيحة لعدد الأزواج");
                textBox57.Text = "";
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {

                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();

                if (comboBox13.Text == "" || comboBox14.Text == "" || textBox55.Text == "" || textBox56.Text == "" || comboBox15.Text == "" || textBox57.Text == "" || textBox58.Text == "")

                { errorProvider1.SetError(button32, "عذراً يجب عدم ترك حقل فارغ"); }
                else
                {
                    int x = Convert.ToInt32(textBox55.Text);
                    int y = Convert.ToInt32(textBox58.Text);
                    int z = x + y;
                    textBox58.Text = Convert.ToString(z);
                    dataGridView7.Rows.Add(comboBox15.Text, comboBox13.Text, Convert.ToString(textBox57.Text), comboBox14.Text, Convert.ToString(textBox56.Text), Convert.ToString(textBox55.Text));
                    toolTip1.Show("تمت الإضافة بنجاح", button32);

                }

            }
            catch
            { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }


            finally
            {
                comboBox14.Text = "";
                comboBox13.Text = "";
                textBox55.Text = "";
                textBox57.Text = "";
                timer2.Enabled = true;
            }
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Class1.total_cost_item = Convert.ToString(dataGridView7.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void dataGridView7_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int x = Convert.ToInt32(textBox58.Text);
            int y = Convert.ToInt32(Class1.total_cost_item);
            int z = x - y;
            textBox58.Text = Convert.ToString(z);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            dataGridView7.Rows.Clear();
            textBox58.Text = "";
            timer2.Enabled = true;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (textBox58.Text == "")
                { errorProvider1.SetError(button35, "عذراً يجب عدم ترك حقل فارغ"); }
                else
                {


                    for (int i = 0; i < dataGridView7.Rows.Count; i++)
                    {

                        SqlConnection mycon11 = new SqlConnection(Class1.x);
                        mycon11.Open();
                        SqlCommand mycom11 = new SqlCommand("UPDATE items SET item_box_pice = item_box_pice + @item_box_pice WHERE (item_no = @item_no) ", mycon11);
                        SqlParameter p13 = new SqlParameter("@item_no", Convert.ToString(dataGridView7.Rows[i].Cells[3].Value));
                        SqlParameter p14 = new SqlParameter("@item_box_pice", Convert.ToInt64(dataGridView7.Rows[i].Cells[2].Value));
                        mycom11.CommandType = CommandType.Text;
                        mycom11.Parameters.Add(p13);
                        mycom11.Parameters.Add(p14);
                        SqlDataReader myreader11 = mycom11.ExecuteReader();
                        mycon11.Close();

                    }

                    SqlConnection mycon111 = new SqlConnection(Class1.x);
                    mycon111.Open();
                    SqlCommand mycom111 = new SqlCommand("INSERT INTO exports  ( amount, amount_date, other )VALUES (@amount,@amount_date,@other )", mycon111);

                    SqlParameter p3 = new SqlParameter("@amount", textBox58.Text);
                    SqlParameter p4 = new SqlParameter("@amount_date", textBox56.Text);
                    SqlParameter p5 = new SqlParameter("@other", "مرتجع" + " " + comboBox15.Text);
                    mycom111.CommandType = CommandType.Text;
                    mycom111.Parameters.Add(p3);
                    mycom111.Parameters.Add(p4);
                    mycom111.Parameters.Add(p5);
                    SqlDataReader myreader111 = mycom111.ExecuteReader();
                    mycon111.Close();

                    SqlConnection mycon1113 = new SqlConnection(Class1.x);
                    mycon1113.Open();
                    SqlCommand mycom1113 = new SqlCommand(" UPDATE  custmers SET  balance = balance - @balance where custmer_name = @custmer_name ", mycon1113);
                    SqlParameter p1113 = new SqlParameter("@custmer_name", comboBox15.Text);
                    SqlParameter p141 = new SqlParameter("@balance", Convert.ToInt64(textBox58.Text));
                    mycom1113.Parameters.Add(p1113);
                    mycom1113.Parameters.Add(p141);
                    SqlDataReader myreader1113 = mycom1113.ExecuteReader();

                    mycon1113.Close();
                }


                toolTip1.Show("تمت الإضافة بنجاح", button35);

            }
            catch { MessageBox.Show("حدث خطأ الرجاء الأنتباه"); }
            timer2.Enabled = true;
        }

     

      

      

    }
}
    

