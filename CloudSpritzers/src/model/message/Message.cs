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
        private int _id;
        private IResponder _sender;
        private Chat _chat;
        private DateTimeOffset _timestamp;
        private string _messageText;
        private bool _isRead;

        public Message(Object sender, Object chat, string messageText, bool isRead)
        {
            this._sender = sender;
            this._chat = chat;
            this._messageText = messageText;
            this._isRead = isRead;
            this._timestamp = DateTimeOffset.UtcNow;
        }

        void MarkAsRead()
        {
            this._isRead = true;
        }

        bool IsMessageRead()
        {
            return _isRead;
        }

        Chat GetChat()
        {
            return this._chat;
        }

        // Interface functionality

        public string GetMessage()
        {
            return this._messageText;
        }

        public IResponder GetSender()
        {
            return _sender;
        }

        IEnumerable<IMessage> GetNextOptions()
        {
            return new List<IMessage>();
        }

        public int GetId()
        {
            return this._id;
        }

        DateTimeOffset GetTimeStamp()
        {
            return _timestamp;
        }
    }
}
