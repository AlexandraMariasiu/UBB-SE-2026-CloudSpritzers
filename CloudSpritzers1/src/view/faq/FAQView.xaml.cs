using System;
using System.Linq;
using AutoMapper;
using CloudSpritzers1.src.dto.mappingProfiles;
using CloudSpritzers1.src.DTO;
using CloudSpritzers1.src.model.faq;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.service;
using CloudSpritzers1.src.viewModel.faq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace CloudSpritzers1.src.view.faq
{
    public sealed partial class FAQView : Page
    {
        public FAQViewModel ViewModel { get; }

        public FAQView()
        {
            this.InitializeComponent();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FAQEntryMappingProfile>();
            });

            var mapper = mapperConfig.CreateMapper();
            var repository = new FAQRepository();
            var service = new FAQService(repository);

            bool isAdmin =true; // set true for testing admin mode
            ViewModel = new FAQViewModel(service, mapper, isAdmin);

            DataContext = ViewModel;

            UpdateAdminVisibility();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.LoadFAQ();
            UpdateAdminVisibility();
        }

        private void OpenFaqButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FAQEntryDTO faq)
            {
                ViewModel.ToggleFAQ(faq);
            }
        }

        private void AccordionHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FAQEntryDTO faq)
            {
                ViewModel.ToggleFAQ(faq);
            }
        }

        private void AllQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.All);
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.CheckIn);
        }

        private void ParkingButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Parking);
        }

        private void BaggageButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Baggage);
        }

        private void TicketButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Tickets);
        }

        private void FacilitiesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Facilities);
        }

        private async void AddFaqButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame == null)
            {
                var dialog = new ContentDialog
                {
                    Title = "Navigation error",
                    Content = "Frame is null. FAQAddEditPage cannot open.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
                return;
            }

            bool navigated = Frame.Navigate(typeof(FAQAddEditPage));

            if (!navigated)
            {
                var dialog = new ContentDialog
                {
                    Title = "Navigation error",
                    Content = "Navigate returned false.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }

        private async void EditFaqButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedFAQEntry == null)
            {
                var dialog = new ContentDialog
                {
                    Title = "No FAQ selected",
                    Content = "Please open an FAQ first, then click Edit.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
                return;
            }

            if (Frame == null)
            {
                var dialog = new ContentDialog
                {
                    Title = "Navigation error",
                    Content = "Frame is null. FAQAddEditPage cannot open.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
                return;
            }

            bool navigated = Frame.Navigate(typeof(FAQAddEditPage), ViewModel.SelectedFAQEntry);

            if (!navigated)
            {
                var dialog = new ContentDialog
                {
                    Title = "Navigation error",
                    Content = "Navigate returned false.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }

        private async void DeleteFaqButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedFAQEntry == null)
                return;

            var faq = ViewModel.SelectedFAQEntry;

            var dialog = new ContentDialog
            {
                Title = "Delete FAQ",
                Content = $"Are you sure you want to delete \"{faq.Question}\"?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                ViewModel.DeleteFAQEntry(faq);
                ViewModel.SelectedFAQEntry = null;
            }
        }

        private void HelpfulButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is FAQEntryDTO faq)
            {
                ViewModel.SelectedFAQEntry = faq;
                ViewModel.IncrementWasHelpfulVotes();

                faq.IsHelpfulSelected = true;
                faq.IsNotHelpfulSelected = false;
                faq.HasFeedback = true;
            }
        }

        private void NotHelpfulButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is FAQEntryDTO faq)
            {
                ViewModel.SelectedFAQEntry = faq;
                ViewModel.IncrementWasNotHelpfulVotes();

                faq.IsHelpfulSelected = false;
                faq.IsNotHelpfulSelected = true;
                faq.HasFeedback = true;
            }
        }

        private void UpdateAdminVisibility()
        {
            EmployeeActionsPanel.Visibility = ViewModel.IsAdmin
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}