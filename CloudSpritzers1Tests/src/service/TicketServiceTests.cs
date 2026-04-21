using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Model;
using CloudSpritzers1.Src.Model.Ticket;
using CloudSpritzers1.Src.Repository;
using CloudSpritzers1.Src.Repository.Interfaces;
using CloudSpritzers1.Src.Service;
using CloudSpritzers1.Src.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudSpritzers1Tests.Src.Service
{
    [TestClass]
    public class TicketServiceTests
    {
        private ITicketRepository _ticketRepo;
        private TicketService _ticketService;
        private User _testUser;
        private TicketCategory _testCategory;
        private TicketSubcategory _testSubcategory;

        [TestInitialize]
        public void Setup()
        {
            
            _ticketRepo = Substitute.For<ITicketRepository>();
            _ticketService = new TicketService(_ticketRepo);

            _testUser = new User(1, "Dede", "dede_the_racoon@gmail.com");
            _testCategory = new TicketCategory(1, "IT", TicketUrgencyLevelEnum.HIGH);
            _testSubcategory = new TicketSubcategory(10, "Hardware", 101, _testCategory);
        }

        [TestMethod]
        public void CreateTicket_WithValidData_CallsRepository()
        {
            var now = DateTime.Now;

            _ticketService.CreateTicket(1, _testUser, TicketStatusEnum.OPEN, _testCategory, _testSubcategory, "Subject", "Description", now);
            _ticketRepo.Received(1).CreateNewEntity(Arg.Any<Ticket>());
        }

        [TestMethod]
        public void ValidateTicket_WhenTicketIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _ticketService.ValidateTicket(null));
        }

        [TestMethod]
        public void ValidateTicket_WhenCreatorIsNull_ThrowsArgumentNullException()
        {
            var invalidTicket = new Ticket(1, null, TicketStatusEnum.OPEN, _testCategory, _testSubcategory, "Subject", "Desc", DateTime.Now);
            Assert.ThrowsExactly<ArgumentNullException>(() => _ticketService.ValidateTicket(invalidTicket));
        }

        [TestMethod]
        public void ValidateTicket_SubcategoryNotMatchingCategory_ThrowsArgumentException()
        {

            var wrongCategory = new TicketCategory(99, "ThisIsWronggggg", TicketUrgencyLevelEnum.LOW);
            var invalidTicket = new Ticket(1, _testUser, TicketStatusEnum.OPEN, wrongCategory, _testSubcategory, "Subject", "Desc", DateTime.Now);

            var ex = Assert.ThrowsExactly<ArgumentException>(() => _ticketService.ValidateTicket(invalidTicket));

        }

        [TestMethod]
        public void ValidateTicket_WithEmptySubject_ThrowsException()
        {
            var invalidTicket = new Ticket(1, _testUser, TicketStatusEnum.OPEN, _testCategory, _testSubcategory, "", "Desc", DateTime.Now);
            Assert.ThrowsExactly<ArgumentNullException>(() => _ticketService.ValidateTicket(invalidTicket)); 
        }

        [TestMethod]
        public void ValidateTicket_WithEmptyDescription_ThrowsException()
        {
            var invalidTicket = new Ticket(1, _testUser, TicketStatusEnum.OPEN, _testCategory, _testSubcategory, "Subject", "", DateTime.Now);
            Assert.ThrowsExactly<ArgumentNullException>(() => _ticketService.ValidateTicket(invalidTicket));
        }


        [TestMethod]
        public void UpdateStatus_ExistingTicket_UpdatesAndSaves()
        {
            var ticket = new Ticket(1, _testUser, TicketStatusEnum.OPEN, _testCategory, _testSubcategory, "Sub", "Desc", DateTime.Now);
            _ticketRepo.GetById(1).Returns(ticket);
            _ticketService.UpdateStatus(1, TicketStatusEnum.RESOLVED);
            Assert.AreEqual(TicketStatusEnum.RESOLVED, ticket.CurrentStatus);
            _ticketRepo.Received(1).UpdateById(1, ticket);
        }


        [TestMethod]
        public void FilterTicketsByStatus_WithInProgressFilter_ReturnsOnlyInProgressTickets()
        {
            var ticketsDto = new List<TicketDTO>
            {
                new TicketDTO(1, 1, "myoneemail", TicketUrgencyLevelEnum.HIGH, TicketStatusEnum.IN_PROGRESS, 1, "ISSbestDomain", 10, "Some subdomain", "Subj", "D1", DateTime.Now),
                new TicketDTO(2, 1, "myoneemail", TicketUrgencyLevelEnum.LOW, TicketStatusEnum.OPEN, 1, "ISSbestDomain", 10, "Some subdomain", "Subj", "D2", DateTime.Now)
            };
            var result = _ticketService.FilterTicketsByStatus(ticketsDto, TicketFilterStatusEnum.IN_PROGRESS).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(TicketStatusEnum.IN_PROGRESS, result.First().CurrentStatus);
        }

        //[TestMethod]
        //public void FilterTicketsByStatus_WithResolvedFilter_ReturnsOnlyResolvedTickets()
        //{
        //    var ticketsDto = new List<TicketDTO>
        //    {
        //        new TicketDTO(1, 1, "e1", TicketUrgencyLevelEnum.HIGH, TicketStatusEnum.RESOLVED, 1, "C1", 10, "S1", "Sub1", "D1", DateTime.Now),
        //        new TicketDTO(2, 1, "e1", TicketUrgencyLevelEnum.LOW, TicketStatusEnum.OPEN, 1, "C1", 10, "S1", "Sub2", "D2", DateTime.Now)
        //    };
        //    var result = _ticketService.FilterTicketsByStatus(ticketsDto, TicketFilterStatusEnum.RESOLVED).ToList();
        //    Assert.AreEqual(1, result.Count);
        //    Assert.AreEqual(TicketStatusEnum.RESOLVED, result.First().CurrentStatus);
        //}



        //[TestMethod]
        //public void FilterTicketsByStatus_WithOpenFilter_ReturnsOnlyOpenTickets()
        //{
        //    var ticketsDto = new List<TicketDTO>
        //    {
        //        new TicketDTO(1, 1, "e1", TicketUrgencyLevelEnum.HIGH, TicketStatusEnum.OPEN, 1, "C1", 10, "S1", "Sub1", "D1", DateTime.Now),
        //        new TicketDTO(2, 1, "e1", TicketUrgencyLevelEnum.LOW, TicketStatusEnum.RESOLVED, 1, "C1", 10, "S1", "Sub2", "D2", DateTime.Now)
        //    };
        //    var result = _ticketService.FilterTicketsByStatus(ticketsDto, TicketFilterStatusEnum.OPEN).ToList();

        //    Assert.AreEqual(1, result.Count);
        //    Assert.AreEqual(TicketStatusEnum.OPEN, result.First().CurrentStatus);
        //}


    }
}