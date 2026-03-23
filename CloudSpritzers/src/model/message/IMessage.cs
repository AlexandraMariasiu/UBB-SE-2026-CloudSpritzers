using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.message
{
    internal interface IMessage
    {
        string GetMessage();
        IEnumerable<IMessage> GetNextOptions();

        //FIXME: this should return an IResponder but we have to wait for implementation 
        Object GetSender();

        int GetId();

        DateTimeOffset getTimeStamp();

    }
}
