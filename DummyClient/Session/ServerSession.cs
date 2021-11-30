using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DummyClient
{
    class ServerSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            //여기까지 코드가 도착하면 연결이 된거야
            Console.WriteLine($"Connected to {endPoint}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected from : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            //Console.WriteLine($"Transfered Bytes : {numOfBytes}");
        }
    }
}
