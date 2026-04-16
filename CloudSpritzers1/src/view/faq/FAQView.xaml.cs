using System;
using System.Linq;
using AutoMapper;
using CloudSpritzers1.src.dto.mappingProfiles;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.faq;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.repository.implementations;
using CloudSpritzers1.src.service.implementation;
using CloudSpritzers1.src.viewModel.faq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


namespace CloudSpritzers1.src.view.faq
{
    public sealed partial class FAQView : Page
    {
        public FAQViewModel ViewModel { get; }

        private int _currentPersonId;


        private bool IsEmployee(int id)
        {
            try
            {
                var employeeRepository = new EmployeeRepository();
                var employee = employeeRepository.GetById(id);
                return employee != null;
            }
            catch
            {
                return false;
            }
        }

       

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

            //bool isAdmin =true; // set true for testing admin mode
            //ViewModel = new FAQViewModel(service, mapper, isAdmin);
            ViewModel = new FAQViewModel(service, mapper, false);

            DataContext = ViewModel;

            UpdateAdminVisibility();
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{


        //    base.OnNavigatedTo(e);

        //    if (e.Parameter is FAQNavigationData navData)
        //    {
        //        _currentPersonId = navData.CurrentPersonId;
        //        ViewModel.IsAdmin = navData.IsEmployee;
        //    }
        //    else
        //    {
        //        ViewModel.IsAdmin = false;
        //    }

        //    ViewModel.LoadFAQ();
        //    UpdateAdminVisibility();
        //}


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var app = (App)App.Current;

            ViewModel.IsAdmin = app.isEmployee;

            if (app.isEmployee && app.Employee != null)
                _currentPersonId = app.Employee.GetId();
            else if (app.User != null)
                _currentPersonId = app.User.GetId();

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
            this.AllQuestionsButton.Style = (Style) this.Resources["SelectedCategoryButtonStyle"];
            this.CheckInButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.ParkingButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.BaggageButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.TicketsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.FacilitiesButton.Style = (Style)this.Resources["CategoryButtonStyle"];
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.CheckIn);
            this.CheckInButton.Style = (Style)this.Resources["SelectedCategoryButtonStyle"];
            this.AllQuestionsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.ParkingButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.BaggageButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.TicketsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.FacilitiesButton.Style = (Style)this.Resources["CategoryButtonStyle"];
        }

        private void ParkingButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Parking);
            this.ParkingButton.Style = (Style)this.Resources["SelectedCategoryButtonStyle"];
            this.AllQuestionsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.CheckInButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.BaggageButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.TicketsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.FacilitiesButton.Style = (Style)this.Resources["CategoryButtonStyle"];
        }

        private void BaggageButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Baggage);
            this.BaggageButton.Style = (Style)this.Resources["SelectedCategoryButtonStyle"];
            this.AllQuestionsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.ParkingButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.CheckInButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.TicketsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.FacilitiesButton.Style = (Style)this.Resources["CategoryButtonStyle"];
        }

        private void TicketButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Tickets);
            this.TicketsButton.Style = (Style)this.Resources["SelectedCategoryButtonStyle"];
            this.AllQuestionsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.ParkingButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.BaggageButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.CheckInButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.FacilitiesButton.Style = (Style)this.Resources["CategoryButtonStyle"];
        }

        private void FacilitiesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterByCategory(FAQCategoryEnum.Facilities);
            this.FacilitiesButton.Style = (Style)this.Resources["SelectedCategoryButtonStyle"];
            this.AllQuestionsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.ParkingButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.BaggageButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.TicketsButton.Style = (Style)this.Resources["CategoryButtonStyle"];
            this.CheckInButton.Style = (Style)this.Resources["CategoryButtonStyle"];
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

            
            bool navigated = Frame.Navigate(typeof(FAQAddEditPage), new FAQNavigationData
            {
                CurrentPersonId = _currentPersonId,
                IsEmployee = ViewModel.IsAdmin,
                FAQEntry = null
            });

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

            //bool navigated = Frame.Navigate(typeof(FAQAddEditPage), ViewModel.SelectedFAQEntry);
            bool navigated = Frame.Navigate(typeof(FAQAddEditPage), new FAQNavigationData
{
    CurrentPersonId = _currentPersonId,
                IsEmployee = ViewModel.IsAdmin,
                FAQEntry = ViewModel.SelectedFAQEntry
});

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