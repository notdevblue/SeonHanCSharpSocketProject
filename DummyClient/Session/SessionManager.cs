using System;
using System.Collections.Generic;
using System.Text;

namespace DummyClient
{
    class SessionManager
    {
        static SessionManager _instance = new SessionManager();
        public static SessionManager Instance { get { return _instance; } }

        List<ServerSession> _sessions = new List<ServerSession>();

        object _lock = new object();

        public ServerSession Generate()
        {
            lock (_lock)
            {
                ServerSession session = new ServerSession();
                _sessions.Add(session);
                return session;
            }
        }

        public void SendForEach(string msg)
        {
            lock (_lock)
            {
                _sessions.ForEach(x =>
                {
                    ChatMSG packet = new ChatMSG();
                    packet.chat = msg;

                    ArraySegment<byte> segment = packet.Write();

                    x.Send(segment);
                });
            }
        }
    }
}
