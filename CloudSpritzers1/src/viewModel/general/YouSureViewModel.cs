using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CloudSpritzers1.src.viewModel.general
{
    public partial class YouSureViewModel : ObservableObject
    {
        private string _title = "Confirm Action";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message = "Are you sure you want to proceed?";
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _confirmButtonText = "Yes";
        public string ConfirmButtonText
        {
            get => _confirmButtonText;
            set => SetProperty(ref _confirmButtonText, value);
        }

        private string _cancelButtonText = "Cancel";
        public string CancelButtonText
        {
            get => _cancelButtonText;
            set => SetProperty(ref _cancelButtonText, value);
        }
    }
}
