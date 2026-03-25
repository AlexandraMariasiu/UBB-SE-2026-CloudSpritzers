using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.message
{
    using IResponder = Object;
    public class TextDialogOption : IMessage
    {
        private int _dialogId;
        private string _message;
        private DateTimeOffset _timestamp;

        public TextDialogOption(int id, string message)
        {
            this._dialogId = id;
            this._message = message;
            this._timestamp = DateTimeOffset.UtcNow;
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
            return new List<IMessage>();
        }
    }
}
