using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

using CloudSpritzers1.src.view.general;


namespace CloudSpritzers1.src.view.general
{
    public sealed partial class EnterYourId : Page
    {
        public string UserId { get; set; }
        public EnterYourId()
        {        
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserId, out int parsedId))
            {
                var dialog = new YouSure($"Are you certain you are User {parsedId}?", "Boss");
                dialog.XamlRoot = this.Content.XamlRoot;

                if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                {
                    (App.Current as App).SetUser(parsedId);
                }
            }
        }
    }
}
