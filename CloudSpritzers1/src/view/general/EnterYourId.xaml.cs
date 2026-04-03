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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserId, out int parsedId))
            {
                var dialog = new YouSure($"Are you certain you are User {parsedId}?", "Dear Passenger");
                dialog.XamlRoot = this.Content.XamlRoot;

                if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                {
                    try
                    {
                        (App.Current as App).SetUser(parsedId);
                        this.Frame.Navigate(typeof(CloudSpritzers1.src.view.general.LandingPage));
                    }
                    catch (Exception ex)
                    {
                        showError("Invalid Id - does not exist", "ERROR");
                    }
                }
            }
            else
            {
                showError("You need to insert an integer.", "ERROR");
            }
        }
    }
}
