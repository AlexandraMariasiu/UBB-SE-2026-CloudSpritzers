using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers1.src.model.message
{

    // FIXME: remove when chat exists
    using Chat = object;
    public interface IMessage
    {
        string GetMessage();
        IEnumerable<FAQOption> GetNextOptions();

        ISender GetSender();

        int GetId();

        Chat GetChat();
        
        DateTimeOffset GetTimeStamp();

    }
}
