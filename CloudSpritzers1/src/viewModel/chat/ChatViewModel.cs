using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.chat;
using CloudSpritzers1.src.model.faq.bot;
using CloudSpritzers1.src.service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.viewModel.chat
{
    public sealed partial class ChatViewModel : ObservableObject
    {
        public ObservableCollection<FAQOption> CurrentOptions { get; } = new();
        private MessageService _messageService;
        private ChatService _chatService;
        private IMapper _mapper;
        private Chat _chat;
        private User _user;
        private const int _FIRST_OPTION = 1;

        public ChatViewModel(MessageService msgService,ChatService chatService, IMapper mapper) {
            _messageService = msgService;
            _chatService = chatService;
            _mapper = mapper;

            // TODO: add null guard
            _user = (App.Current as App).User; 

            _chat = _chatService.OpenChat(_user.GetId());
            
            LoadFirstMessage();

        }

        [RelayCommand]
        private void HandleOptionClick(FAQOption option)
        {
            if (option == null) return;

            
            System.Diagnostics.Debug.WriteLine($"User selected: {option.Label}");

            UpdateAvailableOptions(option.NextOptionId);

        }

        private void UpdateAvailableOptions(int nextNodeId)
        {
            //var dto = _mapper.Map<MessageDTO>();
            CurrentOptions.Clear();
            // TODO UPDATE
            CurrentOptions.Add(new FAQOption("Tell me more", 101));
            CurrentOptions.Add(new FAQOption("Back to start", 0));
        }

        private void LoadFirstMessage()
        {
            HandleOptionClick(new FAQOption("Hello! I need help.", _FIRST_OPTION));
            //_messageService.SendMessage(_chat.ChatId, _user, new FAQOption("Hello! I need help.", _FIRST_OPTION));
        }
    }
}
