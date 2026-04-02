using CloudSpritzers1.src.viewModel.general;
using CloudSpritzers1.src.viewModel.review;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CloudSpritzers1.src.view.general
{
    public sealed partial class UpperBar : UserControl
    {

        public UpperBarViewModel ViewModel { get; }

        public UpperBar()
        {
            this.InitializeComponent();
            ViewModel = (App.Current as App).Services.GetService<UpperBarViewModel>();
            this.DataContext = ViewModel;
        }
    }
}