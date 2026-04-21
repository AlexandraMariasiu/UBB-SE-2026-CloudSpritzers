using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.service;
using CommunityToolkit.Mvvm.ComponentModel;

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

            if (reviewsFromDb == null || reviewsFromDb.Count == 0)
            {
                return;
            }

            TotalReviews = reviewsFromDb.Count;

            CalculateCategoryAverages(reviewsFromDb);

            var mappedReviews = _mapper.Map<List<ReviewDTO>>(reviewsFromDb);

            foreach (var reviewDataTransferObject in mappedReviews)
            {
                Reviews.Add(reviewDataTransferObject);
            }
        }

        private void CalculateCategoryAverages(List<model.review.Review> reviews)
        {
            AverageDutyFree = reviews.Average(review => review.GetDutyFreeRating());
            AverageFlightExperience = reviews.Average(review => review.GetFlightExperienceRating());
            AverageStaffFriendliness = reviews.Average(review => review.GetStaffFriendlinessRating());
            AverageCleanliness = reviews.Average(review => review.GetCleanlinessRating());
        }
    }
}
