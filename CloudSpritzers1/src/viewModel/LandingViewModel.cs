using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using AutoMapper;
using CloudSpritzers1.src.service;
using CloudSpritzers1.src.dto;

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
                var dto = _mapper.Map<ReviewDTO>(review);
                Reviews.Add(dto);
            }
        }
    }
}