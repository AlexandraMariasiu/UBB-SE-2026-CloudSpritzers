using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers.src.model;

namespace CloudSpritzers.src.model.message
{
    using IResponder = Object;
    public class ButtonDialogOption : IMessage
    {
        private int _dialogId;
        private string _message;
        private string _payload;
        private List<IMessage> _nextOptions;
        private DateTimeOffset _timestamp;

        private ButtonDialogOption(int id, string message, string payload, List<IMessage> nextOptions)
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

        IEnumerable<IMessage> IMessage.GetNextOptions()
        {
            return _nextOptions;
        }

        model.IResponder IMessage.GetSender()
        {
            throw new NotImplementedException();
        }

        public class Builder
        {
            private int _dialogId;
            private string _message;
            private string _payload;
            private List<IMessage> _nextOptions = new List<IMessage>();

            public Builder(int dialogId, string message, string payload)
            {
                this._message = message;
                this._dialogId = dialogId;
                this._payload = payload;
            }

            public Builder()
            {
                this._dialogId = -1;
                this._message = "";
                this._payload = "";
            }

            public Builder WithMessage(string setMessage)
            {
                this._message = setMessage;
                return this;
            }

            public Builder WithId(int setId)
            {
                this._dialogId = setId;
                return this;
            }

            public Builder WithPayload(string setPayload)
            {
                this._payload = setPayload;
                return this;
            }

            public Builder AddOption(IMessage addedOption)
            {
                _nextOptions.Add(addedOption);
                return this;
            }

            public ButtonDialogOption build()
            {
                return new ButtonDialogOption(this._dialogId, this._message, this._payload, this._nextOptions);
            }

        }
    }
}
