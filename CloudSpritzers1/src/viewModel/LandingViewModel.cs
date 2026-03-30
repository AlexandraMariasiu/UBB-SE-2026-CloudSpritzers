using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.repository.database;
using CloudSpritzers1.src.service;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace CloudSpritzers1.src.viewmodel
{
    public partial class LandingViewModel : ObservableObject
    {
        private readonly ReviewService _reviewService;
        private readonly IMapper _mapper;

        public ObservableCollection<ReviewDTO> Reviews { get; } = new();

        public LandingViewModel(ReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            LoadReviews();
        }

        public void LoadReviews()
        {
            var reviewsFromDb = _reviewService.GetAll();
            Reviews.Clear();
    
            foreach (var review in reviewsFromDb)
            {
                int idToSearch = review.GetUser().UserId;
        
                string realName = GetUserNameFromDatabase(idToSearch);

                float average = (review.GetDutyFreeRating() +
                         review.GetFlightExperienceRating() +
                         review.GetStaffFriendlinessRating() +
                         review.GetCleanlinessRating()) / 4.0f;

                var dto = _mapper.Map<ReviewDTO>(review);

                var finalDto = dto with
                {
                    userName = realName,
                    overallRating = average
                };

                Reviews.Add(finalDto);
            }
        }

        private string GetUserNameFromDatabase(int userId)
        {
            using var conn = DBConnectionHandler.Instance.Connection;
            string query = "SELECT name FROM [User] WHERE user_id = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", userId);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return result?.ToString() ?? "Unknown User";
        }
    }
}