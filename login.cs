using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Management;

namespace montaser
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (user_name_txt.Text == "" || password_txt.Text == "")
            {


                errorProvider1.SetError(user_name_txt, "يجب ادخال اسم المستخدم");
                errorProvider1.SetError(password_txt, "يجب ادخال كلمة المرور");


            }
            else
            {
                SqlConnection mycon = new SqlConnection(Class1.x);
                mycon.Open();
                SqlCommand mycom2 = new SqlCommand("select *  from login where (user_name = @user) and (password = @pass) and (flsh_num = @flsh_num) ", mycon);
                SqlParameter p1 = new SqlParameter("@user", user_name_txt.Text);
                SqlParameter p2 = new SqlParameter("@pass", password_txt.Text);
                SqlParameter p3 = new SqlParameter("@flsh_num",Convert.ToInt32( Class1.flash_num));
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p1);
                mycom2.Parameters.Add(p2);
                mycom2.Parameters.Add(p3);
                SqlDataReader myreader = mycom2.ExecuteReader();
                if (myreader.HasRows == false)
                {


                    errorProvider1.SetError(button1, "اسم المستخدم او كلمة المرور خاطئة الرجاء التأكد");

                    user_name_txt.Text = "";
                    password_txt.Text = "";
                }
                else
                {
                    menu f = new menu();
               
                    user_name_txt.Text = "";
                    password_txt.Text = "";
                    f.ShowDialog();
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {
            
            ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            foreach (ManagementObject currentObject in theSearcher.Get())
            {
                ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                Class1.flash_num = theSerialNumberObjectQuery["SerialNumber"].ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


           
            menu f = new menu();
            f.ShowDialog();

        }

    
    }
}
