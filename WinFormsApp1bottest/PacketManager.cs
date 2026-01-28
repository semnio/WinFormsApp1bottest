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
        // 1. تعريف الأكواد الأساسية (Opcodes)
        public static class Opcodes
        {
            public const ushort CLIENT_VERSION = 0x2001;
            public const ushort CLIENT_LOGIN_REQUEST = 0x6102;
            public const ushort CLIENT_CHARACTER_SELECTION_ACTION = 0x7001;
            public const ushort CLIENT_MOVEMENT_REQUEST = 0x7021;
            public const ushort CLIENT_CHAT_REQUEST = 0x7025;
            public const ushort SERVER_HP_MP_UPDATE = 0x3013;
            public const ushort SERVER_LOGIN_RESPONSE = 0xA102;
            public const ushort SERVER_CHARACTER_LIST = 0xB007;
        }

        private TcpListener _proxyListener = null;
        private bool _isProxyRunning = false;
        public static Action<string> OnPacketReceived;
        private SecurityManager _security = new SecurityManager();

        // --- الدالة التي كانت تسبب الخطأ (تمت إعادتها) ---
        public byte[] CreateLoginPacket(string username, string password)
        {
            // مؤقتاً نعيد مصفوفة فارغة حتى نربط التشفير لاحقاً
            return new byte[] { 0x00 };
        }
        // -----------------------------------------------

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Proxy Error: " + ex.Message);
            }
        }

        private void ListenForSROClient()
        {
            while (_isProxyRunning)
            {
                try
                {
                    TcpClient sroClient = _proxyListener.AcceptTcpClient();

                    // تهيئة الحماية للطرفين
                    SecurityManager clientSecurity = new SecurityManager();
                    SecurityManager serverSecurity = new SecurityManager();

                    TcpClient realServer = new TcpClient();
                    // تأكد أن هذا الـ IP هو IP سيرفر اللعبة وليس 127.0.0.1
                    realServer.Connect("IP_REAL_SERVER", 15779);

                    // تمرير كائنات الـ Security للجسر
                    Thread clientToServer = new Thread(() => ProxyDataBridge(sroClient, realServer, "Client -> Server", clientSecurity));
                    Thread serverToClient = new Thread(() => ProxyDataBridge(realServer, sroClient, "Server -> Client", serverSecurity));

                    clientToServer.IsBackground = true;
                    serverToClient.Start();
                    clientToServer.Start();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            
            }
        }

        private void ProxyDataBridge(TcpClient source, TcpClient destination, string direction, SecurityManager clientSecurity)
        {
            try
            {
                NetworkStream sourceStream = source.GetStream();
                NetworkStream destStream = destination.GetStream();
                byte[] buffer = new byte[8192];
                int bytesRead;

                while (source.Connected && (bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ProcessData(buffer, bytesRead, direction);
                    if (destination.Connected) destStream.Write(buffer, 0, bytesRead);
                }
            }
            catch { }
            finally
            {
                source.Close();
                destination.Close();
            }
        }

        private void ProcessData(byte[] data, int length, string direction)
        {
            try
            {
                string hexData = BitConverter.ToString(data, 0, length).Replace("-", " ");
                if (hexData.Contains("13 30")) OnPacketReceived?.Invoke($"[STAT] HP/MP Update Detected");
                OnPacketReceived?.Invoke($"[{direction}] {hexData}");
            }
            catch { }
        }

        public void StopProxy() => _isProxyRunning = false;
    }
}