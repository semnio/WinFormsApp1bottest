using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            // التأكد من أن المستخدم أدخل بيانات
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("من فضلك أدخل اسم المستخدم وكلمة المرور");
                return;
            }
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
            string username = txtUser.Text;
            string password = txtPass.Text;
            if (username == "admin" && password == "123")
            {
                MessageBox.Show("تم تسجيل الدخول بنجاح! جاري تشغيل البروكسي...");
                return;
            }

            // استدعاء محرك الباكتات الذي أنشأناه
            PacketManager pm = new PacketManager();
            pm.StartProxy(15779); // 15779 هو البورت الافتراضي لسيلكرود

            // محاكاة إرسال باكت الدخول (هنا يبدأ شغل الـ Silkroad الحقيقي)
            byte[] loginPacket = pm.CreateLoginPacket(username, password);

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
            //PacketManager pm = new PacketManager(); لانها موجودة فوق سطر 36 
        }
    }
}
