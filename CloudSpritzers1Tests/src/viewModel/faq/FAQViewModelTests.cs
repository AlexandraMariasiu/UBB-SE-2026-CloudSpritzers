using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.dto.mappingProfiles;
using CloudSpritzers1.src.model.faq;
using CloudSpritzers1.src.repository.interfaces;
using CloudSpritzers1.src.service.implementation;
using CloudSpritzers1.src.service.interfaces;
using CloudSpritzers1.src.viewModel.faq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.Core;

namespace CloudSpritzers1.src.viewModel.faq
{
    [TestClass]
    public class FAQViewModelTests
    {
        private IFAQRepository _faqRepository;
        private IMapper _mapper;
        private IFAQService _faqService;
        private FAQViewModel _faqViewModelAdmin;
        private FAQViewModel _faqViewModelCustomer;

        [TestInitialize]
        public void Setup()
        {
            _faqRepository = Substitute.For<IFAQRepository>();
            _mapper = Substitute.For<IMapper>();
            _faqService = Substitute.For<IFAQService>();

            _mapper.Map<FAQEntryDTO>(Arg.Any<FAQEntry>()).Returns(callInfo => MapToDto((FAQEntry)callInfo[0]));
            _mapper.Map<FAQEntry>(Arg.Any<FAQEntryDTO>()).Returns(callInfo => MapToEntity((FAQEntryDTO)callInfo[0]));
            //_mapper.Map<List<FAQEntryDTO>>(Arg.Any<List<FAQEntry>>()).Returns(callInfo =>
            //    new List<FAQEntryDTO>(((List<FAQEntry>)callInfo[0]).Select(e => MapToDto(e))));

            var entries = new List<FAQEntry>
            {
                new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0),
                new FAQEntry(2, "How much does parking cost per day?", "Parking is 100 euros", FAQCategoryEnum.Parking, 2, 3, 1),
                new FAQEntry(3, "Can I bring my dog on the plane?", "Only if you buy a ticket for him also", FAQCategoryEnum.Baggage, 3, 4, 2),
            };
        
            _faqService.GetAll().Returns(entries);
            _faqService.FilterFAQEntry(Arg.Any<FAQCategoryEnum>(), Arg.Any<string>()).Returns(entries);

            _faqViewModelAdmin = new FAQViewModel(_faqService, _mapper, true);
            _faqViewModelCustomer = new FAQViewModel(_faqService, _mapper, false);
        }

        [TestMethod]
        public void ConstructorLoadsFAQs()
        {
            var allFAQs = _faqService.GetAll();
            _faqService.FilterFAQEntry(Arg.Any<FAQCategoryEnum>(), Arg.Any<string>()).Returns(allFAQs);

            Assert.AreEqual(3, _faqViewModelCustomer.FAQs.Count);
            Assert.AreEqual(3, _faqViewModelCustomer.FilteredFAQs.Count);
            Assert.AreEqual(FAQCategoryEnum.All, _faqViewModelCustomer.SelectedCategory);
            Assert.AreEqual(string.Empty, _faqViewModelCustomer.SearchQuery);

            AssertFaqMatches(_faqViewModelCustomer.FAQs[0], allFAQs[0]);
            AssertFaqMatches(_faqViewModelCustomer.FilteredFAQs[2], allFAQs[2]);
        }

        [TestMethod]
        public void SearchByQuestionOrAnswer()
        {
            var searchResults = new List<FAQEntry>
            {
                new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0),
                new FAQEntry(2, "How much does parking cost per day?", "Parking is 100 euros", FAQCategoryEnum.Parking, 2, 3, 1),
            };
            _faqService.FilterFAQEntry(FAQCategoryEnum.All, "park").Returns(searchResults);

            _faqViewModelCustomer.SearchQuery = "park";

            Assert.AreEqual(2, _faqViewModelCustomer.FilteredFAQs.Count);
            CollectionAssert.AreEqual(new[] { 1, 2 }, _faqViewModelCustomer.FilteredFAQs.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void FilterByCategory()
        {
            var parkingEntries = new List<FAQEntry>
            {
                new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0),
                new FAQEntry(2, "How much does parking cost per day?", "Parking is 100 euros", FAQCategoryEnum.Parking, 2, 3, 1),
            };
            _faqService.FilterFAQEntry(FAQCategoryEnum.Parking, Arg.Any<string>()).Returns(parkingEntries);
            _faqViewModelCustomer.FilterByCategory(FAQCategoryEnum.Parking);

            Assert.AreEqual(FAQCategoryEnum.Parking, _faqViewModelCustomer.SelectedCategory);
            Assert.AreEqual(2, _faqViewModelCustomer.FilteredFAQs.Count);
            CollectionAssert.AreEqual(new[] { 1, 2 }, _faqViewModelCustomer.FilteredFAQs.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void AddFAQEntryAsAdminSucceeds()
        {
            var newEntry = new FAQEntry(4, "How much can the baggage on the plane be?", "10kg", FAQCategoryEnum.Baggage, 0, 0, 0);
            var allFAQs = new List<FAQEntry>
            {
                new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0),
                new FAQEntry(2, "How much does parking cost per day?", "Parking is 100 euros", FAQCategoryEnum.Parking, 2, 3, 1),
                new FAQEntry(3, "Can I bring my dog on the plane?", "Only if you buy a ticket for him also", FAQCategoryEnum.Baggage, 3, 4, 2),
                newEntry
            };

            var newDto = MapToDto(newEntry);

            _faqViewModelAdmin.AddFAQEntry(newDto);

            _faqService.Received(1).AddFAQEntry(Arg.Is<FAQEntry>(x => x.Id == newEntry.Id && x.Question == newEntry.Question));
            Assert.AreEqual(3, _faqViewModelAdmin.FAQs.Count);
            Assert.AreEqual(3, _faqViewModelAdmin.FilteredFAQs.Count);
        }

        [TestMethod]
        public void AddFAQEntryNotAdminThrowsUnauthorizedAccessException()
        {
            Assert.ThrowsExactly<UnauthorizedAccessException>(() => _faqViewModelCustomer.AddFAQEntry(MapToDto(new FAQEntry(4, "Q", "A", FAQCategoryEnum.Baggage, 0, 0, 0))));
            _faqService.DidNotReceive().AddFAQEntry(Arg.Any<FAQEntry>());
        }

        [TestMethod]
        public void EditFAQEntryAsAdminSucceeds()
        {
            var updatedEntry = new FAQEntry(1, "What cars can I park here?", "Only BMWs", FAQCategoryEnum.Parking, 5, 2, 1);
            var allFAQs = new List<FAQEntry>
            {
                updatedEntry,
                new FAQEntry(2, "How much does parking cost per day?", "Parking is 100 euros", FAQCategoryEnum.Parking, 2, 3, 1),
                new FAQEntry(3, "Can I bring my dog on the plane?", "Only if you buy a ticket for him also", FAQCategoryEnum.Baggage, 3, 4, 2),
            };
            _faqService.GetAll().Returns(allFAQs);

            var updatedDto = MapToDto(updatedEntry);

            _faqViewModelAdmin.EditFAQEntry(updatedDto);

            _faqService.Received(1).EditFAQEntry(Arg.Is<FAQEntry>(x => x.Id == 1), 1);
            Assert.AreEqual("Only BMWs", _faqViewModelAdmin.FAQs.First(x => x.Id == 1).Answer);
        }

        [TestMethod]
        public void EditFAQEntryNotAdminThrowsUnauthorizedAccessException()
        {
            Assert.ThrowsExactly<UnauthorizedAccessException>(() => _faqViewModelCustomer.EditFAQEntry(MapToDto(new FAQEntry(4, "Q", "A", FAQCategoryEnum.Baggage, 0, 0, 0))));
            _faqService.DidNotReceive().EditFAQEntry(Arg.Any<FAQEntry>(), Arg.Any<int>());
        }

        [TestMethod]
        public void EditFAQEntryThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _faqViewModelAdmin.EditFAQEntry(null));
            _faqService.DidNotReceive().EditFAQEntry(Arg.Any<FAQEntry>(), Arg.Any<int>());
        }

        [TestMethod]
        public void DeleteFAQEntryAsAdminSucceeds()
        {
            var entryToDelete = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            var updatedEntries = new List<FAQEntry>
            {
                new FAQEntry(2, "How much does parking cost per day?", "Parking is 100 euros", FAQCategoryEnum.Parking, 2, 3, 1),
                new FAQEntry(3, "Can I bring my dog on the plane?", "Only if you buy a ticket for him also", FAQCategoryEnum.Baggage, 3, 4, 2),
            };
            _faqService.GetAll().Returns(updatedEntries);

            var entryToDeleteDto = MapToDto(entryToDelete);

            _faqViewModelAdmin.DeleteFAQEntry(entryToDeleteDto);

            _faqService.Received(1).DeleteFAQEntry(entryToDelete.Id);
            Assert.AreEqual(2, _faqViewModelAdmin.FAQs.Count);
        }

        [TestMethod]
        public void DeleteFAQEntryNotAdminThrowsUnauthorizedAccessException()
        {
            Assert.ThrowsExactly<UnauthorizedAccessException>(() => _faqViewModelCustomer.DeleteFAQEntry(MapToDto(new FAQEntry(4, "Q", "A", FAQCategoryEnum.Baggage, 0, 0, 0))));
            _faqService.DidNotReceive().DeleteFAQEntry(Arg.Any<int>());
        }

        [TestMethod]
        public void DeleteFAQEntryThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _faqViewModelAdmin.DeleteFAQEntry(null));
            _faqService.DidNotReceive().DeleteFAQEntry(Arg.Any<int>());
        }

        [TestMethod]
        public void ToggleFAQExpandsEntryAndIncrementsViewCount()
        {
            var firstFaq = _faqViewModelCustomer.FilteredFAQs[0];
            var secondFaq = _faqViewModelCustomer.FilteredFAQs[1];

            _faqViewModelCustomer.ToggleFAQ(firstFaq);

            Assert.IsTrue(firstFaq.IsExpanded);
            Assert.IsFalse(secondFaq.IsExpanded);
            Assert.AreEqual(firstFaq, _faqViewModelCustomer.SelectedFAQEntry);
            Assert.AreEqual(2, _faqViewModelCustomer.FAQs.First(x => x.Id == firstFaq.Id).ViewCount);
            Assert.AreEqual(2, _faqViewModelCustomer.FilteredFAQs.First(x => x.Id == firstFaq.Id).ViewCount);
            _faqService.Received(1).IncrementViewCount(Arg.Any<FAQEntry>());
        }

        [TestMethod]
        public void ToggleFAQCalledForNullEntityReturns()
        {
            var firstFaq = _faqViewModelCustomer.FilteredFAQs[0];
            _faqViewModelCustomer.ToggleFAQ(null);

            Assert.IsFalse(firstFaq.IsExpanded);
            _faqService.DidNotReceive().IncrementViewCount(Arg.Any<FAQEntry>());
        }

        [TestMethod]
        public void IncrementWasHelpfulVotes()
        {
            _faqViewModelCustomer.SelectedFAQEntry = _faqViewModelCustomer.FilteredFAQs[0];

            _faqViewModelCustomer.IncrementWasHelpfulVotes();

            _faqService.Received(1).IncrementWasHelpfulVotes(Arg.Any<FAQEntry>());
            Assert.AreEqual(2, _faqViewModelCustomer.SelectedFAQEntry!.HelpfulVotesCount);
        }

        [TestMethod]
        public void IncrementWasNotHelpfulVotes()
        {
            _faqViewModelCustomer.SelectedFAQEntry = _faqViewModelCustomer.FilteredFAQs[0];

            _faqViewModelCustomer.IncrementWasNotHelpfulVotes();

            _faqService.Received(1).IncrementWasNotHelpfulVotes(Arg.Any<FAQEntry>());
            Assert.AreEqual(1, _faqViewModelCustomer.SelectedFAQEntry!.NotHelpfulVotesCount);
        }

        [TestMethod]
        public void IncrementViewCount_WithNoSelectedFAQ_DoesNothing()
        {
            _faqViewModelCustomer.IncrementViewCount();

            _faqService.DidNotReceive().IncrementViewCount(Arg.Any<FAQEntry>());
        }

        private static FAQEntryDTO MapToDto(FAQEntry entry)
        {
            return new FAQEntryDTO(
                entry.Id,
                entry.Question,
                entry.Answer,
                entry.Category,
                entry.ViewCount,
                entry.HelpfulVotesCount,
                entry.NotHelpfulVotesCount);
        }

        private static FAQEntry MapToEntity(FAQEntryDTO dto)
        {
            return new FAQEntry(
                dto.Id,
                dto.Question,
                dto.Answer,
                dto.Category,
                dto.ViewCount,
                dto.HelpfulVotesCount,
                dto.NotHelpfulVotesCount);
        }

        private static void AssertFaqMatches(FAQEntryDTO actual, FAQEntry expected)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Question, actual.Question);
            Assert.AreEqual(expected.Answer, actual.Answer);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.ViewCount, actual.ViewCount);
            Assert.AreEqual(expected.HelpfulVotesCount, actual.HelpfulVotesCount);
            Assert.AreEqual(expected.NotHelpfulVotesCount, actual.NotHelpfulVotesCount);
        }
    }
}
