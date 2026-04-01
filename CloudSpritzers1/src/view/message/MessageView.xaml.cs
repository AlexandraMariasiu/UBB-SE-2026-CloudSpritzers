using CloudSpritzers1.src.dto;
using Microsoft.UI.Xaml.Controls;

namespace CloudSpritzers1.src.view.message
{
    public sealed partial class MessageView : UserControl
    {
        public MessageDTO ViewModel => (MessageDTO)DataContext;

        public MessageView()
        {
            this.InitializeComponent();
        }
    }
}