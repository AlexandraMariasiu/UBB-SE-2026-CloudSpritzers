using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudSpritzers1.src.repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.repository.interfaces;
using CloudSpritzers1.src.model.faq;
using CloudSpritzers1Tests.src.mockClasses;

namespace CloudSpritzers1.src.repository.implementations.Tests
{
    [TestClass()]
    public class FAQRepositoryTests
    {
        private IFAQRepository _faqRepository;

        //[AssemblyInitialize]
        //public static void AssemblyInit(TestContext _)
        //{
        //    // Option A: Load from absolute .env.test path
        //    var baseDir = AppContext.BaseDirectory;
        //    var envPath = Path.GetFullPath(Path.Combine(baseDir, "../../../../src/.env.test"));
        //    DotNetEnv.Env.Load(envPath);

        //    // Option B (most robust): set explicitly (can be used instead of Env.Load)
        //    //Environment.SetEnvironmentVariable("DB_SERVER", "DESKTOP-7CMFPVG\\SQLEXPRESS");
        //    //Environment.SetEnvironmentVariable("DB_NAME", "CloudSpritzersTest");
        //    //Environment.SetEnvironmentVariable("DB_USER", "Ama");
        //    //Environment.SetEnvironmentVariable("DB_PASS", "amalia");
        //}


        [TestInitialize]
        public void Setup()
        {
            
            _faqRepository = new InMemoryFAQRepository();
        }

        [TestMethod()]
        public void GetByIdSucceeds()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(entry);
            var expected = entry;

            var result = _faqRepository.GetById(1);
            Assert.AreEqual(expected, result);

        }

        [TestMethod()]
        public void GetByIdThrowsKeyNotFoundException()
        {
            Assert.ThrowsExactly<KeyNotFoundException>(() => _faqRepository.GetById(1));
        }

        [TestMethod()]
        public void AddFaqSucceeds()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);

            var resultId = _faqRepository.Add(entry);
            Assert.AreEqual(entry.Id, resultId);
        }

        [TestMethod()]
        public void AddFaqThrowsArgumentException()
        {
            var firstEntry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            var secondEntry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);

            _faqRepository.Add(firstEntry);

            Assert.ThrowsExactly<ArgumentException>(() => _faqRepository.Add(secondEntry));
        }

        [TestMethod()]
        public void UpdateByIdSucceeds()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(entry);

            var updatedEntry = new FAQEntry(entry.Id, entry.Question, "Only BMWs", FAQCategoryEnum.Parking, 3, entry.HelpfulVotesCount, 6);
            _faqRepository.UpdateById(entry.Id, updatedEntry);

            var updateResultEntry = _faqRepository.GetById(entry.Id);
            Assert.AreEqual(updatedEntry, updateResultEntry);
        }

        [TestMethod()]
        public void UpdateFaqThrowsKeyNotFoundException()
        {
            var firstEntry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            var updatedEntry = new FAQEntry(2, "What cars can I park here?", "Only BMWS", FAQCategoryEnum.Parking, 1, 1, 0);

            _faqRepository.Add(firstEntry);

            Assert.ThrowsExactly<KeyNotFoundException>(() => _faqRepository.UpdateById(updatedEntry.Id, updatedEntry));
        }

        [TestMethod()]
        public void DeleteByIdSucceeds()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(entry);
  
            _faqRepository.DeleteById(1);

            var newEntries = _faqRepository.GetAll().ToList();
            var expected = new List<FAQEntry>();
            CollectionAssert.AreEqual(newEntries, expected);
        }

        [TestMethod()]
        public void DeleteFaqThrowsKeyNotFoundException()
        {
            var firstEntry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(firstEntry);
            _faqRepository.DeleteById(firstEntry.Id);

            Assert.ThrowsExactly<KeyNotFoundException>(() => _faqRepository.DeleteById(firstEntry.Id));
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var expected = new List<FAQEntry>();
            var result = _faqRepository.GetAll().ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetByCategoryTest()
        {
            var expected= new List<FAQEntry>
            {
                new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0),
                new FAQEntry(2, "How much does parking cost per day?", "100 euros", FAQCategoryEnum.Parking, 200, 3, 1),
                new FAQEntry(3, "Can I bring my dog on the plane?", "Only if you buy a plane ticket for him also", FAQCategoryEnum.Baggage, 123, 34, 2),
            };
            foreach (var e in expected)
                _faqRepository.Add(e);

            var result = _faqRepository.GetByCategory(FAQCategoryEnum.All);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void IncrementViewCountTest()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(entry);
            var expected = new FAQEntry(entry.Id, entry.Question, entry.Answer, entry.Category, entry.ViewCount + 1, entry.HelpfulVotesCount, entry.NotHelpfulVotesCount);

            _faqRepository.IncrementViewCount(entry.Id);
            
            var result = _faqRepository.GetById(expected.Id);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void IncrementWasHelpfulVotesTest()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(entry);
            var expected = new FAQEntry(entry.Id, entry.Question, entry.Answer, entry.Category, entry.ViewCount, entry.HelpfulVotesCount + 1, entry.NotHelpfulVotesCount);

            _faqRepository.IncrementWasHelpfulVotes(entry.Id);
            
            var result = _faqRepository.GetById(expected.Id);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void IncrementWasNotHelpfulVotesTest()
        {
            var entry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
            _faqRepository.Add(entry);
            var expected = new FAQEntry(entry.Id, entry.Question, entry.Answer, entry.Category, entry.ViewCount, entry.HelpfulVotesCount, entry.NotHelpfulVotesCount + 1);

            _faqRepository.IncrementWasNotHelpfulVotes(entry.Id);
            
            var result = _faqRepository.GetById(expected.Id);
            Assert.AreEqual(expected, result);
        }
    }
}