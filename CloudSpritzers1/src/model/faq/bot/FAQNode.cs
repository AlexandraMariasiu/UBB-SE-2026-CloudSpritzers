using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers.src.model.message;
using CloudSpritzers1.src.service.bot;

namespace CloudSpritzers1.src.model.faq.bot
{ 
    public record FAQNode(
        int FaqNodeId,
        string QuestionText,
        List<FAQOption> Options,
        bool IsFinalAnswer
    );
}
