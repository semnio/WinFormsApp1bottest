using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WinFormsApp1bottest
{
    public class PacketManager
    {
        // سوف نقوم بإضافة كلاس startProxy 
        private TcpListener? _proxyListener = null; // أضف = null لحل التحذير 
        // حالة البروكسي (يعمل/متوقف)
        // تم اضافة علامة ؟  لتمكين القيمة من ان تكون فارغة في حالة عدم التهيئة في سطر 13
        private bool _isProxyRunning = false;

        public void StartProxy(int localPort)
        {
            try
            {
                // 1. إعداد البروكسي ليسمع على جهازك المحلي (IP: 127.0.0.1)
                _proxyListener = new TcpListener(IPAddress.Loopback, localPort);
                _proxyListener.Start();
                _isProxyRunning = true;

                // 2. تشغيل "خيط" منفصل لانتظار اللعبة حتى لا يتجمد البوت
                Thread listenThread = new Thread(ListenForSROClient);
                listenThread.IsBackground = true; // لكي يغلق مع إغلاق البوت
                listenThread.Start();

                Console.WriteLine($"[G-BOT] البروكسي يعمل الآن على البورت: {localPort}");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("فشل تشغيل البروكسي: " + ex.Message);
            }
        }

        private void ListenForSROClient()
        {
            while (_isProxyRunning)
            {
                try
                {
                    // البوت هنا في حالة "انتظار" لاتصال sro_client.exe
                    TcpClient clientSocket = _proxyListener.AcceptTcpClient();// انتظار اتصال اللعبة
                    Console.WriteLine("تم اتصال اللعبة بالبوت بنجاح!");

                    // هنا سنضع مستقبلاً كود الـ Bridge (الجسر) لنقل البيانات للسيرفر
                }
                catch { /* في حالة إغلاق البروكسي */ }
            }
        }
        // تعريف الأكواد الأساسية (Opcodes) المستخرجة من nBot/mBot
        public static class Opcodes
        {
            public const ushort CLIENT_VERSION = 0x2001;
            public const ushort CLIENT_LOGIN_REQUEST = 0x6102;
            public const ushort CLIENT_CHARACTER_SELECTION_ACTION = 0x7001;
            public const ushort CLIENT_MOVEMENT_REQUEST = 0x7021; // كود التحرك
            public const ushort CLIENT_CHAT_REQUEST = 0x7025;     // كود الشات

            // SERVER SIDE (الأكواد اللي بنستقبلها)
            public const ushort SERVER_LOGIN_RESPONSE = 0xA102;
            public const ushort SERVER_CHARACTER_LIST = 0xB007;
        }

        // دالة لتجهيز الباكت (كمثال بسيط)
        public byte[] CreateLoginPacket(string username, string password)
        {
            // هنا يتم تحويل اليوزر والباسورد لبايتات (Binary) 
            // ليتم إرسالها للسيرفر باستخدام Opcode 0x6102
            Console.WriteLine($"تجهيز باكت الدخول للمستخدم: {username}");

            // ملاحظة: التشفير (Security) سيتم إضافته هنا لاحقاً
            return new byte[] { /* مصفوفة البيانات */ };
        }

        // دالة لمعالجة الباكتات القادمة من السيرفر
        public void ProcessIncomingPacket(ushort opcode, byte[] data)
        {
            switch (opcode)
            {
                case Opcodes.SERVER_LOGIN_RESPONSE:
                    // منطق نجاح أو فشل الدخول
                    break;
                case Opcodes.SERVER_CHARACTER_LIST:
                    // منطق عرض الشخصيات في البوت
                    break;
            }
        }//end method ProcessIncomingPacket
        public void StartProxy(string serverIp, int port)
        {
            // البوت يبدأ في التنصت على الجهاز المحلي
            // Local Host: 127.0.0.1
            Console.WriteLine($"جاري تشغيل البروكسي على IP: {serverIp} بورت: {port}");
        }
    }//end class PacketManager
}
