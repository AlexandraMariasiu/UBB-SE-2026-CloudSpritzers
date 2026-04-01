using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace CloudSpritzers1.src.view.faq
{
    public enum FAQViewMode
    {
        User,
        Employee
    }

    public sealed partial class FAQView : Page
    {
        private FAQViewMode _mode = FAQViewMode.User;
        private bool _isFaqExpanded = false;

        public FAQView()
        {
            this.InitializeComponent();
            ApplyMode();
        }

        public FAQView(FAQViewMode mode)
        {
            this.InitializeComponent();
            _mode = mode;
            ApplyMode();
        }

        private void ApplyMode()
        {
            if (EmployeeActionsPanel != null)
            {
                EmployeeActionsPanel.Visibility =
                    _mode == FAQViewMode.Employee
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void AddFaqButton_Click(object sender, RoutedEventArgs e)
        {
           
            // Frame.Navigate(typeof(FAQAddEditPage));
        }

        private void EditFaqButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Frame.Navigate(typeof(FAQAddEditPage));
        }

        private void DeleteFaqButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete logic here later
        }

        private void OpenFaqButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag)
            {
                switch (tag)
                {
                    case "1":
                        SelectedQuestionText.Text = "What is the baggage allowance?";
                        SelectedCategoryText.Text = "Baggage Policies";
                        SelectedViewsText.Text = "3421 views";
                        SelectedAnswerText.Text = "Passengers may bring one cabin bag and one personal item. Checked baggage allowance depends on ticket type and airline policy.";
                        break;

                    case "2":
                        SelectedQuestionText.Text = "How do I buy a ticket?";
                        SelectedCategoryText.Text = "Ticket Purchasing";
                        SelectedViewsText.Text = "2654 views";
                        SelectedAnswerText.Text = "You can buy tickets through the airline website, mobile app, airport counters, or approved travel agencies.";
                        break;

                    case "3":
                        SelectedQuestionText.Text = "Is there free WiFi at the airport?";
                        SelectedCategoryText.Text = "Airport Facilities & Services";
                        SelectedViewsText.Text = "2567 views";
                        SelectedAnswerText.Text = "Yes, free WiFi is available throughout the airport. Connect to the public airport network and follow the login instructions.";
                        break;

                    case "4":
                        SelectedQuestionText.Text = "How much does airport parking cost?";
                        SelectedCategoryText.Text = "Parking Information";
                        SelectedViewsText.Text = "2301 views";
                        SelectedAnswerText.Text = "Parking prices depend on duration and parking zone. Short-stay and long-stay areas have different rates.";
                        break;

                    case "5":
                        SelectedQuestionText.Text = "Where can I park at the airport?";
                        SelectedCategoryText.Text = "Parking Information";
                        SelectedViewsText.Text = "2145 views";
                        SelectedAnswerText.Text = "The airport provides short-stay, long-stay, premium, and accessible parking near the terminals.";
                        break;
                }

                _isFaqExpanded = true;
                SelectedFaqDetailsPanel.Visibility = Visibility.Visible;
                SelectedFaqChevron.Glyph = "\uE70E";

                FeedbackThankYouText.Visibility = Visibility.Collapsed;
                ResetFeedbackButtons();
            }
        }

        private void SelectedFaqHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            _isFaqExpanded = !_isFaqExpanded;

            SelectedFaqDetailsPanel.Visibility =
                _isFaqExpanded ? Visibility.Visible : Visibility.Collapsed;

            SelectedFaqChevron.Glyph = _isFaqExpanded ? "\uE70E" : "\uE76C";
        }

        private void HelpfulButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackThankYouText.Visibility = Visibility.Visible;

            HelpfulButton.Background = new SolidColorBrush(ColorHelper.FromArgb(255, 231, 248, 236));
            HelpfulButton.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 34, 197, 94));
            HelpfulButton.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 2, 122, 72));

            NotHelpfulButton.Background = new SolidColorBrush(ColorHelper.FromArgb(255, 248, 249, 251));
            NotHelpfulButton.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 209, 213, 219));
            NotHelpfulButton.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 107, 114, 128));
        }

        private void NotHelpfulButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackThankYouText.Visibility = Visibility.Visible;

            NotHelpfulButton.Background = new SolidColorBrush(ColorHelper.FromArgb(255, 254, 242, 242));
            NotHelpfulButton.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 239, 68, 68));
            NotHelpfulButton.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 185, 28, 28));

            HelpfulButton.Background = new SolidColorBrush(ColorHelper.FromArgb(255, 248, 249, 251));
            HelpfulButton.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 209, 213, 219));
            HelpfulButton.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 107, 114, 128));
        }

        private void ResetFeedbackButtons()
        {
            HelpfulButton.Background = new SolidColorBrush(ColorHelper.FromArgb(255, 248, 249, 251));
            HelpfulButton.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 209, 213, 219));
            HelpfulButton.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 107, 114, 128));

            NotHelpfulButton.Background = new SolidColorBrush(ColorHelper.FromArgb(255, 248, 249, 251));
            NotHelpfulButton.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 209, 213, 219));
            NotHelpfulButton.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 107, 114, 128));
        }


    }
}