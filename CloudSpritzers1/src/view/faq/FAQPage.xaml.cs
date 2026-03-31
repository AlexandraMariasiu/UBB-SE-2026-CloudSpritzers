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
using Windows.UI;
using Windows.UI.Xaml;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CloudSpritzers1.src.view.faq
{

    public enum FAQPageMode
    {
        User,
        Employee
    }
    
    public sealed partial class FAQPage : Page
        {
            private FAQPageMode _mode = FAQPageMode.User;

            public FAQPage()
            {
                this.InitializeComponent();
                ApplyMode();
            }

        public FAQPage(FAQPageMode mode)
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
                    _mode == FAQPageMode.Employee
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void AddFaqButton_Click(object sender, RoutedEventArgs e) { }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) { }

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
            }
        }



        private void EditFaqButton_Click(object sender, RoutedEventArgs e) { }

        private void HelpfulButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackThankYouText.Visibility = Visibility.Visible;

            // Yes selected = green
            HelpfulButton.Background = new SolidColorBrush(Color.FromArgb(255, 231, 248, 236));   // #E7F8EC
            HelpfulButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 34, 197, 94));    // #22C55E
            HelpfulButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 2, 122, 72));      // #027A48

            // No unselected = neutral
            NotHelpfulButton.Background = new SolidColorBrush(Color.FromArgb(255, 248, 249, 251)); // #F8F9FB
            NotHelpfulButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 209, 213, 219)); // #D1D5DB
            NotHelpfulButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 107, 114, 128));  // #6B7280
        }

        private void NotHelpfulButton_Click(object sender, RoutedEventArgs e) {
            FeedbackThankYouText.Visibility = Visibility.Visible;

            // No selected = red
            NotHelpfulButton.Background = new SolidColorBrush(Color.FromArgb(255, 254, 242, 242)); // light red
            NotHelpfulButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 239, 68, 68));   // red
            NotHelpfulButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 185, 28, 28));    // dark red

            // Yes unselected = neutral
            HelpfulButton.Background = new SolidColorBrush(Color.FromArgb(255, 248, 249, 251));     // #F8F9FB
            HelpfulButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 209, 213, 219));    // #D1D5DB
            HelpfulButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 107, 114, 128));     // #6B7280
        
        }

        private void DeleteFaqButton_Click(object sender, RoutedEventArgs e) { }
    
}
    

}
