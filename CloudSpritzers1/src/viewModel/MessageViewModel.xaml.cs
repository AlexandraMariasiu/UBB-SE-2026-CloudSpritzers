using System;
using System.Collections.ObjectModel;
using AutoMapper;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Model.Faq.Bot;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Service;
using CloudSpritzers1.Src.Service.Bot;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CloudSpritzers1.Src.Service.Interfaces;

namespace CloudSpritzers1.Src.ViewModel
{
    public partial class MessageViewModel : ObservableObject
    {
        private readonly MessageService _messageService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private readonly int _chatId;
        private readonly int _currentUserId;

        public ObservableCollection<MessageDTO> Messages { get; } = new();

        public MessageViewModel(
            MessageService messageService,
            IUserService userService,
            IMapper mapper,
            int chatId,
            int currentUserId)
        {
            _messageService = messageService;
            _userService = userService;
            _mapper = mapper;
            _chatId = chatId;
            _currentUserId = currentUserId;

            LoadMessages();
        }

        public void LoadMessages()
        {
            var messagesFromDb = _messageService.GetAllMessages(_chatId);
            Messages.Clear();

            foreach (var message in messagesFromDb)
            {
                Messages.Add(_mapper.Map<MessageDTO>(message));
            }
        }

        [RelayCommand]
        public void SendMessage(FAQOption selectedOption)
        {
            if (selectedOption == null)
            {
                throw new ArgumentNullException(nameof(selectedOption));
            }

            // Lazily resolve the current user only when needed.
            var sender = _userService.GetById(_currentUserId);

            BotMessage botReply = _messageService.SendMessage(_chatId, sender, selectedOption);

            Messages.Add(_mapper.Map<MessageDTO>(new Message(sender, botReply.GetChat(), selectedOption.Label)));
            Messages.Add(_mapper.Map<MessageDTO>(botReply));
        }
    }
}