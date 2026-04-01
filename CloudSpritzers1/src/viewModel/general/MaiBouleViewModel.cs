using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.viewModel.general
{
    public sealed partial class MaiBouleViewModel : ObservableObject
    {
        private string _title = "Warning";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message = "Oopsie Daisy! Boule.";
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

    }
}
