using System;
using System.Collections.Generic;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers1.src.dto
{
    public record MessageDTO(
        int MessageId,
        int ChatId,
        int SenderId,
        string MessageText,
        DateTimeOffset Timestamp,
        IEnumerable<FAQOption> FaqOptions
    );
}
