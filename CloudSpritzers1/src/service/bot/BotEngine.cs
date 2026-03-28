using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers.src.model.message;
using CloudSpritzers1.src.model.faq.bot;

namespace CloudSpritzers1.src.service.bot
{
    public class BotEngine : IResponder
    {

        private Dictionary<string, IMessage> currentSelection;

        private IMessage currentDiscussionNode;

        public IMessage Respond(IMessage message)
        {
            string text = message.GetMessage();

            if(!currentSelection.ContainsKey(text))
            {
                // TODO take this from repository, map to db the chat options
                return new FAQNode.Builder(-1, "Sorry, I am experiencing some technical difficulties. Could you start again?", "")
                    .AddOption(
                        new FAQNode.Builder(-1, "Yes", "")
                    )
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
