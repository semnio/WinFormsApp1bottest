namespace WinFormsApp1bottest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // استقبال البيانات من البروكسي وعرضها
            PacketManager.OnPacketReceived += (message) => {
                this.Invoke(new MethodInvoker(() => {
                    lstLog.Items.Insert(0, message); // إضافة أحدث باكت في الأعلى
                }));
            };
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void procetinsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void SaveGamePathToSettings(string path)
        {
            // هذا الكود سيقوم بتحديث ملف JSON لكي يقرأه البوت عند التشغيل
            string jsonPath = "إعدادات_متقدمة.json";

            // ملاحظة: يمكنك استخدام مكتبة Newtonsoft.Json لتعديل الملف بسهولة
            // أو ببساطة تخزين المسار في ملف نصي بسيط يقرأه البوت
            File.WriteAllText(jsonPath, $"{{ \"GamePath\": \"{path.Replace("\\", "\\\\")}\" }}");
            try
            {
                // تجهيز النص الذي سيتم حفظه بصيغة JSON
                // نستخدم @ قبل المسار للتعامل مع العلامات المائلة \ بشكل صحيح
                string jsonContent = "{\n  \"game_path\": \"" + path.Replace("\\", "\\\\") + "\"\n}";

                // حفظ الملف في نفس مجلد البوت
                File.WriteAllText("settings.json", jsonContent);

                // إضافة رسالة في الـ Logs الخاصة بواجهتك
                lstLog.Items.Add("[" + DateTime.Now.ToString("HH:mm") + "] تم حفظ مسار اللعبة بنجاح.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في حفظ الإعدادات: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*// تحديد نوع الملفات التي تظهر (مثلاً ملف تشغيل اللعبة sro_client.exe)
            openFileDialog1.Filter = "SRO Client|sro_client.exe|Executable Files (*.exe)|*.exe";
            openFileDialog1.Title = "Select Silkroad Game Executable";

            // فتح النافذة والتأكد أن المستخدم اختار ملفاً
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // تخزين المسار في نص أو متغير
                string gamePath = openFileDialog1.FileName;

                // عرض المسار في صندوق نص (TextBox) إذا كان لديك واحد للإعدادات
                textBox4.Text = gamePath;

                // إرسال رسالة تأكيد في السجلات (Logs)
                MessageBox.Show("تم ربط البوت بملف اللعبة بنجاح!");
            } */
            // إعداد عنوان النافذة
            folderBrowserDialog1.Description = "اختر مجلد لعبة سيلك رود الرئيسي";
            folderBrowserDialog1.ShowNewFolderButton = false; // لمنع المستخدم من إنشاء مجلدات جديدة

            // فتح النافذة
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                // الحصول على المسار الذي اختاره المستخدم
                string sroPath = folderBrowserDialog1.SelectedPath;

                // تحديث النص في الواجهة (للتأكد من اختيار المجلد الصحيح)
                textBox4.Text = sroPath;

                // إرسال المسار إلى ملف الإعدادات الخاص بالبوت
                SaveGamePathToSettings(sroPath);

                MessageBox.Show("تم تحديد مجلد اللعبة بنجاح: " + sroPath);
            }
        }

        private void listBoxLogs1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
