using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
    public static void ChatMSGHandler(PacketSession session, IPacket packet)
    {
        ChatMSG p = packet as ChatMSG;

        ClientSession clientSession = session as ClientSession;

        if(clientSession.Room == null)
        {
            return; //방에 들어오지도 않은 세션이 메시지를 보내면 잘못된 거
        }

        clientSession.Room.Push(() => clientSession.Room.BroadCast(clientSession, p.chat));
    }

}

