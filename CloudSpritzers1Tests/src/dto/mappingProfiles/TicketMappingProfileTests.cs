using System;
using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Dto.MappingProfiles;
using CloudSpritzers1.Src.Model;
using CloudSpritzers1.Src.Model.Ticket;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CloudSpritzers1Tests.Dto.MappingProfiles;

[TestClass]
public class TicketMappingProfileTests
{

    private IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<TicketMappingProfile>());
        _mapper = configuration.CreateMapper();
    }


    [TestMethod]
    public void Map_FromTicketToDTO_MapsCorrectly()
    {
        var creatorMock = new User(1, "John Doe", "johdoe@gmail.com");

        var categoryMock = new TicketCategory(1, "Software", TicketUrgencyLevelEnum.HIGH);

        var subcategoryMock = new TicketSubcategory(1, "Bug", 10, categoryMock);

        var ticket = new Ticket(1, creatorMock, TicketStatusEnum.IN_PROGRESS, categoryMock, subcategoryMock, "Issue with login", "Cannot login to the system", DateTime.UtcNow);

        var ticketDataTransferObject = _mapper.Map<TicketDTO>(ticket);
        Assert.AreEqual(ticket.TicketId, ticketDataTransferObject.ticketId);
        Assert.AreEqual(ticket.Creator.RetrieveUniqueDatabaseIdentifierForBot(), ticketDataTransferObject.creatorAccountId);
        Assert.AreEqual(ticket.Creator.RetrieveConfiguredEmailAddressForBotContact(), ticketDataTransferObject.creatorEmailAddress);
        Assert.AreEqual(ticket.UrgencyLevel, ticketDataTransferObject.urgencyLevel);
        Assert.AreEqual(ticket.CurrentStatus, ticketDataTransferObject.currentStatus);
        Assert.AreEqual(ticket.Category.CategoryId, ticketDataTransferObject.categoryId);
        Assert.AreEqual(ticket.Category.CategoryName, ticketDataTransferObject.categoryName);
        Assert.AreEqual(ticket.Subcategory.SubcategoryId, ticketDataTransferObject.subcategoryId);
        Assert.AreEqual(ticket.Subcategory.SubcategoryName, ticketDataTransferObject.subcategoryName);
        Assert.AreEqual(ticket.Subject, ticketDataTransferObject.subject);
        Assert.AreEqual(ticket.Description, ticketDataTransferObject.description);
        Assert.AreEqual(ticket.CreationTimestamp, ticketDataTransferObject.creationTimestamp);


    }
    
}