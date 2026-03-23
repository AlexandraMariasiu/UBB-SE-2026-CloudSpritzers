using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.message
{
    // FIXME: Remove this once the actual classes are implemented
    using IResponder = Object;
    using Chat = Object;
    public class Message : IMessage
    {
        private int id;
        private IResponder sender;
        private Chat chat;
        private DateTimeOffset timestamp;
        private string messageText;
        private bool isRead;

        public Message(Object sender, Object chat, string messageText, bool isRead)
        {
            this.sender = sender;
            this.chat = chat;
            this.messageText = messageText;
            this.isRead = isRead;
            this.timestamp = DateTimeOffset.UtcNow;
        }

        void MarkAsRead()
        {
            this.isRead = true;
        }

        bool isMessageRead()
        {
            return isRead;
        }

        Chat getChat()
        {
            return this.chat;
        }

        // Interface functionality

        public string GetMessage()
        {
            return this.messageText;
        }

        public IResponder GetSender()
        {
            return sender;
        }

        IEnumerable<IMessage> GetNextOptions()
        {
            return new List<IMessage>();
        }

        public int GetId()
        {
            return this.id;
        }

        DateTimeOffset getTimeStamp()
        {
            return getTimeStamp();
        }
    }
}
