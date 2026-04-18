using CloudSpritzers1.src.view.chat;
using CloudSpritzers1.src.view.general;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace CloudSpritzers1.src.view.general
{
    public sealed partial class EnterYourId : Page
    {
        public string UserId { get; set; }
        public EnterYourId()
        {        
            this.InitializeComponent();
        }

        private async void showError(string message, string title)
        {
            var dialog1 = new MaiBoule(message, title);
            dialog1.XamlRoot = this.Content.XamlRoot;
            await dialog1.ShowAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs eventArguments)
        {
            if (int.TryParse(UserId, out int parsedId))
            {
                var confirmationDialog = new YouSure($"Are you certain you are ID {parsedId}?", "Confirmation");
                confirmationDialog.XamlRoot = this.Content.XamlRoot;

                if (await confirmationDialog.ShowAsync() == ContentDialogResult.Primary)
                {
                    // Now we use the boolean result from the fixed App method
                    bool authenticationSuccessful = ((App)App.Current).SetUser(parsedId);

                    if (authenticationSuccessful)
                    {
                        this.Frame.Navigate(typeof(LandingPage));
                    }
                    else
                    {
                        showError("The ID entered does not exist.", "ERROR");
                    }
                }
            }
            else
            {
                // This block executes if int.TryParse returns false (input is not a valid integer)
                showError("Please enter a valid numeric ID.", "FORMAT ERROR");
            }
        }

        /// <summary>
        /// Navigates the user back to the initial choosing page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if the frame can navigate back, otherwise navigate explicitly
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            else
            {
                // Explicitly naming the target class (ChoosingPage)
                this.Frame.Navigate(typeof(CloudSpritzers1.src.view.general.ChoosingPage));
            }
        }
    }
}
