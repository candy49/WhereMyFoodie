﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereMyFoodie
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            //test
            Form2 userForm = new Form2();
            userForm.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Form3 adminForm = new Form3();
            adminForm.Show();
        }
    }
}
