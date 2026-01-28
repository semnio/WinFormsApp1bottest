using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1bottest
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // مستقبلاً سنربط هذا الجزء بقاعدة بيانات أو بالـ GitHub
            if (txtUser.Text == "admin" && txtPass.Text == "123")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("خطأ في بيانات الدخول!");
            }
        }
    }
}
