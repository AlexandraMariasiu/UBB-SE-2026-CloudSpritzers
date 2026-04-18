using CloudSpritzers1.src.viewmodel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace CloudSpritzers1.src.view.general
{
    public sealed partial class ChoosingPage : Page
    {

        public ChoosingPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays an error dialog with the specified message and title.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="title">The title of the error dialog.</param>
        private async void DisplayErrorMessage(string message, string title)
        {
            var dialog1 = new MaiBoule(message, title);
            dialog1.XamlRoot = this.Content.XamlRoot;
            await dialog1.ShowAsync();
        }

        /// <summary>
        /// Handles the click event for user role selection buttons.
        /// Sets the application's user role based on the button's Tag and navigates to the EnterYourId page.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">Event data for the click event.</param>
        private async void SelectUserRole_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            bool isEmployee = button?.Tag?.ToString() == "Employee";
            var app = (App)App.Current;
            app.isEmployee = isEmployee;

            Frame.Navigate(typeof(EnterYourId));
        }
    }
}


