using System;
using CloudSpritzers1.Src.Model;
using CloudSpritzers1.Src.Model.Employee;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CloudSpritzers1.Src.ViewModel.general
{
    public sealed partial class UpperBarViewModel : ObservableObject
    {
        private readonly User _user;
        private readonly Employee _employee;

        public UpperBarViewModel()
        {
            // Access to application state is performed inside the ViewModel layer
            _user = ((App)App.Current).User;
            _employee = ((App)App.Current).Employee;

            // Determine if the current context is a client (user) or an employee
            _isClientView = _user != null;
        }

        private bool _isClientView;
        public bool IsClientView
        {
            get => _isClientView;
            private set
            {
                if (SetProperty(ref _isClientView, value))
                {
                    // Notify dependent properties
                    OnPropertyChanged(nameof(UserDisplayLabel));
                }
            }
        }

        public string UserDisplayLabel
        {
            get
            {
                if (IsClientView && _user != null)
                {
                    return $"ID: {_user.RetrieveUniqueDatabaseIdentifierForBot()}";
                }

                if (!IsClientView && _employee != null)
                {
                    return $"ID: {_employee.RetrieveUniqueDatabaseIdentifierForBot()}";
                }

                return "Not signed in";
            }
        }

        // Navigation targets are resolved here so the view does not contain branching logic
        public Type LandingPageType => typeof(CloudSpritzers1.Src.View.general.LandingPage);
        public Type FAQPageType => typeof(CloudSpritzers1.Src.View.Faq.FAQView);
        public Type ChatPageType => typeof(CloudSpritzers1.Src.View.Chat.ChatPage);
        public Type TicketsPageType => IsClientView
            ? typeof(CloudSpritzers1.Src.View.Ticket.TicketsView)
            : typeof(CloudSpritzers1.Src.View.Ticket.TicketEmployeeView);
        public Type ReviewsPageType => IsClientView
            ? typeof(CloudSpritzers1.Src.View.Review.ReviewPage)
            : typeof(CloudSpritzers1.Src.View.Review.EmployeeSeeReviews);
        public Type ChoosingPageType => typeof(CloudSpritzers1.Src.View.general.ChoosingPage);
    }
}
