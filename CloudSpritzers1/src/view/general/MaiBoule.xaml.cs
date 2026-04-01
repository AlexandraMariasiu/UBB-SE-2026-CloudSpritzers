using CloudSpritzers1.src.viewModel.general;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.view.general
{
    
    public sealed partial class MaiBoule : ContentDialog
    {
        public MaiBouleViewModel ViewModel { get; set; } = new();
        public MaiBoule()
        {
            this.InitializeComponent();
        }

        public MaiBoule(string message, string title = "Warning") : this()
        {
            ViewModel.Message = message;
            ViewModel.Title = title;
        }
    }
}
