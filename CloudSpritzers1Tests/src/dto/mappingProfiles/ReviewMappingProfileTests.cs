using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Model;
using CloudSpritzers1.Src.Model.Review;
using CloudSpritzers1.Src.Dto.MappingProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CloudSpritzers1Tests.Src.Dto.MappingProfiles
{
    [TestClass]
    public class ReviewMappingProfileTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            // Initialize AutoMapper with the specific profile
            var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<ReviewMappingProfile>());
            _mapper = configuration.CreateMapper();
        }

        [TestMethod]
        public void Map_FromReviewToReviewDTO_ValidReview_AllFieldsMappedCorrectly()
        {
            var user = new User(101, "John Doe", "john@example.com");
            var sourceReview = new Review(1, user, "Great flight!", 5, 4, 3, 2);

            
            var resultDataTransferObject = _mapper.Map<ReviewDTO>(sourceReview);

            
            Assert.IsNotNull(resultDataTransferObject);
            Assert.AreEqual(sourceReview.GetId(), resultDataTransferObject.reviewId);
            Assert.AreEqual(sourceReview.GetUser().UserId, resultDataTransferObject.userId);
            Assert.AreEqual("John Doe", resultDataTransferObject.userName);
            Assert.AreEqual(sourceReview.GetMessage(), resultDataTransferObject.message);
            Assert.AreEqual(sourceReview.GetDutyFreeRating(), resultDataTransferObject.dutyFreeRating);
            Assert.AreEqual(sourceReview.GetFlightExperienceRating(), resultDataTransferObject.flightExperienceRating);
            Assert.AreEqual(sourceReview.GetStaffFriendlinessRating(), resultDataTransferObject.staffFriendlinessRating);
            Assert.AreEqual(sourceReview.GetCleanlinessRating(), resultDataTransferObject.cleanlinessRating);
        }

        [TestMethod]
        public void Map_FromReviewToReviewDTO_AllRatingsEqual_OverallRatingCalculatedCorrectly()
        {
            
            var user = new User(102, "Jane Doe", "jane@example.com");
            // All 4s should equal exactly 4.0 average
            var sourceReview = new Review(2, user, "Good", 4, 4, 4, 4);

           
            var resultDataTransferObject = _mapper.Map<ReviewDTO>(sourceReview);

            Assert.AreEqual(4.0f, resultDataTransferObject.overallRating);
        }

        [TestMethod]
        public void Map_FromReviewToReviewDTO_ZeroRatings_OverallRatingIsZero()
        {
            
            var user = new User(103, "Bob", "bob@example.com");
            var sourceReview = new Review(3, user, "Bad", 0, 0, 0, 0);

            var resultDataTransferObject = _mapper.Map<ReviewDTO>(sourceReview);

            Assert.AreEqual(0.0f, resultDataTransferObject.overallRating);
        }
    }
}
