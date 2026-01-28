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
            // طريقة ربط اصدار 1
            // مستقبلاً سنربط هذا الجزء بقاعدة بيانات أو بالـ GitHub
            /*if (txtUser.Text == "admin" && txtPass.Text == "123")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("خطأ في بيانات الدخول!");
            }*/
            // طريقة ربط جديدة اصدار 2
            string user = txtUser.Text;
            string pass = txtPass.Text;

            // استدعاء محرك الباكتات الذي أنشأناه
            PacketManager pm = new PacketManager();

            // محاكاة إرسال باكت الدخول (هنا يبدأ شغل الـ Silkroad الحقيقي)
            byte[] loginPacket = pm.CreateLoginPacket(user, pass);

            if (loginPacket != null)
            {
                // إذا نجحت العملية، نفتح واجهة البوت الرئيسية
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("فشل في تجهيز بيانات الدخول!");
            }
            PacketManager pm = new PacketManager();
            pm.StartProxy(15779); // 15779 هو البورت الافتراضي لسيلكرود
        }
    }
}
