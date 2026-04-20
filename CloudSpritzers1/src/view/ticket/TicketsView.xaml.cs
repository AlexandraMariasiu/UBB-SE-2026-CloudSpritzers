using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CloudSpritzers1.src.viewModel;

namespace CloudSpritzers1.src.view.ticket
{
    public sealed partial class TicketsView : Page
    {
        public TicketsViewModel ViewModel { get; }

        public TicketsView()
        {
            ViewModel = (App.Current as App).Services.GetService<TicketsViewModel>();
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private async void CreateTicketButton_Click(object sender, RoutedEventArgs e)
        {
            // Set XamlRoot so the dialog knows where to appear
            NewTicketDialog.XamlRoot = this.XamlRoot;
            await NewTicketDialog.ShowAsync();
        }

        private async void NewTicketDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                await ViewModel.SubmitNewTicketAsync();
            }
            catch (Exception ex)
            {
                // Stop the dialog from closing so the user can fix the error
                args.Cancel = true;
                await ShowError(ex.Message);
            }
        }

        private async Task ShowError(string message)
        {
            var dialog = new ContentDialog { XamlRoot = this.XamlRoot, Title = "Error", Content = message, CloseButtonText = "OK" };
            await dialog.ShowAsync();
        }

        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox combo && combo.SelectedItem is ComboBoxItem selected && selected.Tag != null)
            {
                ViewModel.SelectedFilterString = selected.Tag.ToString();
            }
        }
    }
}