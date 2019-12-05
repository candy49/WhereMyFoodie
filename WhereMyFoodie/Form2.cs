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
    public partial class Form2 : Form
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
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connectionString = "Data source=" + host + "; Database=" + database + "; User ID=" + user + "; Password=" + password + "; Charset=utf8; AllowUserVariables=true";
            conn = new MySqlConnection(connectionString);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Welcome User");
            }
        }

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(comboType.SelectedItem);
            //dgvFoodUser.Rows.Clear();

            if (comboType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select search category");
                return;
            }

            if (comboType.SelectedItem == "id")
            {
                var isNumeric = int.TryParse(txtSearchUser.Text, out int n);
                if (!isNumeric)
                {
                    MessageBox.Show("Invalid ID");
                    return;
                }
                sql = "select * from foods where foodId like '" + Convert.ToInt32(txtSearchUser.Text) + "'";
            } else if (comboType.SelectedItem == "name")
            {
                sql = "select * from foods where foodName like '" + txtSearchUser.Text + "'";
            } else if (comboType.SelectedItem == "source")
            {
                sql = "select * from foods where foodSource like '" + txtSearchUser.Text + "'";
            } else if (comboType.SelectedItem == "place")
            {
                sql = "select * from foods where foodPlace like '" + txtSearchUser.Text + "'";
            } else
            {
                MessageBox.Show("Invalid Category");
                return;
            }

            cmd = new MySqlCommand(sql, conn);
            adapter = new MySqlDataAdapter(cmd);
            dst = new DataSet();
            adapter.Fill(dst, "foods");

            Debug.WriteLine(dst.Tables["foods"].Rows.Count);

            if (dst.Tables["foods"].Rows.Count == 0)
            {
                MessageBox.Show(txtSearchUser.Text + " not found in " + comboType.SelectedItem);
            }

            for (int i = 0; i < dst.Tables["foods"].Rows.Count; i++)
            {
                dgvFoodUser.Rows.Add(1);
                dgvFoodUser.Rows[i].Cells[0].Value = dst.Tables["foods"].Rows[i][0].ToString();
                dgvFoodUser.Rows[i].Cells[1].Value = dst.Tables["foods"].Rows[i][1].ToString();
                dgvFoodUser.Rows[i].Cells[2].Value = dst.Tables["foods"].Rows[i][2].ToString();
                dgvFoodUser.Rows[i].Cells[3].Value = dst.Tables["foods"].Rows[i][3].ToString();
                dgvFoodUser.Rows[i].Cells[4].Value = dst.Tables["foods"].Rows[i][4].ToString();
            }
        }

        private void btnGoToAdmin_Click_1(object sender, EventArgs e)
        {
            Form3 adminForm = new Form3();
            adminForm.Show();
        }
    }
}
