using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Model;
using CloudSpritzers1.Src.Model.Chats;
using CloudSpritzers1.Src.Model.Faq.Bot;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Repository;
using CloudSpritzers1.Src.Service;
using CloudSpritzers1.Src.Service.Bot;
using CloudSpritzers1.Src.Service.Bot.Strategy;
using CloudSpritzers1.Src.Service.Interfaces;
using CloudSpritzers1.Src.ViewModel.Chats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CloudSpritzers1Tests.Src.ViewModel.Chats
{
    [TestClass]
    public class ChatViewModelTests
    {
        // Mock Repositories
        private IRepository<int, Chat> _chatRepoMock;
        private IRepository<int, Message> _msgRepoMock;
        private IBotStrategy _strategyMock;
        private IUserService _userService;
        private IMapper _mapper;

        // Real Services using mocked Repositories
        private BotEngine _botEngine;
        private MessageService _messageService;
        private ChatService _chatService;

        // Test Data
        private User _testUser;
        private Chat _testChat;

        [TestInitialize]
        public void Setup()
        {
            _chatRepoMock = Substitute.For<IRepository<int, Chat>>();
            _msgRepoMock = Substitute.For<IRepository<int, Message>>();
            _strategyMock = Substitute.For<IBotStrategy>();
            _userService = Substitute.For<IUserService>();
            _mapper = Substitute.For<IMapper>();

            _botEngine = new BotEngine(_strategyMock);
            _messageService = new MessageService(_chatRepoMock, _msgRepoMock, _botEngine);
            _chatService = new ChatService(_chatRepoMock);

            _testUser = new User(42, "Test User", "test@test.com");
            _testChat = new Chat(1, 42, ChatStatus.Active);

            _chatRepoMock.CreateNewEntity(Arg.Any<Chat>()).Returns(1);
            _chatRepoMock.GetById(1).Returns(_testChat);

            _mapper.Map<MessageDTO>(Arg.Any<IMessage>()).Returns(callInfo =>
            {
                var msg = (IMessage)callInfo[0];
                var dto = new MessageDTO();
                var sender = msg.GetSender();
                if (sender != null)
                {
                    dto.SenderId = sender.RetrieveUniqueDatabaseIdentifierForBot();
                }
                return dto;
            });

            var defaultBotReply = new BotMessage.BotMessageBuilder(_testUser, _testChat, 2).Build();
            _strategyMock.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>())
                .Returns(defaultBotReply);

            _userService.GetById(42).Returns(_testUser);
        }

        private ChatViewModel CreateViewModel(List<Message> initialMessages = null)
        {
            if (initialMessages != null)
            {
                _msgRepoMock.GetAll().Returns(initialMessages);
            }
            else
            {
                _msgRepoMock.GetAll().Returns(new List<Message>());
            }

            // Inject _testUser explicitly to bypass App.Current crash
            return new ChatViewModel(_messageService, _chatService, _mapper, _userService, _testUser);
        }

        [TestMethod]
        public void Constructor_HistoryIsEmpty_LoadsFirstMessage()
        {
            _strategyMock.ClearReceivedCalls();

            var vm = CreateViewModel(new List<Message>());

            // An empty history triggers LoadFirstMessage which queries the bot Strategy to formulate a bot response
            _strategyMock.ReceivedWithAnyArgs(1).ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(default, default);
        }

        [TestMethod]
        public void Constructor_HistoryIsNotEmpty_DoesNotLoadFirstMessage()
        {
            var mockMsg = new Message(1, _testUser, _testChat, "Test", DateTimeOffset.UtcNow);
            _strategyMock.ClearReceivedCalls();

            // Because history has 1 message, it bypasses LoadFirstMessage entirely
            var vm = CreateViewModel(new List<Message> { mockMsg });

            _strategyMock.DidNotReceiveWithAnyArgs().ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(default, default);
        }

        [TestMethod]
        public void FormatUserId_ReturnsCorrectlyFormattedString()
        {
            var vm = CreateViewModel();

            Assert.AreEqual("User Id: 42", vm.FormatUserId);
        }

        [TestMethod]
        public void CloseChat_CallsChatRepositoryUpdate()
        {
            var vm = CreateViewModel();

            vm.CloseChat();

            // Modifying chat state updates the Chat via the repository
            _chatRepoMock.Received(1).UpdateById(1, Arg.Any<Chat>());
        }

        [TestMethod]
        public void HandleOptionClick_NullOption_DoesNothing()
        {
            var mockMsg = new Message(1, _testUser, _testChat, "Init", DateTimeOffset.UtcNow);
            var vm = CreateViewModel(new List<Message> { mockMsg });
            _strategyMock.ClearReceivedCalls();

            vm.HandleOptionClickCommand.Execute(null);

            // Null option causes early return; message execution strategy should never be invoked
            _strategyMock.DidNotReceiveWithAnyArgs().ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(default, default);
        }

        [TestMethod]
        public void HandleOptionClick_ValidOption_SendsMessage()
        {
            var mockMsg = new Message(1, _testUser, _testChat, "Init", DateTimeOffset.UtcNow);
            var vm = CreateViewModel(new List<Message> { mockMsg });
            _msgRepoMock.ClearReceivedCalls();

            var option = new FAQOption("Test", 2);

            vm.HandleOptionClickCommand.Execute(option);

            // Sending a message saves exactly 2 DB records (the User's message + the Bot's algorithmic reply)
            _msgRepoMock.Received(2).CreateNewEntity(Arg.Any<Message>());
        }

        [TestMethod]
        public void HandleOptionClick_NextOptionsAvailable_AddsToCurrentOptions()
        {
            var mockMsg = new Message(1, _testUser, _testChat, "Init", DateTimeOffset.UtcNow);
            var vm = CreateViewModel(new List<Message> { mockMsg });
            var option = new FAQOption("Test", 2);
            var nextOption = new FAQOption("Next", 3);

            var botReply = new BotMessage.BotMessageBuilder(_testUser, _testChat, 2)
                .AddOption(nextOption)
                .Build();

            _strategyMock.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>()).Returns(botReply);

            vm.HandleOptionClickCommand.Execute(option);

            Assert.AreEqual(nextOption, vm.CurrentOptions[0]);
        }

        [TestMethod]
        public void HandleOptionClick_NextOptionsNull_AddsRestartChatOption()
        {
            var mockMsg = new Message(1, _testUser, _testChat, "Init", DateTimeOffset.UtcNow);
            var vm = CreateViewModel(new List<Message> { mockMsg });
            var option = new FAQOption("Test", 2);
            var botReply = new BotMessage.BotMessageBuilder(_testUser, _testChat, 2).Build();

            // Force the IEnumerable of faqOptions to null to test fallback branch inside UpdateAvailableOptions
            typeof(BotMessage).GetField("faqOptions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(botReply, null);

            _strategyMock.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>()).Returns(botReply);

            vm.HandleOptionClickCommand.Execute(option);

            Assert.AreEqual("Restart Chat", vm.CurrentOptions[0].label);
        }

        [TestMethod]
        public void LoadChatHistory_OutgoingMessage_SetsIsOutgoingTrue()
        {
            var mockMsg = new Message(1, _testUser, _testChat, "Test", DateTimeOffset.UtcNow);

            var vm = CreateViewModel(new List<Message> { mockMsg });

            Assert.IsTrue(vm.ChatHistory[0].IsOutgoing);
        }

        [TestMethod]
        public void LoadChatHistory_IncomingMessage_SetsIsOutgoingFalse()
        {
            var otherUser = new User(99, "Other", "other@other.com");
            var mockMsg = new Message(1, otherUser, _testChat, "Test", DateTimeOffset.UtcNow);

            var vm = CreateViewModel(new List<Message> { mockMsg });

            Assert.IsFalse(vm.ChatHistory[0].IsOutgoing);
        }

        [TestMethod]
        public void LoadChatHistory_SetsSenderNameCorrectly()
        {
            var otherUser = new User(99, "Other Name", "other@other.com");
            var mockMsg = new Message(1, otherUser, _testChat, "Test", DateTimeOffset.UtcNow);
            _userService.GetById(99).Returns(otherUser);

            var vm = CreateViewModel(new List<Message> { mockMsg });

            Assert.AreEqual("Other Name", vm.ChatHistory[0].SenderName);
        }
    }
}