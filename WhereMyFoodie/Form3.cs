using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace WhereMyFoodie
{
    public partial class Form3 : Form
    {

        #region Variables
        string host = "127.0.0.1";
        string database = "db_food";
        string user = "root";
        string password = "";
        string connectionString;
        string sql;
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private DataSet dst;
        #endregion

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            connectionString = "Data source=" + host + "; Database=" + database + "; User ID=" + user + "; Password=" + password + "; Charset=utf8; AllowUserVariables=true";
            conn = new MySqlConnection(connectionString);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Hi There!");
            }
        }
        private bool isEmpty()
        {
            bool empty = false;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                empty = true;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                empty = true;
            }
            return empty;
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            sql = "select * from accounts where accountUsername like '" + txtUsername.Text + "'";
            cmd = new MySqlCommand(sql, conn);
            adapter = new MySqlDataAdapter(cmd);
            dst = new DataSet();
            adapter.Fill(dst, "accounts");

            if (dst.Tables["accounts"].Rows.Count == 0)
            {
                MessageBox.Show("Username Not Found");
            }
            else
            {
                sql = "select * from accounts where accountPassword like '" + txtPassword.Text + "'";
                cmd = new MySqlCommand(sql, conn);
                adapter = new MySqlDataAdapter(cmd);
                dst = new DataSet();
                adapter.Fill(dst, "accounts");

                if (dst.Tables["accounts"].Rows.Count == 0)
                {
                    MessageBox.Show("Password Wrong");
                }
                else
                {
                    Form1 adminForm = new Form1();
                    adminForm.Show();
                }

            }
        }
    }
}
