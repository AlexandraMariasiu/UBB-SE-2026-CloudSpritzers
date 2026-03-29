using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.service.bot;

namespace CloudSpritzers1.src.model.faq.bot
{ 
    public record FAQNode(
        int FaqNodeId,
        string QuestionText,
        ImmutableArray<FAQOption> Options,
        bool IsFinalAnswer
    );
}
