using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.viewModel.faq;
using CloudSpritzers1.src.dto.mappingProfiles;
using AutoMapper;
using CloudSpritzers1.src.repository.implementations;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.service.implementation;
using CloudSpritzers1.src.model.faq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CloudSpritzers1.src.view.faq
{
   
    public sealed partial class FAQAddEditPage : Page
    {

        private FAQViewModel _viewModel;
        private FAQEntryDTO? _editingFaq;
        private bool _isEditMode;
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

        public FAQAddEditPage()
        {
            this.InitializeComponent();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FAQEntryMappingProfile>();
            });

            var mapper = mapperConfig.CreateMapper();
            var repository = new FAQRepository();
            var service = new FAQService(repository);

            //bool isAdmin = true; // for testing admin features
            //_viewModel = new FAQViewModel(service, mapper, isAdmin);
            _viewModel = new FAQViewModel(service, mapper, false);

        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    if (e.Parameter is FAQEntryDTO faq)
        //    {
        //        _editingFaq = faq;
        //        _isEditMode = true;

        //        QuestionTextBox.Text = faq.Question;
        //        AnswerTextBox.Text = faq.Answer;
        //        CategoryComboBox.SelectedItem = FindCategoryComboBoxItem(faq.Category);

        //        PageTitleText.Text = "Edit FAQ";
        //        PageSubtitleText.Text = "Update the selected frequently asked question entry";
        //        SaveButton.Content = "Save Changes";
        //    }
        //    else
        //    {
        //        _editingFaq = null;
        //        _isEditMode = false;

        //        PageTitleText.Text = "Add FAQ";
        //        PageSubtitleText.Text = "Create a frequently asked question entry";
        //        SaveButton.Content = "Add FAQ";
        //    }
        //}

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    if (e.Parameter is FAQNavigationData navData)
        //    {
        //        _currentPersonId = navData.CurrentPersonId;
        //        _viewModel.IsAdmin = IsEmployee(_currentPersonId);

        //        if (navData.FAQEntry != null)
        //        {
        //            var faq = navData.FAQEntry;
        //            _editingFaq = faq;
        //            _isEditMode = true;

        //            QuestionTextBox.Text = faq.Question;
        //            AnswerTextBox.Text = faq.Answer;
        //            CategoryComboBox.SelectedItem = FindCategoryComboBoxItem(faq.Category);

        //            PageTitleText.Text = "Edit FAQ";
        //            PageSubtitleText.Text = "Update the selected frequently asked question entry";
        //            SaveButton.Content = "Save Changes";
        //        }
        //        else
        //        {
        //            _editingFaq = null;
        //            _isEditMode = false;

        //            QuestionTextBox.Text = string.Empty;
        //            AnswerTextBox.Text = string.Empty;
        //            CategoryComboBox.SelectedItem = null;

        //            PageTitleText.Text = "Add FAQ";
        //            PageSubtitleText.Text = "Create a frequently asked question entry";
        //            SaveButton.Content = "Add FAQ";
        //        }
        //    }
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is FAQNavigationData navData)
            {
                _currentPersonId = navData.CurrentPersonId;
                _viewModel.IsAdmin = navData.IsEmployee;

                if (navData.FAQEntry != null)
                {
                    var faq = navData.FAQEntry;
                    _editingFaq = faq;
                    _isEditMode = true;

                    QuestionTextBox.Text = faq.Question;
                    AnswerTextBox.Text = faq.Answer;
                    CategoryComboBox.SelectedItem = FindCategoryComboBoxItem(faq.Category);

                    PageTitleText.Text = "Edit FAQ";
                    PageSubtitleText.Text = "Update the selected frequently asked question entry";
                    SaveButton.Content = "Save Changes";
                }
                else
                {
                    _editingFaq = null;
                    _isEditMode = false;

                    QuestionTextBox.Text = string.Empty;
                    AnswerTextBox.Text = string.Empty;
                    CategoryComboBox.SelectedItem = null;

                    PageTitleText.Text = "Add FAQ";
                    PageSubtitleText.Text = "Create a frequently asked question entry";
                    SaveButton.Content = "Add FAQ";
                }
            }
        }

        private ComboBoxItem? FindCategoryComboBoxItem(FAQCategoryEnum category)
        {
            foreach (var item in CategoryComboBox.Items)
            {
                if (item is ComboBoxItem comboItem &&
                    comboItem.Content?.ToString() == category.ToString())
                {
                    return comboItem;
                }
            }

            return null;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await HandleSaveChanges();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame != null && Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async System.Threading.Tasks.Task HandleSaveChanges()
        {
            string question = QuestionTextBox.Text?.Trim() ?? string.Empty;
            string answer = AnswerTextBox.Text?.Trim() ?? string.Empty;

            if (CategoryComboBox.SelectedItem is not ComboBoxItem selectedCategoryItem)
            {
                await ShowMessage("Validation error", "Please select a category.");
                return;
            }

            if (string.IsNullOrWhiteSpace(question))
            {
                await ShowMessage("Validation error", "Question cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(answer))
            {
                await ShowMessage("Validation error", "Answer cannot be empty.");
                return;
            }

            if (!Enum.TryParse<FAQCategoryEnum>(selectedCategoryItem.Content?.ToString(), out var category))
            {
                await ShowMessage("Validation error", "Invalid category selected.");
                return;
            }

            try
            {
                if (_isEditMode && _editingFaq != null)
                {
                    var updatedFaq = new FAQEntryDTO(
                        _editingFaq.Id,
                        question,
                        answer,
                        category,
                        _editingFaq.ViewCount,
                        _editingFaq.HelpfulVotesCount,
                        _editingFaq.NotHelpfulVotesCount
                    );

                    _viewModel.EditFAQEntry(updatedFaq);
                }
                else
                {
                    var newFaq = new FAQEntryDTO(
                        0,
                        question,
                        answer,
                        category,
                        0,
                        0,
                        0
                    );

                    _viewModel.AddFAQEntry(newFaq);
                }

                if (Frame != null && Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
            catch (Exception ex)
            {
                await ShowMessage("Save failed", ex.Message);
            }
        }

        private async System.Threading.Tasks.Task ShowMessage(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();

        }
    }
}
