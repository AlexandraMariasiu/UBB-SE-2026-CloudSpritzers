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

        private async void showError(string message, string title)
        {
            var dialog1 = new MaiBoule(message, title);
            dialog1.XamlRoot = this.Content.XamlRoot;
            await dialog1.ShowAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            bool isEmployee = button?.Tag?.ToString() == "Employee";
            var app = (App)App.Current;
            app.isEmployee = isEmployee;

            Frame.Navigate(typeof(EnterYourId));
        }
    }
}
