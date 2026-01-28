namespace WinFormsApp1bottest
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        // نجعل الـ PacketManager متاحاً لكل المشروع
        public static PacketManager GlobalPacketManager = new PacketManager();
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
            Application.EnableVisualStyles(); 
            //Application.SetCompatibleTextRenderingDefault(false);

            // نفتح شاشة اللوجن أولاً
            LoginForm login = new LoginForm();

            if (login.ShowDialog() == DialogResult.OK)
            {
                // إذا نجح اللوجن، نفتح واجهة البوت الرئيسية
                GlobalPacketManager.StartProxy(15779);
                Application.Run(new Form1());
            }
        }
    }
}