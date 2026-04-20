using System;
using CloudSpritzers1.src.viewmodel;
using CloudSpritzers1.src.viewModel.chat;
using CloudSpritzers1.src.viewModel.general;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CloudSpritzers1.src.view.chat
{
    public sealed partial class ChatPage : Page
    {
        public ChatViewModel ViewModel { get; }
        public ChatPage()
        {
            ViewModel = (App.Current as App).Services.GetService<ChatViewModel>();
            this.InitializeComponent();
        }

        public void EndChat(object sender, RoutedEventArgs e)
        {
            ViewModel.CloseChat();
            this.Frame.Navigate(typeof(CloudSpritzers1.src.view.general.LandingPage));
        }
    }
}