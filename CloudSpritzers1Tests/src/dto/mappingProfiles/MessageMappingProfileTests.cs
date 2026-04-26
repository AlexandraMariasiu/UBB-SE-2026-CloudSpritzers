using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Dto.MappingProfiles;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Model.Chats;
using CloudSpritzers1.Src.Model;
using System;

namespace CloudSpritzers1Tests.src.dto.mappingprofiles;

[TestClass]
public class MessageMappingProfileTests
{
    private IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<MessageMappingProfile>());

        _mapper = configuration.CreateMapper();
    }

    [TestMethod]
    public void Map_MessageToMessageDTO_Succeeds()
    {
        var user = new User(1, "Alex", "alex@mail.com");
        var chat = new Chat(10, 1, ChatStatus.Active);
        var message = new Message(user, chat, "Hello");

        var result = _mapper.Map<MessageDTO>(message);

        Assert.AreEqual("Hello", result.MessageText);
        Assert.AreEqual(chat.ChatId, result.ChatId);
        Assert.AreEqual(user.RetrieveUniqueDatabaseIdentifierForBot(), result.SenderId);
        Assert.AreEqual(user.RetrieveConfiguredDisplayFullNameForBot(), result.SenderName);

        Assert.IsTrue(result.Timestamp != default);

        Assert.IsNotNull(result.FaqOptions);
    }

    [TestMethod]
    public void Configuration_IsValid()
    {
        var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<MessageMappingProfile>());

        configuration.AssertConfigurationIsValid();
    }
}