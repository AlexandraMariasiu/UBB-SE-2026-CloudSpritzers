using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1Tests.src.mockClasses;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudSpritzers1Tests.src.repository
{
    [TestClass]
    public class ReviewRepositoryTests
    {
        private InMemoryReviewRepository _repository;
        private User _testUser;

        [TestInitialize]
        public void Setup()
        {
            
            _repository = new InMemoryReviewRepository();

          
            _testUser = new User(1, "John Doe", "john@example.com");
        }

        [TestMethod]
        public void CreateNewEntity_WhenValidReview_AddsToRepository()
        {
           
            var reviewId = 1;
            var review = new Review(reviewId, _testUser, "Excellent service!", 5, 5, 5, 5);

            
            var resultId = _repository.CreateNewEntity(review);

           
            var allReviews = _repository.GetAll();
            Assert.AreEqual(1, allReviews.Count(), "The repository should contain exactly one review.");
            Assert.AreEqual(reviewId, resultId, "The returned ID should match the review's ID.");

            var retrievedReview = _repository.GetById(reviewId);
            Assert.AreEqual("Excellent service!", retrievedReview.GetMessage());
            Assert.AreEqual(_testUser.UserId, retrievedReview.GetUser().UserId);
        }

        [TestMethod]
        public void CreateNewEntity_WhenReviewIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Review nullReview = null;

            // Act & Assert
            Assert.ThrowsExactly<ArgumentNullException>(() =>
                _repository.CreateNewEntity(nullReview)
            );
        }

        [TestMethod]
        public void GetById_WhenIdDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int nonExistentId = 999;

            // Act & Assert
            var exception = Assert.ThrowsExactly<KeyNotFoundException>(() =>
                _repository.GetById(nonExistentId)
            );

            Assert.AreEqual($"Review with id {nonExistentId} was not found.", exception.Message);
        }

        [TestMethod]
        public void UpdateById_WhenValidReview_UpdatesExistingData()
        {
            // Arrange
            var originalReview = new Review(1, _testUser, "Original Message", 3, 3, 3, 3);
            _repository.CreateNewEntity(originalReview);

            var updatedReview = new Review(1, _testUser, "Updated Message", 5, 5, 5, 5);

            // Act
            _repository.UpdateById(1, updatedReview);

            // Assert
            var result = _repository.GetById(1);
            Assert.AreEqual("Updated Message", result.GetMessage());
            Assert.AreEqual(5, result.GetDutyFreeRating());
        }

        [TestMethod]
        public void DeleteById_WhenReviewExists_RemovesFromRepository()
        {
            // Arrange
            var review = new Review(1, _testUser, "Delete me", 1, 1, 1, 1);
            _repository.CreateNewEntity(review);

            // Act
            _repository.DeleteById(1);

            // Assert
            var all = _repository.GetAll();
            Assert.AreEqual(0, all.Count());

            // Also verify it now throws an error if we try to get it
            Assert.ThrowsExactly<KeyNotFoundException>(() => _repository.GetById(1));
        }

        [TestMethod]
        public void UpdateById_WhenReviewIsNull_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsExactly<ArgumentNullException>(() =>
                _repository.UpdateById(1, null)
            );
        }

        [TestMethod]
        public void UpdateById_WhenIdDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var review = new Review(999, _testUser, "Updating non-existent", 5, 5, 5, 5);

            // Act & Assert
            Assert.ThrowsExactly<KeyNotFoundException>(() =>
                _repository.UpdateById(999, review)
            );
        }

        [TestMethod]
        public void GetAll_WhenRepositoryIsEmpty_ReturnsEmptyList()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }


    }
}