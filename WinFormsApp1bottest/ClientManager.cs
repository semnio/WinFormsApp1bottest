using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinFormsApp1bottest
{
    public class ClientManager
    {
        // استيراد دوال الويندوز للتحكم في ذاكرة العمليات
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        public void LaunchAndPatch(string clientPath, string ipToReplace)
        {
            try
            {
                // 1. تشغيل اللعبة
                Process sroClient = Process.Start(clientPath, "0 /22 0 0"); // بارامترات التشغيل القياسية

                // 2. هنا سنضع كود البحث عن الـ IP القديم واستبداله بـ 127.0.0.1
                // ننتظر قليلاً حتى يتم تحميل البيانات في الرام
                System.Threading.Thread.Sleep(2000);
                // سنحتاج لعنوان الذاكرة (Offset) الخاص بالسيرفر الذي تلعب عليه
                //Console.WriteLine("تم تشغيل اللعبة وجاري حقن الـ IP...");
                // 2. تحويل الـ Localhost إلى مصفوفة بايتات لرسلها للرام
                byte[] redirectIp = System.Text.Encoding.ASCII.GetBytes("127.0.0.1");

                // 3. العناوين (Offsets) تختلف من نسخة لأخرى (مثلاً v1.188 تختلف عن v1.200)
                // هذا مثال لعنوان مشهور في نسخ الـ VSRO:
                IntPtr ipAddressLocation = (IntPtr)0x00B0D123; // هذا الرقم مجرد مثال ستحتاج لتغييره حسب نسختك

                int bytesWritten = 0;
                WriteProcessMemory(sroClient.Handle, ipAddressLocation, redirectIp, redirectIp.Length, out bytesWritten);

                if (bytesWritten > 0)
                    Console.WriteLine("Successfully Redirected to G-BOT Proxy!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);Console.WriteLine(ex.ToString());
                System.Windows.Forms.MessageBox.Show("خطأ في تشغيل اللعبة: " + ex.Message);
            }
        }
    }
}