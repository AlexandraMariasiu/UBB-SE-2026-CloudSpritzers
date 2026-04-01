using CloudSpritzers1.src.viewModel.general;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CloudSpritzers1.src.view.general
{
    public sealed partial class YouSure : ContentDialog
    {
        
        public YouSureViewModel ViewModel { get; set; } = new();
        public YouSure()
        {
            this.InitializeComponent();
        }

        public YouSure(string message, string title = "Confirm") : this()
        {
            ViewModel.Message = message;
            ViewModel.Title = title;
        }
    }
}