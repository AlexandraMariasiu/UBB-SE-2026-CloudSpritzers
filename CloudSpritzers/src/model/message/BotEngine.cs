using System;
using System.Collections.Generic;

//FIXME: This is just a stud for the bot engine, it will need to be implemented.
namespace CloudSpritzers.src.model.message
{
    public class BotEngine : IResponder
    {
        private static readonly BotEngine _instance = new BotEngine();
        public static BotEngine Instance => _instance;

        private Dictionary<string, int> _tokens = new Dictionary<string, int>();
        private Dictionary<string, IMessage> _options = new Dictionary<string, IMessage>();
        private IMessage _currentOption;

        private BotEngine() { }

        public void ParseMessage(string message)
        {
            throw new NotImplementedException();
        }

        public IMessage Respond()
        {
            throw new NotImplementedException();
        }

        public string GetName() => "BotEngine";
        public string GetEmail() => string.Empty;
    }
}