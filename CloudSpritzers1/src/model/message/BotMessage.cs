using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using CloudSpritzers1.src.model.chat;
using CloudSpritzers1.src.model.faq.bot;


// TODO : Maybe merge this with the regular message or pull general data in IMessage and make it abstract class instead of interface
// At this point it is not a contract of functionality but an identity
namespace CloudSpritzers1.src.model.message
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

        IEnumerable<FAQOption> IMessage.GetNextOptions()
        {
            return this._faqOptions;
        }

        DateTimeOffset IMessage.GetTimeStamp()
        {
            return _timestamp;
        }

        object IMessage.GetChat()
        {
            return this._chat;
        }

        public class Builder
        {
            private int _messageId;
            private ISender _sender;
            private Chat _chat;
            private string _messageText;
            private List<FAQOption> _faqOptions;

            public Builder(ISender sender, Chat chat, int messageId, FAQNode nodeToMessage) 
                : this(sender, chat, messageId)
            {
                this._messageText = nodeToMessage.QuestionText;
                this._faqOptions = nodeToMessage.Options.ToList();
            }

            public Builder(ISender sender, Chat chat, int messageId)
            {
                this._messageText = "";
                this._messageId = messageId;
                this._sender = sender;
                this._chat = chat;
                this._faqOptions = new List<FAQOption>();
            }

            public Builder WithMessage(string setMessage)
            {
                this._messageText = setMessage;
                return this;
            }

            public Builder WithId(int setId)
            {
                this._messageId = setId;
                return this;
            }

            public Builder AddOption(FAQOption addedOption)
            {
                _faqOptions.Add(addedOption);
                return this;
            }

            public Builder AddOptions(IEnumerable<FAQOption> setOptions)
            {
                _faqOptions.Clear();
                _faqOptions.AddRange(setOptions);
                return this;
            }

            public BotMessage Build()
            {
                return new BotMessage(this._messageId, this._sender, this._chat, this._messageText, this._faqOptions.ToImmutableArray());
            }

        }
    }
}
