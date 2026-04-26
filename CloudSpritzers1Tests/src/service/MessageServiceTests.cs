using CloudSpritzers1.Src.Model.Chats;
using CloudSpritzers1.Src.Model.Faq.Bot;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Repository;
using CloudSpritzers1.Src.Service;
using CloudSpritzers1.Src.Service.Bot;
using CloudSpritzers1.Src.Service.Bot.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudSpritzers1Tests.Src.Service
{
    [TestClass]
    public class MessageServiceTests
    {
        private IRepository<int, Chat> _mockChatRepository = null!;
        private IRepository<int, Message> _mockMessageRepository = null!;
        private IBotStrategy _mockStrategy = null!;
        private BotEngine _realBotEngine = null!;
        private MessageService _messageService = null!;

        private class TestSender : ISender
        {
            public int RetrieveUniqueDatabaseIdentifierForBot() => 1;
            public string RetrieveConfiguredDisplayFullNameForBot() => "Test User";
            public string RetrieveConfiguredEmailAddressForBotContact() => "user@test.com";
        }
        private TestSender _testSender = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockChatRepository = Substitute.For<IRepository<int, Chat>>();
            _mockMessageRepository = Substitute.For<IRepository<int, Message>>();
            _mockStrategy = Substitute.For<IBotStrategy>();

            _realBotEngine = new BotEngine(_mockStrategy);
            _messageService = new MessageService(_mockChatRepository, _mockMessageRepository, _realBotEngine);
            _testSender = new TestSender();
        }

        [TestMethod]
        public void Constructor_WithNullChatRepo_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new MessageService(null!, _mockMessageRepository, _realBotEngine));
        }

        [TestMethod]
        public void Constructor_WithNullMessageRepo_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new MessageService(_mockChatRepository, null!, _realBotEngine));
        }

        [TestMethod]
        public void Constructor_WithNullBotEngine_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new MessageService(_mockChatRepository, _mockMessageRepository, null!));
        }

        [TestMethod]
        public void SendMessage_WithNullOption_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _messageService.SendMessage(1, _testSender, null!));
        }

        [TestMethod]
        public void SendMessage_WithInactiveChat_ThrowsInvalidOperationException()
        {
            var closedChat = new Chat(1, 101, ChatStatus.Closed);
            _mockChatRepository.GetById(1).Returns(closedChat);
            var selectedChatOption = new FAQOption("Hello", 2);

            Assert.ThrowsExactly<InvalidOperationException>(() => _messageService.SendMessage(1, _testSender, selectedChatOption));
        }

        [TestMethod]
        public void SendMessage_WithInactiveChat_ThrowsCorrectMessage()
        {
            var closedChat = new Chat(1, 101, ChatStatus.Closed);
            _mockChatRepository.GetById(1).Returns(closedChat);
            var selectedChatOption = new FAQOption("Hello", 2);

            var exceptionThrown = Assert.ThrowsExactly<InvalidOperationException>(() => _messageService.SendMessage(1, _testSender, selectedChatOption));
            Assert.AreEqual("Chat 1 is not active.", exceptionThrown.Message);
        }

        [TestMethod]
        public void SendMessage_WithValidInput_ReturnsCorrectBotReplyMessage()
        {
            var activeChat = new Chat(1, 101, ChatStatus.Active);
            _mockChatRepository.GetById(1).Returns(activeChat);
            var selectedChatOption = new FAQOption("Help me", 2);
            var expectedReply = new BotMessage.BotMessageBuilder(_realBotEngine, activeChat, 2).WithMessage("I can help").Build();
            _mockStrategy.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>()).Returns(expectedReply);

            var resultedChatMessage = _messageService.SendMessage(1, _testSender, selectedChatOption);

            Assert.AreEqual("I can help", resultedChatMessage.GetMessage());
        }

        [TestMethod]
        public void SendMessage_WithValidInput_PersistsBothUserAndBotMessages()
        {
            var activeChat = new Chat(1, 101, ChatStatus.Active);
            _mockChatRepository.GetById(1).Returns(activeChat);
            var selectedChatOption = new FAQOption("Help me", 2);
            var expectedReply = new BotMessage.BotMessageBuilder(_realBotEngine, activeChat, 2).WithMessage("I can help").Build();
            _mockStrategy.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>()).Returns(expectedReply);

            _messageService.SendMessage(1, _testSender, selectedChatOption);

            _mockMessageRepository.Received(2).CreateNewEntity(Arg.Any<Message>());
        }

        [TestMethod]
        public void SendMessage_WithOptionId1_ResetsBotStrategy()
        {
            var activeChat = new Chat(1, 101, ChatStatus.Active);
            _mockChatRepository.GetById(1).Returns(activeChat);
            var restartOption = new FAQOption("Restart", 1);
            var expectedReply = new BotMessage.BotMessageBuilder(_realBotEngine, activeChat, 1).WithMessage("Restarting").Build();
            _mockStrategy.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>()).Returns(expectedReply);

            _messageService.SendMessage(1, _testSender, restartOption);

            _mockStrategy.Received(1).ResetCurrentlyActiveConversationNodeToInitialStartingPoint();
        }

        [TestMethod]
        public void GetMessage_ForWrongChat_ThrowsInvalidOperationException()
        {
            var wrongChat = new Chat(99, 101, ChatStatus.Active);
            var chatMessage = new Message(_testSender, wrongChat, "Text");
            _mockMessageRepository.GetById(5).Returns(chatMessage);

            Assert.ThrowsExactly<InvalidOperationException>(() => _messageService.GetMessage(1, 5));
        }

        [TestMethod]
        public void GetMessage_ForWrongChat_ThrowsCorrectErrorMessage()
        {
            var wrongChat = new Chat(99, 101, ChatStatus.Active);
            var chatMessage = new Message(_testSender, wrongChat, "Text");
            _mockMessageRepository.GetById(5).Returns(chatMessage);

            var exceptionThrown = Assert.ThrowsExactly<InvalidOperationException>(() => _messageService.GetMessage(1, 5));
            Assert.AreEqual("Message 5 does not belong to chat 1.", exceptionThrown.Message);
        }

        [TestMethod]
        public void GetMessage_WithValidMessage_ReturnsMessageText()
        {
            var correctChat = new Chat(1, 101, ChatStatus.Active);
            var chatMessage = new Message(_testSender, correctChat, "Correct Text");
            _mockMessageRepository.GetById(5).Returns(chatMessage);

            var resultedMessage = _messageService.GetMessage(1, 5);

            Assert.AreEqual("Correct Text", resultedMessage.GetMessage());
        }

        [TestMethod]
        public void GetAllMessages_WhenCalled_ReturnsCorrectCount()
        {
            var firstChat = new Chat(1, 101, ChatStatus.Active);
            var firstMessage = new Message(1, _testSender, firstChat, "A", DateTimeOffset.UtcNow);
            var secondMessage = new Message(2, _testSender, firstChat, "B", DateTimeOffset.UtcNow);
            _mockMessageRepository.GetAll().Returns(new List<Message> { firstMessage, secondMessage });

            var resultedMessages = _messageService.GetAllMessages(1).ToList();

            Assert.AreEqual(2, resultedMessages.Count);
        }

        [TestMethod]
        public void GetAllMessages_WhenCalled_ReturnsMessagesOrderedByTimestampAscending()
        {
            var firstChat = new Chat(1, 101, ChatStatus.Active);
            var earlierMessage = new Message(1, _testSender, firstChat, "Earlier", DateTimeOffset.UtcNow.AddMinutes(-10));
            var laterMessage = new Message(2, _testSender, firstChat, "Later", DateTimeOffset.UtcNow);
            _mockMessageRepository.GetAll().Returns(new List<Message> { laterMessage, earlierMessage });

            var resultedMessages = _messageService.GetAllMessages(1).ToList();

            Assert.AreEqual("Earlier", resultedMessages[0].GetMessage());
        }

        [TestMethod]
        public void GetAllMessages_WhenCalled_FiltersOutOtherChats()
        {
            var firstChat = new Chat(1, 101, ChatStatus.Active);
            var secondChat = new Chat(2, 202, ChatStatus.Active);
            var firstMessage = new Message(1, _testSender, firstChat, "Keep", DateTimeOffset.UtcNow);
            var secondMessage = new Message(2, _testSender, secondChat, "Discard", DateTimeOffset.UtcNow);
            _mockMessageRepository.GetAll().Returns(new List<Message> { firstMessage, secondMessage });

            var resultedMessages = _messageService.GetAllMessages(1).ToList();

            Assert.AreEqual("Keep", resultedMessages[0].GetMessage());
        }
    }
}