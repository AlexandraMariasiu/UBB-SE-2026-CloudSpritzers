using CloudSpritzers1.Src.ViewModel.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatModel = CloudSpritzers1.Src.Model.Chat.Chat;
using CloudSpritzers1.Src.Model.Faq.Bot;

namespace CloudSpritzers1.Src.Model.Message
{
    public class Message : IMessage
    {
        private int _message_id;
        private ISender _sender;
        private ChatModel _chat;
        private DateTimeOffset _timestamp;
        private string _messageText;

        public Message(ISender sender, ChatModel chat, string messageText)
        {
            this._sender = sender;
            this._chat = chat;
            this._messageText = messageText;
            this._timestamp = DateTimeOffset.UtcNow;
        }

        // TODO: This constructor is currently used only for mapping from DB. Without this message_id and timestamp are unsettable.
        public Message(int id, ISender sender, ChatModel chat, string messageText, DateTimeOffset timestamp)
        {
            this._message_id = id;
            this._sender = sender;
            this._chat = chat;
            this._messageText = messageText;
            this._timestamp = timestamp;
        }

        public ChatModel GetChat()
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

        ChatModel IMessage.GetChat()
        {
            return this._chat;
        }
    }
}
