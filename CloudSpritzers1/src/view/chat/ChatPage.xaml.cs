using CloudSpritzers1.src.viewmodel;
using CloudSpritzers1.src.viewModel.chat;
using CloudSpritzers1.src.viewModel.general;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

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
       
    }
}