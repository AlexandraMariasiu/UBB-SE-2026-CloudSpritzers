using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.chat;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers1.src.model.message
{
    public interface IMessage
    {
        public string GetMessage();
        public IEnumerable<FAQOption> GetNextOptions();

        public ISender GetSender();

        public int GetId();

        public Chat GetChat();

        public DateTimeOffset GetTimeStamp();

    }
}
