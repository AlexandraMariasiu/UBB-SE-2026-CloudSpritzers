using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers.src.model.message
{
    public interface IMessage
    {
        string GetMessage();
        IEnumerable<FAQOption> GetNextOptions();

        ISender GetSender();

        int GetId();

        DateTimeOffset GetTimeStamp();

    }
}
