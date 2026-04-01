using CloudSpritzers1.src.viewModel.review;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CloudSpritzers1.src.view.review
{
    public sealed partial class ReviewPage : Page
    {
        public AddReviewViewModel ViewModel { get; }

        public ReviewPage()
        {
            this.InitializeComponent();
            ViewModel = (App.Current as App).Services.GetService<AddReviewViewModel>();

            this.DataContext = ViewModel;
        }
    }
}

