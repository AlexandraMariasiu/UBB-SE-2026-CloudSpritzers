using CloudSpritzers1.src.viewModel.review;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CloudSpritzers1.src.view.review
{
    public sealed partial class EmployeeSeeReviews : Page
    {
        public AllReviewsViewModel ViewModel { get; }

        public EmployeeSeeReviews()
        {
            this.InitializeComponent();

            ViewModel = (App.Current as App).Services.GetService<AllReviewsViewModel>();

            this.DataContext = ViewModel;
        }
    }
}