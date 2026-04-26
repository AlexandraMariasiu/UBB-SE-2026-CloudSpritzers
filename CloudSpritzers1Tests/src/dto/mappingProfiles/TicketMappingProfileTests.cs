using System;
using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Model;
using CloudSpritzers1.Src.Model.Ticket;
using CloudSpritzers1.Src.Dto.MappingProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudSpritzers1Tests.Src.Dto.MappingProfiles
{
    [TestClass]
    public class TicketMappingProfileTests
    {
        private IMapper _mapper;
        private User _testUser;
        private TicketCategory _testCategory;
        private TicketSubcategory _testSubcategory;
        private Ticket _testTicket;

        [TestInitialize]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<TicketMappingProfile>());
            _mapper = configuration.CreateMapper();

            _testUser = new User(101, "Jane Doe", "jane@example.com");
            _testCategory = new TicketCategory(1, "Billing", TicketUrgencyLevelEnum.HIGH);
            _testSubcategory = new TicketSubcategory(10, "Refund", 99, _testCategory);

            _testTicket = new Ticket(
                5,
                _testUser,
                TicketStatusEnum.OPEN,
                _testCategory,
                _testSubcategory,
                "Refund Request",
                "I want a refund for my delayed flight.",
                new DateTime(2026, 1, 1),
                TicketUrgencyLevelEnum.HIGH
            );
        }

        [TestMethod]
        public void Map_ValidTicket_ReturnsNotNullObject()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.IsNotNull(resultDTO);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsTicketIdCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.TicketId, resultDTO.ticketId);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsCreatorAccountIdCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Creator.UserId, resultDTO.creatorAccountId);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsCreatorEmailAddressCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Creator.RetrieveConfiguredEmailAddressForBotContact(), resultDTO.creatorEmailAddress);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsUrgencyLevelCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.UrgencyLevel, resultDTO.urgencyLevel);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsCurrentStatusCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.CurrentStatus, resultDTO.currentStatus);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsCategoryIdCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Category.CategoryId, resultDTO.categoryId);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsCategoryNameCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Category.CategoryName, resultDTO.categoryName);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsSubcategoryIdCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Subcategory.SubcategoryId, resultDTO.subcategoryId);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsSubcategoryNameCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Subcategory.SubcategoryName, resultDTO.subcategoryName);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsSubjectCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Subject, resultDTO.subject);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsDescriptionCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.Description, resultDTO.description);
        }

        [TestMethod]
        public void Map_ValidTicket_MapsCreationTimestampCorrectly()
        {
            var resultDTO = _mapper.Map<TicketDTO>(_testTicket);

            Assert.AreEqual(_testTicket.CreationTimestamp, resultDTO.creationTimestamp);
        }
    }
}