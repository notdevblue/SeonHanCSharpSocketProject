using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();

        JobQueue _jobQueue = new JobQueue();

        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }


        public void Enter(ClientSession session)
        {
            _sessions.Add(session);
            session.Room = this;
            
        }

        public void Leave(ClientSession session)
        {
            _sessions.Remove(session);
            
        }

        public void BroadCast(ClientSession session, string chat)
        {
            ChatBroad chatBroad = new ChatBroad();
            chatBroad.playerId = session.sessionId;
            chatBroad.chat = chat;

            ArraySegment<byte> segment = chatBroad.Write();

            _pendingList.Add(segment);
        }

        public void Flush()
        {
            _sessions.ForEach(x => x.Send(_pendingList));

            Console.WriteLine($"FLUSHED {_pendingList.Count} item");
            _pendingList.Clear();
        }
    }
}
