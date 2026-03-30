using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.faq.bot;
using CloudSpritzers1.src.model.message;
using CloudSpritzers1.src.repository;

namespace CloudSpritzers1.src.service.bot.strategy
{
    public class DecisionTreeStrategy : IBotStrategy
    {
        const int UNASSIGNED_ID = -1;
        [AllowNull]
        private FAQNode _currentDiscussionNode;

        private DecisionTreeRepository _faqDecisionRepository;

        public DecisionTreeStrategy(DecisionTreeRepository faqRepository)
        {
            this._faqDecisionRepository = faqRepository;
            this._currentDiscussionNode = null;
        }

        public BotMessage Process(BotEngine botEngine, IMessage message)
        {
            string text = message.GetMessage();

            FAQOption? option = _currentDiscussionNode.Options.FirstOrDefault((option) => option.Label.Equals(text));
            if (option == null)
            {
                return new BotMessage.BotMessageBuilder(botEngine, message.GetChat(), UNASSIGNED_ID,
                    _faqDecisionRepository.GetById((int)BotStandardMessages.RESTART_CONVERSATION)).Build();
            }

            FAQNode nextQuestion = _faqDecisionRepository.GetById(option.NextOptionId);

            return new BotMessage.BotMessageBuilder(botEngine, message.GetChat(), UNASSIGNED_ID, nextQuestion).Build();
        }
    }
}
