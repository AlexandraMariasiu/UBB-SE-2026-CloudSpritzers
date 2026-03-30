using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.faq.bot;
using CloudSpritzers1.src.model.message;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.service.bot.strategy;

namespace CloudSpritzers1.src.service.bot
{
    public class BotEngine : ISender
    {
        public const int BOT_CANNONIZED_ID = 0;
        private IBotStrategy _responseStrategy;

        public BotEngine(IBotStrategy responseStrategy)
        {
            this._responseStrategy = responseStrategy;
        }


        public BotMessage Respond(IMessage message)
        {
            return _responseStrategy.Process(this, message);
        }
        
        public string GetEmail()
        {
            return "customer-support@cloudspritzers.com";
        }

        public string GetName()
        {
            return "Carlos";
        }

        public int GetId()
        {
            return 0; // ChatBot is always identified as the first 
        }
    }
}
