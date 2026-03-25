using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers.src.model.chat;

namespace CloudSpritzers.src.repository
{
    public class ChatMemoryRepository : IRepository<int, Chat>
    {

        private List<Chat> _chats;

        public ChatMemoryRepository() 
        { 
            
            _chats = new List<Chat> ();
        }

        public IEnumerable<Chat> GetUnresolvedChats()
        {
            return _chats.Where(chat => chat.Status != ChatStatus.Closed);
        }

        public IEnumerable<Chat> GetUnansweredChats()
        {
            return _chats.Where(chat =>
                chat.Messages.Count > 0 &&
                chat.Messages.Last().GetSender() is new Object() // FIXME after it exists
                );
        }

    }
}
