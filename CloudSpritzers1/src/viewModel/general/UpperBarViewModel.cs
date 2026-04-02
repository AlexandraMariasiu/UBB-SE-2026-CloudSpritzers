using CloudSpritzers1.src.model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.viewModel.general
{
    public sealed partial class UpperBarViewModel : ObservableObject
    {
        private User _user;
        public UpperBarViewModel()
        {
            _user = ((App)App.Current).User;
        }

        public string UserId => _user != null ? $"ID: {_user.GetId()}" : "Not signed in";
    }
}
