using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Dto.MappingProfiles;
using CloudSpritzers1.Src.Model.Chats;

namespace CloudSpritzers1Tests.src.dto.mappingprofiles;

[TestClass]
public class ChatMappingProfileTests
{
    private IMapper _mapper;
    private Chat _chat;

    [TestInitialize]
    public void Setup()
    {
        var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<ChatMappingProfile>());

        _mapper = configuration.CreateMapper();
        _chat = new Chat(1, 10, ChatStatus.Active);
    }

    [TestMethod]
    public void Map_ChatToChatDTO_MapsChatIdCorrectly()
    {
        var result = _mapper.Map<ChatDTO>(_chat);

        Assert.AreEqual(_chat.ChatId, result.chatId);
    }

    [TestMethod]
    public void Map_ChatToChatDTO_MapsUserIdCorrectly()
    {
        var result = _mapper.Map<ChatDTO>(_chat);

        Assert.AreEqual(_chat.UserId, result.userId);
    }

    [TestMethod]
    public void Map_ChatToChatDTO_MapsStatusCorrectly()
    {
        var result = _mapper.Map<ChatDTO>(_chat);

        Assert.AreEqual(_chat.Status, result.status);
    }

    [TestMethod]
    public void Map_ChatToChatDTO_MapsMessageCountCorrectly()
    {
        var result = _mapper.Map<ChatDTO>(_chat);   

        Assert.AreEqual(0, result.messageCount); 
    }

    [TestMethod]
    public void Map_ChatToChatDTO_ValidConfiguration()
    {
        var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<ChatMappingProfile>());

        configuration.AssertConfigurationIsValid();
    }
}