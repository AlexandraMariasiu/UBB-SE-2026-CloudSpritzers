using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.message
{
    public interface IMessage
    {
        string GetMessage();
        IEnumerable<IMessage> GetNextOptions();

        IResponder GetSender();

        int GetId();

        DateTimeOffset GetTimeStamp();

    }
}
