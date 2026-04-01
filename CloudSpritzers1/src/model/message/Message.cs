using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.chat;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers1.src.model.message
{
    public class Message : IMessage
    {
        private int _message_id;
        private ISender _sender;
        private Chat _chat;
        private DateTimeOffset _timestamp;
        private string _messageText;

        public Message(ISender sender, Chat chat, string messageText)
        {
            this._sender = sender;
            this._chat = chat;
            this._messageText = messageText;
            this._timestamp = DateTimeOffset.UtcNow;
        }

        // TODO: This constructor is currently used only for mapping from DB. Without this message_id and timestamp are unsettable.
        public Message(int id, ISender sender, Chat chat, string messageText, DateTimeOffset timestamp)
        {
            this._message_id = id;
            this._sender = sender;
            this._chat = chat;
            this._messageText = messageText;
            this._timestamp = timestamp;
        }

        public Chat GetChat()
        {
            return this._chat;
        }

        // Interface functionality

        public string GetMessage()
        {
            return this._messageText;
        }

        public ISender GetSender()
        {
            return _sender;
        }

        public int GetId()
        {
            return this._message_id;
        }

        IEnumerable<FAQOption> IMessage.GetNextOptions()
        {
            return new List<FAQOption>();
        }

        DateTimeOffset IMessage.GetTimeStamp()
        {
            return _timestamp;
        }

        Chat IMessage.GetChat()
        {
            return this._chat;
        }

    }
}
