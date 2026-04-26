using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudSpritzers1.Src.Service;
using CloudSpritzers1.Src.Repository.Interfaces;
using CloudSpritzers1.Src.Model;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudSpritzers1Tests.Src.Service
{
    [TestClass()]
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);

            var initialUsers = new List<User>
            {
                new User(1, "Ion Popescu", "ion@test.com"),
                new User(2, "Maria Ioana", "maria@test.com")
            };
            _userRepository.GetAll().Returns(initialUsers);
        }

        [TestMethod()]
        public void GetById_ExistingId_ReturnsUserFromRepository()
        {
            var expectedUser = new User(1, "Ion Popescu", "ion@test.com");
            _userRepository.GetById(1).Returns(expectedUser); 

            var result = _userService.GetById(1);

            Assert.AreEqual(expectedUser, result);
            _userRepository.Received(1).GetById(1); 
        }

        [TestMethod()]
        public void GetAllUsers_ReturnsAllEntities()
        {
            var result = _userService.GetAllUsers();

            Assert.AreEqual(2, result.Count); 
            Assert.AreEqual("Ion Popescu", result[0].RetrieveConfiguredDisplayFullNameForBot()); 
            Assert.AreEqual("Maria Ioana", result[1].RetrieveConfiguredDisplayFullNameForBot());
        }

        [TestMethod()]
        public void CreateNewUser_WithValidData_CallsRepository()
        {
            int identificationNumber = 10;
            string fullName = "New User";
            string emailAddress = "new@test.com";

            _userService.CreateNewUser(identificationNumber, fullName, emailAddress);

            _userRepository.Received(1).CreateNewEntity(Arg.Is<User>(user => user.RetrieveUniqueDatabaseIdentifierForBot() == identificationNumber));
        }

        [TestMethod()]
        public void ValidateUserIntegrity_NullUser_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() =>
                _userService.ValidateUserIntegrity(null!));
        }

        [TestMethod()]
        public void ValidateUserIntegrity_DuplicateUser_ThrowsArgumentException()
        {
            var existingUser = _userService.GetAllUsers().First();

            var exceptionThrown = Assert.ThrowsExactly<ArgumentException>(() =>
                _userService.ValidateUserIntegrity(existingUser));

            StringAssert.Contains("User already exists", exceptionThrown.Message);
        }

        [TestMethod()]
        public void ValidateUserIntegrity_EmptyName_ThrowsArgumentException()
        {
            var userWithEmptyName = new User(1, "", "email@test.com");

            var exceptionThrown = Assert.ThrowsExactly<ArgumentException>(() =>
                _userService.ValidateUserIntegrity(userWithEmptyName));

            StringAssert.Contains("Name cannot be null or empty", exceptionThrown.Message);
        }

        [TestMethod()]
        public void ValidateUserIntegrity_EmptyEmail_ThrowsArgumentException()
        {
            var userWithEmptyEmail = new User(1, "Nume Valid", "");

            var exceptionThrown = Assert.ThrowsExactly<ArgumentException>(() =>
                _userService.ValidateUserIntegrity(userWithEmptyEmail));

            StringAssert.Contains("Email cannot be null or empty", exceptionThrown.Message);
        }

        [TestMethod()]
        public void DeleteUserById_CallsRepository()
        {
            _userService.DeleteUserById(100);

            _userRepository.Received(1).DeleteById(100);
        }

        [TestMethod()]
        public void UpdateUserById_CallsRepositoryWithCorrectData()
        {
            int identificationNumber = 1;
            var updatedUser = new User(identificationNumber, "Nume Actualizat", "updated@test.com");

            _userService.UpdateUserById(identificationNumber, updatedUser);

            _userRepository.Received(1).UpdateById(identificationNumber, updatedUser);
        }
    }
}