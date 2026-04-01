using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.viewmodel;
using CloudSpritzers1.src.viewModel.message;
using Microsoft.UI.Xaml.Controls;

namespace CloudSpritzers1.src.view.message
{
    public sealed partial class MessageView : UserControl
    {
        public MessageDTO ViewModel => (MessageDTO)DataContext;
        public DisplayMessageViewModel MessageViewModel { get; set; } = new();

        public MessageView()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                if (e.NewValue is MessageDTO)
                {
                    this.Bindings.Update();
                }
            };
        }
    }
}