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

namespace LoginForm
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string username = "";

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        int numTries = 0;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ADMIN\SOURCE\REPOS\LOGINFORM\DB\LOGINDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query1 = "Select * from tblLogin Where username = '" + txtUsername.Text.Trim() + "'";
            string query = "Select * from tblLogin Where username = '" + txtUsername.Text.Trim() + "'and password = '" + txtPassword.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            SqlDataAdapter sda1 = new SqlDataAdapter(query1, sqlcon);
            DataTable dtbl = new DataTable();
            DataTable dtbl1 = new DataTable();
            sda.Fill(dtbl);
            sda1.Fill(dtbl1);
            if ((dtbl.Rows.Count == 1) && (dtbl1.Rows.Count == 1) && (dtbl.Rows[0]["isAdmin"].ToString() == "True") && (dtbl.Rows[0]["Block"].ToString() != "True"))
            {
                username = txtUsername.Text.Trim();
                Admin obj = new Admin();
                this.Hide();
                obj.Show();
            }
            else if ((dtbl.Rows.Count == 1) && (dtbl1.Rows.Count == 1) && (dtbl.Rows[0]["isAdmin"].ToString() == "False") && (dtbl.Rows[0]["Block"].ToString() != "True"))
            {
                username = txtUsername.Text.Trim();
                User obj = new User();
                this.Hide();
                obj.Show();
            }
            else if ((dtbl.Rows.Count == 1) && (dtbl.Rows[0]["Block"].ToString() == "True"))
            {
                MessageBox.Show("You are blocked");
            }
            else if (dtbl1.Rows.Count == 1)
            {
                numTries++;
                MessageBox.Show("Wrong password. Left: " + (3 - numTries) + " tries");
                if (numTries == 3)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Username doesn't exist.");
            }

        }
         
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
