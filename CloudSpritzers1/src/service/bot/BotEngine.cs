using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers.src.model.message;
using CloudSpritzers1.src.model.faq.bot;
using CloudSpritzers1.src.service.bot.strategy;

namespace CloudSpritzers1.src.service.bot
{
    public class BotEngine : ISender
    {

        private FAQNode _currentDiscussionNode;

        private IBotStrategy _responseStrategy;

        public BotEngine(IBotStrategy responseStrategy)
        {
            this._responseStrategy = responseStrategy;
            this._currentDiscussionNode = null; 
        }


        public Respond(IMessage message)
        {
            string text = message.GetMessage();

            FAQOption? option = _currentDiscussionNode.Options.FirstOrDefault((option) => option.Label.Equals(text));
            if(option == null)
            {
                 return new Message(this, )
            }
            return null;
        }
        
        public string GetEmail()
        {
            return "customer-support@cloudspritzers.com";
        }

        public string GetName()
        {
            return "Carlos";
        }
    }
}
