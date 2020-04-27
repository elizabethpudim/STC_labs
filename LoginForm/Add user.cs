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
    public partial class Add_user : Form
    {
        public Add_user()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txtAdd.Text;
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ADMIN\SOURCE\REPOS\LOGINFORM\DB\LOGINDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using (sqlcon)
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblLogin WHERE username = '" + username + "'", sqlcon);
                DataTable dtb = new DataTable();
                sda.Fill(dtb);
                if (dtb.Rows.Count == 0)
                {
                    SqlCommand sqlcom = new SqlCommand("INSERT INTO tblLogin (username, password) VALUES ('" + username + "', '')", sqlcon);
                    sqlcom.ExecuteNonQuery();
                    MessageBox.Show("User successfully added");
                }
                else
                {
                    MessageBox.Show("Username already exists");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
