using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using CloudSpritzers1.Src.Model.Chats;
using CloudSpritzers1.Src.Model.Faq.Bot;

// TODO : Maybe merge this with the regular message or pull general data in IMessage and make it abstract class instead of interface
// At this point it is not a contract of functionality but an identity
namespace CloudSpritzers1.Src.Model.Message
{
    public class BotMessage : IMessage
    {
        private int _messageId;
        private ISender _sender;
        private Chat _chat;
        private DateTimeOffset _timestamp;
        private string _messageText;
        private IEnumerable<FAQOption> _faqOptions;

        private BotMessage(int messageId, ISender sender, Chat chat, string messageText, IEnumerable<FAQOption> options)
        {
            this._messageId = messageId;
            this._sender = sender;
            this._chat = chat;
            this._messageText = messageText;
            this._timestamp = DateTimeOffset.UtcNow;
            this._faqOptions = options;
        }

        private BotMessage(int messageId, ISender sender, Chat chat, string messageText, IEnumerable<FAQOption> options, DateTimeOffset timestamp) : this(messageId, sender, chat, messageText, options)
        {
            this._timestamp = timestamp;
        }

        public Chat GetChat()
        {
            return this._chat;
        }

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
            return this._messageId;
        }

        public IEnumerable<FAQOption> GetNextOptions()
        {
            return this._faqOptions;
        }

        public DateTimeOffset GetTimeStamp()
        {
            return _timestamp;
        }
        public class BotMessageBuilder
        {
            private int _messageId;
            private ISender _sender;
            private Chat _chat;
            private string _messageText;
            private List<FAQOption> _faqOptions;
            private DateTimeOffset _timestamp;

            public BotMessageBuilder(ISender sender, Chat chat, int messageId, FAQNode nodeToMessage)
                : this(sender, chat, messageId)
            {
                this._messageText = nodeToMessage.QuestionText;
                this._faqOptions = nodeToMessage.Options.ToList();
            }

            public BotMessageBuilder(ISender sender, Chat chat, int messageId)
            {
                this._messageText = string.Empty;
                this._messageId = messageId;
                this._sender = sender;
                this._chat = chat;
                this._faqOptions = new List<FAQOption>();
                this._timestamp = DateTimeOffset.UtcNow;
            }

            public BotMessageBuilder WithTimestamp(DateTimeOffset timestamp)
            {
                this._timestamp = timestamp;
                return this;
            }

            public BotMessageBuilder WithMessage(string setMessage)
            {
                this._messageText = setMessage;
                return this;
            }

            public BotMessageBuilder WithId(int setId)
            {
                this._messageId = setId;
                return this;
            }

            public BotMessageBuilder AddOption(FAQOption addedOption)
            {
                _faqOptions.Add(addedOption);
                return this;
            }

            public BotMessageBuilder AddOptions(IEnumerable<FAQOption> setOptions)
            {
                _faqOptions.Clear();
                _faqOptions.AddRange(setOptions);
                return this;
            }

            public BotMessage Build()
            {
                return new BotMessage(this._messageId, this._sender, this._chat, this._messageText, this._faqOptions.ToImmutableArray(), this._timestamp);
            }
        }
    }
}
