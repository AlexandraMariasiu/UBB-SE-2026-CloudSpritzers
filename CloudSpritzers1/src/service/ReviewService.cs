using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1.src.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.service
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review GetById(int id)
        {
            return _reviewRepository.GetById(id);
        }

        public int Add(Review review)
        {
            return _reviewRepository.Add(review);
        }

        public void UpdateById(int id, Review review)
        {
            _reviewRepository.UpdateById(id, review);
        }

        public void DeleteById(int id)
        {
            _reviewRepository.DeleteById(id);
        }

        public List<Review> GetAll()
        {
            return _reviewRepository.GetAll().ToList();
        }

        public void CreateReview(int id, User user, string message, int dutyFreeRating, int flightExperienceRating, int staffFriendlinessRating, int cleanlinessRating)
        {
            Review review = new(id, user, message, dutyFreeRating, flightExperienceRating, staffFriendlinessRating, cleanlinessRating);
            ValidateReview(review);
            Add(review);
        }

        public void ValidateReview(Review review)
        {
            ArgumentNullException.ThrowIfNull(review);
            if (this.GetAll().Contains(review))
                throw new ArgumentException("Review already exists");
            if (review.GetUser() == null)
                throw new ArgumentException("User cannot be null");
            if (string.IsNullOrEmpty(review.GetMessage()))
                throw new ArgumentException("Message cannot be null or empty");
            if (review.GetDutyFreeRating() < 1 || review.GetDutyFreeRating() > 5)
                throw new ArgumentException("Duty Free Rating must be between 1 and 5");
            if (review.GetFlightExperienceRating() < 1 || review.GetFlightExperienceRating() > 5)
                throw new ArgumentException("Flight Experience Rating must be between 1 and 5");
            if (review.GetStaffFriendlinessRating() < 1 || review.GetStaffFriendlinessRating() > 5)
                throw new ArgumentException("Staff Friendliness Rating must be between 1 and 5");
            if (review.GetCleanlinessRating() < 1 || review.GetCleanlinessRating() > 5)
                throw new ArgumentException("Cleanliness Rating must be between 1 and 5");
            // maybe more validation on the user

        }
    }
}
