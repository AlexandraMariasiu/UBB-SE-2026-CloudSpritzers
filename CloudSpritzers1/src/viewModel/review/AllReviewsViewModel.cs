using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.repository.database;
using CloudSpritzers1.src.service;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Linq;

namespace CloudSpritzers1.src.viewModel.review
{
    public partial class AllReviewsViewModel : ObservableObject
    {
        private readonly ReviewService _reviewService;
        private readonly IMapper _mapper;

        public ObservableCollection<ReviewDTO> Reviews { get; } = new();

        [ObservableProperty]
        private int _totalReviews;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FormattedAverageDutyFree))]
        private double _averageDutyFree;
        public string FormattedAverageDutyFree => AverageDutyFree.ToString("0.0");

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FormattedAverageFlightExperience))]
        private double _averageFlightExperience;
        public string FormattedAverageFlightExperience => AverageFlightExperience.ToString("0.0");

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FormattedAverageStaffFriendliness))]
        private double _averageStaffFriendliness;
        public string FormattedAverageStaffFriendliness => AverageStaffFriendliness.ToString("0.0");

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FormattedAverageCleanliness))]
        private double _averageCleanliness;
        public string FormattedAverageCleanliness => AverageCleanliness.ToString("0.0");

        public AllReviewsViewModel(ReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            LoadData();
        }

        public void LoadData()
        {
            var reviewsFromDb = _reviewService.GetAll();
            Reviews.Clear();

            if (reviewsFromDb == null || reviewsFromDb.Count == 0) return;

            TotalReviews = reviewsFromDb.Count;
            AverageDutyFree = reviewsFromDb.Average(r => r.GetDutyFreeRating());
            AverageFlightExperience = reviewsFromDb.Average(r => r.GetFlightExperienceRating());
            AverageStaffFriendliness = reviewsFromDb.Average(r => r.GetStaffFriendlinessRating());
            AverageCleanliness = reviewsFromDb.Average(r => r.GetCleanlinessRating());

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
