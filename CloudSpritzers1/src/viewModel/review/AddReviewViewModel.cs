using CloudSpritzers1.src.model;
using CloudSpritzers1.src.service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;

namespace CloudSpritzers1.src.viewModel.review
{
    public partial class AddReviewViewModel : ObservableObject
    {
        private readonly ReviewService _reviewService;
        private readonly UserService _userService;

        public event EventHandler<string>? AlertRequested;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitReviewCommand))]
        [NotifyPropertyChangedFor(nameof(DutyText))]
        private int _dutyRating;
        public string DutyText => DutyRating > 0 ? $"{DutyRating}/5" : "Not rated";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitReviewCommand))]
        [NotifyPropertyChangedFor(nameof(FlightText))]
        private int _flightRating;
        public string FlightText => FlightRating > 0 ? $"{FlightRating}/5" : "Not rated";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitReviewCommand))]
        [NotifyPropertyChangedFor(nameof(StaffText))]
        private int _staffRating;
        public string StaffText => StaffRating > 0 ? $"{StaffRating}/5" : "Not rated";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitReviewCommand))]
        [NotifyPropertyChangedFor(nameof(CleanText))]
        private int _cleanRating;
        public string CleanText => CleanRating > 0 ? $"{CleanRating}/5" : "Not rated";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitReviewCommand))]
        [NotifyPropertyChangedFor(nameof(CharCountText))]
        private string _reviewMessage = string.Empty;
        public string CharCountText => $"{ReviewMessage?.Length ?? 0} characters";

        public AddReviewViewModel(ReviewService reviewService, UserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        private bool CanSubmitReview()
        {
            return DutyRating > 0 &&
                   FlightRating > 0 &&
                   StaffRating > 0 &&
                   CleanRating > 0 &&
                   !string.IsNullOrWhiteSpace(ReviewMessage);
        }

        [RelayCommand(CanExecute = nameof(CanSubmitReview))]
        private void SubmitReview()
        {
            try
            {
                //TODO: Replace with actual logged-in user retrieval logic

                User sampleUser = _userService.GetById(2);
                _reviewService.CreateReview(1, sampleUser, ReviewMessage, DutyRating, FlightRating, StaffRating, CleanRating);
            
             
                DutyRating = 0;
                FlightRating = 0;
                StaffRating = 0;
                CleanRating = 0;
                ReviewMessage = string.Empty;

                Debug.WriteLine("Review successfully submitted!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error submitting review: {ex.Message}");
            }
        }
    }
}