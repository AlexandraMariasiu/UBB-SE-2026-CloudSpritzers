using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.message
{
    using IResponder = Object;
    public class ButtonDialogueOption : IMessage
    {
        private int _dialogId;
        private string _message;
        private string _payload;
        private List<IMessage> _nextOptions;
        private DateTimeOffset _timestamp;

        private ButtonDialogueOption(int id, string message, string payload, List<IMessage> nextOptions)
        {
            this._dialogId = id;
            this._message = message;
            this._timestamp = DateTimeOffset.UtcNow;
            this._nextOptions = nextOptions;
        }

        public int GetId()
        {
            return this._dialogId;
        }

        public string GetMessage()
        {
            return this._message;
        }

        public IResponder GetSender()
        {
            return new Object(); // FIXME send chat bot engine when implemented 
        }

        public DateTimeOffset GetTimeStamp()
        {
            return this._timestamp;
        }

        IEnumerable<IMessage> GetNextOptions()
        {
            return new List<IMessage>();
        }

        public class ButtonDialogueBuilder
        {
            private int _dialogId;
            private string _message;
            private string _payload;
            private List<IMessage> _nextOptions = new List<IMessage>();

            public ButtonDialogueBuilder(int dialogId, string message, string payload)
            {
                this._message = message;
                this._dialogId = dialogId;
                this._payload = payload;
            }

            public ButtonDialogueBuilder()
            {
                this._dialogId = -1;
                this._message = "";
                this._payload = "";
            }

            public ButtonDialogueBuilder WithMessage(string setMessage)
            {
                this._message = message;
            }

            public ButtonDialogueBuilder WithId(int setId)
            {
                this._dialogId = id;
            }

            public ButtonDialogueBuilder WithPayload(string setPayload)
            {
                this._payload = payload;
            }

            public ButtonDialogueBuilder AddOption(IMessage addedOption)
            {
                _nextOptions.Add(addedOption);
            }

            public ButtonDialogueOption build()
            {
                return new ButtonDialogueOption(this._dialogId, this._message, this._payload, this._nextOptions);
            }

        }
    }
}
