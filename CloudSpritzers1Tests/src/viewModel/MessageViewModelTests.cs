using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CloudSpritzers1.Src.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CloudSpritzers1Tests.Src.ViewModel
{
    [TestClass]
    public class MessageViewModelTests
    {
        private IRepository<int, Chat> _chatRepoMock;
        private IRepository<int, Message> _msgRepoMock;
        private IBotStrategy _strategyMock;
        private IUserService _userServiceMock;
        private IMapper _mapperMock;

        private BotEngine _botEngine;
        private MessageService _messageService;

        private User _testUser;
        private Chat _testChat;
        private int _testChatId = 1;
        private int _testUserId = 42;

        [TestInitialize]
        public void Setup()
        {
            _chatRepoMock = Substitute.For<IRepository<int, Chat>>();
            _msgRepoMock = Substitute.For<IRepository<int, Message>>();
            _strategyMock = Substitute.For<IBotStrategy>();
            _userServiceMock = Substitute.For<IUserService>();
            _mapperMock = Substitute.For<IMapper>();

            _botEngine = new BotEngine(_strategyMock);
            _messageService = new MessageService(_chatRepoMock, _msgRepoMock, _botEngine);

            _testUser = new User(_testUserId, "Test User", "test@test.com");
            _testChat = new Chat(_testChatId, _testUserId, ChatStatus.Active);

            _chatRepoMock.GetById(_testChatId).Returns(_testChat);
            _userServiceMock.GetById(_testUserId).Returns(_testUser);

            var botReply = new BotMessage.BotMessageBuilder(_testUser, _testChat, 2).Build();
            _strategyMock.ProcessIncomingUserMessageAndDetermineNextDecisionTreeNode(Arg.Any<BotEngine>(), Arg.Any<IMessage>()).Returns(botReply);

            _mapperMock.Map<MessageDTO>(Arg.Any<IMessage>()).Returns(new MessageDTO());
        }

        private MessageViewModel CreateViewModel()
        {
            return new MessageViewModel(_messageService, _userServiceMock, _mapperMock, _testChatId, _testUserId);
        }

        [TestMethod]
        public void Constructor_CallsLoadMessages_WhenNoMessagesInDb_MessagesCollectionIsEmpty()
        {
            _msgRepoMock.GetAll().Returns(new List<Message>());

            var vm = CreateViewModel();

            Assert.AreEqual(0, vm.Messages.Count);
        }

        [TestMethod]
        public void LoadMessages_WhenMessagesExistInDb_PopulatesObservableCollection()
        {
            var testMsg = new Message(1, _testUser, _testChat, "Hello", DateTimeOffset.UtcNow);
            _msgRepoMock.GetAll().Returns(new List<Message> { testMsg });

            var vm = CreateViewModel();

            Assert.AreEqual(1, vm.Messages.Count);
        }

        [TestMethod]
        public void LoadMessages_WhenCalledTwice_ClearsPreviousMessagesBeforeAdding()
        {
            var testMsg = new Message(1, _testUser, _testChat, "Hello", DateTimeOffset.UtcNow);
            _msgRepoMock.GetAll().Returns(new List<Message> { testMsg });

            var vm = CreateViewModel();

            vm.LoadMessages();

            Assert.AreEqual(1, vm.Messages.Count);
        }

        [TestMethod]
        public void SendMessage_WhenOptionIsNull_ThrowsArgumentNullException()
        {
            var vm = CreateViewModel();

            try 
            {
                vm.SendMessage(null);
                Assert.Fail("Expected ArgumentNullException was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                
            }
        }

        [TestMethod]
        public void SendMessage_WhenOptionIsValid_FetchesCurrentUser()
        {
            var vm = CreateViewModel();
            var option = new FAQOption("Test Option", 1);
            _userServiceMock.ClearReceivedCalls();

            vm.SendMessage(option);

            _userServiceMock.Received(1).GetById(_testUserId);
        }

        [TestMethod]
        public void SendMessage_WhenOptionIsValid_MapsBothUserMessageAndBotReply()
        {
            var vm = CreateViewModel();
            var option = new FAQOption("Test Option", 1);
            _mapperMock.ClearReceivedCalls();

            vm.SendMessage(option);

            _mapperMock.ReceivedWithAnyArgs(2).Map<MessageDTO>(default);
        }

        [TestMethod]
        public void SendMessage_WhenOptionIsValid_AddsTwoItemsToMessagesCollection()
        {
            _msgRepoMock.GetAll().Returns(new List<Message>());
            var vm = CreateViewModel(); 
            var option = new FAQOption("Test Option", 1);

            vm.SendMessage(option);

            Assert.AreEqual(2, vm.Messages.Count);
        }
    }
}