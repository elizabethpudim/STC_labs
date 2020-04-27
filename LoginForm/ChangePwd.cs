using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;



namespace LoginForm
{
    public partial class ChangePwd : Form
    {
        public ChangePwd()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ADMIN\SOURCE\REPOS\LOGINFORM\DB\LOGINDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = "Select * from tblLogin Where username = '" + Login.username + "'and password = '" + txtOldPwd.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
             
            if (dtbl.Rows.Count == 1) //старый пароль указан верно
            {
                string opt = dtbl.Rows[0]["Opt"].ToString();
                string newPwd = txtNewPwd.Text.Trim();

                if (txtNewPwd.Text == txtConf.Text) //confirm и new pwd совпадают
                {
                    if (opt == "True") //если есть ограничение на пароль
                    {
                        if (newPwd.Any(char.IsUpper) && newPwd.Any(char.IsLower) && newPwd.IndexOfAny("+-*/%".ToCharArray()) != -1) //строчные, прописные и арифметические знаки
                        {
                            sqlcon.Open();
                            string query1 = "UPDATE tblLogin SET password ='" + txtNewPwd.Text.Trim() + "'WHERE username = '" + Login.username + "'";
                            SqlCommand sqlcom = new SqlCommand(query1, sqlcon);
                            sqlcom.ExecuteNonQuery();
                            MessageBox.Show("Password successfully changed!");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Your password should contain uppercase, lowercase and arithmetic symbols");
                        }
                    }
                    else
                    {
                        sqlcon.Open();
                        string query1 = "UPDATE tblLogin SET password ='" + txtNewPwd.Text.Trim() + "'WHERE username = '" + Login.username + "'";
                        SqlCommand sqlcom = new SqlCommand(query1, sqlcon);
                        sqlcom.ExecuteNonQuery();
                        MessageBox.Show("Password successfully changed!");
                        sqlcon.Close();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Passwords don't match");
                }


            }
            else
            {
                MessageBox.Show("Incorrect Password");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
