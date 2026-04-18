using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.repository.interfaces;
using CloudSpritzers1Tests.src.mockClasses;
using System.Collections.Generic;
using System.Linq;

namespace CloudSpritzers1Tests.src.repository
{
    [TestClass]
    public class UserRepositoryTests
    {
        private IUserRepository? _userRepository;

        [TestInitialize]
        public void Setup()
        {
            _userRepository = new InMemoryUserRepository();
        }

        [TestMethod]
        public void GetById_ExistingUser_ReturnsCorrectUser()
        {
            var user = new User(1, "Test Name", "test@email.com");
            _userRepository.Add(user);

            var result = _userRepository.GetById(1);

            Assert.AreEqual(user.GetFullName(), result.GetFullName());
            Assert.AreEqual(user.GetId(), result.GetId());
        }

        [TestMethod]
        public void GetById_NonExistingId_ThrowsKeyNotFoundException()
        {
            var exception = Assert.ThrowsExactly<KeyNotFoundException>(() =>
                _userRepository!.GetById(999));

            StringAssert.Contains("not found", exception.Message);
        }

        [TestMethod]
        public void Add_NullUser_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _userRepository!.Add(null!));
        }

        [TestMethod]
        public void GetAll_ReturnsAllUsers()
        {
            _userRepository.Add(new User(1, "User1", "u1@test.com"));
            _userRepository.Add(new User(2, "User2", "u2@test.com"));

            var results = _userRepository.GetAll().ToList();

            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void UpdateById_ExistingUser_UpdatesCorrectly()
        {
            var user = new User(1, "Old Name", "old@test.com");
            _userRepository!.Add(user);
            var updatedUser = new User(1, "New Name", "new@test.com");

            _userRepository.UpdateById(1, updatedUser);
            var result = _userRepository.GetById(1);

            Assert.AreEqual("New Name", result.GetFullName());
            Assert.AreEqual("new@test.com", result.GetEmailAddress());
        }

        [TestMethod]
        public void DeleteById_ExistingUser_DecreasesCount()
        {
            _userRepository!.Add(new User(1, "User1", "u1@test.com"));

            _userRepository.DeleteById(1);
            var results = _userRepository.GetAll().ToList();

            Assert.AreEqual(0, results.Count);
        }
    }
}