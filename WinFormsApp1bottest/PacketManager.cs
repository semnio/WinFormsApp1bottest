using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApp1bottest
{
    public class PacketManager
    {
        // 1. تعريف الأكواد الأساسية (Opcodes) المستخرجة من المشاريع السابقة
        public static class Opcodes
        {
            public const ushort CLIENT_VERSION = 0x2001;
            public const ushort CLIENT_LOGIN_REQUEST = 0x6102;
            public const ushort CLIENT_CHARACTER_SELECTION_ACTION = 0x7001;
            public const ushort CLIENT_MOVEMENT_REQUEST = 0x7021;
            public const ushort CLIENT_CHAT_REQUEST = 0x7025;

            public const ushort SERVER_LOGIN_RESPONSE = 0xA102;
            public const ushort SERVER_CHARACTER_LIST = 0xB007;
        }

        // 2. متغيرات التحكم في البروكسي والربط
        private TcpListener _proxyListener = null;
        private bool _isProxyRunning = false;
        private string _realServerIP = "127.0.0.1"; // سيتم تحديثه لاحقاً بـ IP السيرفر الحقيقي
        private int _port = 15779;

        /// <summary>
        /// تشغيل البروكسي لفتح بوابة بين اللعبة والبوت
        /// </summary>
        public void StartProxy(int localPort)
        {
            try
            {
                if (_isProxyRunning) return;

                _proxyListener = new TcpListener(IPAddress.Loopback, localPort);
                _proxyListener.Start();
                _isProxyRunning = true;

                Thread listenThread = new Thread(ListenForSROClient);
                listenThread.IsBackground = true;
                listenThread.Start();

                Console.WriteLine($"[G-BOT] Proxy started on port: {localPort}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تشغيل البروكسي: " + ex.Message);
            }
        }

        private void ListenForSROClient()
        {
            while (_isProxyRunning)
            {
                try
                {
                    // استقبال اتصال اللعبة (sro_client)
                    TcpClient sroClient = _proxyListener.AcceptTcpClient();
                    Console.WriteLine("SRO Client Connected to G-BOT!");

                    // الاتصال بسيرفر سيلكرود الحقيقي (هنا يجب وضع IP السيرفر الحقيقي)
                    TcpClient realServer = new TcpClient();
                    // ملاحظة: إذا كنت تختبر محلياً اتركها 127.0.0.1 ولكن بورت مختلف
                    // realServer.Connect(_realServerIP, _port); 

                    // تشغيل الجسر (Bridge) لنقل البيانات
                    Thread clientToServer = new Thread(() => ProxyDataBridge(sroClient, realServer, "Client -> Server"));
                    Thread serverToClient = new Thread(() => ProxyDataBridge(realServer, sroClient, "Server -> Client"));

                    clientToServer.IsBackground = true;
                    serverToClient.IsBackground = true;

                    clientToServer.Start();
                    serverToClient.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Error: " + ex.Message);
                }
            }
        }

        private void ProxyDataBridge(TcpClient source, TcpClient destination, string direction)
        {
            try
            {
                NetworkStream sourceStream = source.GetStream();
                NetworkStream destStream = destination.GetStream();
                byte[] buffer = new byte[8192]; // زيادة حجم البفر لتحسين الأداء
                int bytesRead;

                while (source.Connected && (bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // تحليل البيانات (هنا سيتم إضافة نظام الـ Ai والـ Log لاحقاً)
                    ProcessData(buffer, bytesRead, direction);

                    // تمرير البيانات للطرف الآخر
                    if (destination.Connected)
                    {
                        destStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            catch { /* إغلاق صامت عند قطع الاتصال */ }
            finally
            {
                source.Close();
                destination.Close();
            }
        }

        private void ProcessData(byte[] data, int length, string direction)
        {
            // دالة مبدئية لطباعة حجم البيانات (Packet Logging)
            // سنقوم هنا لاحقاً بفك التشفير باستخدام SecurityManager
            Console.WriteLine($"[{direction}] Packet Size: {length} bytes");
        }

        public void StopProxy()
        {
            _isProxyRunning = false;
            _proxyListener?.Stop();
        }
    }
}