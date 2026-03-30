using System;
using System.Collections.Generic;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers1.src.DTO
{
    public record MessageDTO(
        int MessageId,
        int ChatId,
        int SenderId,       // numeric FK matching the DB column sender_id
        string MessageText,
        DateTimeOffset Timestamp,
        IEnumerable<FAQOption> FaqOptions
    );
}
