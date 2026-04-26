using CloudSpritzers1.Src.Model.Chats;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Service.Bot;
using CloudSpritzers1.Src.Service.Bot.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CloudSpritzers1Tests.Src.Service.Bot
{
    [TestClass]
    public class BotEngineTests
    {
        private IBotStrategy _mockStrategy = null!;
        private BotEngine _botEngine = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockStrategy = Substitute.For<IBotStrategy>();

            _botEngine = new BotEngine(_mockStrategy);
        }

        [TestMethod]
        public void GenerateAppropriateResponse_ValidMessage_ReturnsStrategyResult()
        {
            var mockIncomingMessage = Substitute.For<IMessage>();
            var dummyChat = new Chat(1, 1, ChatStatus.Active);

            var expectedResponse = new BotMessage.BotMessageBuilder(_botEngine, dummyChat, 1)
                .WithMessage("I am the mocked strategy response")
                .Build();

            _mockStrategy.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(_botEngine, mockIncomingMessage)
                .Returns(expectedResponse);

            var resultedBotResponse = _botEngine.GenerateAppropriateResponseBasedOnCurrentStrategy(mockIncomingMessage);

            Assert.AreEqual(expectedResponse, resultedBotResponse);
        }

        [TestMethod]
        public void ResetBotConversationState_CallsStrategyResetMethod_ExactlyOnce()
        {
            _botEngine.ResetBotConversationStateToInitialRootNode();

            _mockStrategy.Received(1).ResetCurrentlyActiveConversationNodeToInitialStartingPoint();
        }

        [TestMethod]
        public void RetrieveConfiguredEmailAddressForBotContact_WhenCalled_ReturnsCorrectEmail()
        {
            var resultedEmail = _botEngine.RetrieveConfiguredEmailAddressForBotContact();

            Assert.AreEqual("customer-support@cloudspritzers.com", resultedEmail);
        }

        [TestMethod]
        public void RetrieveConfiguredDisplayFullNameForBot_WhenCalled_ReturnsCarlos()
        {
            var resultedFullName = _botEngine.RetrieveConfiguredDisplayFullNameForBot();

            Assert.AreEqual("Carlos", resultedFullName);
        }

        [TestMethod]
        public void RetrieveUniqueDatabaseIdentifierForBot_WhenCalled_ReturnsZero()
        {
            var resultedIdentifier = _botEngine.RetrieveUniqueDatabaseIdentifierForBot();

            Assert.AreEqual(0, resultedIdentifier);
        }
    }
}