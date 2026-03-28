using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers.src.model;
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

        public Chat GetById(int id)
        {
            Chat chat = _chats.First(c => c.ChatId == id);

            if (chat == null)
                throw new KeyNotFoundException($"Chat with id {id} not found.");

            return chat;
        }

        public int Add(Chat elem)
        {
            if (_chats.Any(c => c.ChatId == elem.ChatId))
            {
                throw new InvalidOperationException($"Chat with ID {elem.ChatId} already exists.");
            }

            _chats.Add(elem);
            return elem.ChatId;
        }

        public void DeleteById(int id)
        {
            Chat chat = GetById(id);
            _chats.Remove(chat);
        }

        public void UpdateById(int id, Chat elem)
        {
            int index = _chats.FindIndex(c => c.ChatId == id);
            if (index == -1) throw new KeyNotFoundException($"Chat with ID {id} not found");
            _chats[index] = elem;
        }

        public IEnumerable<Chat> GetAll()
        {
            return _chats;
        }
    }
}
