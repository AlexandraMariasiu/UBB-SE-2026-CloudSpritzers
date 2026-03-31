using Microsoft.UI.Xaml.Controls;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CloudSpritzers1.src.view.review
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReviewPage : Page
    {
        private int duty = 0, flight = 0, staff = 0, clean = 0;

        public ReviewPage()
        {
            this.InitializeComponent();
        }

        private void Rating_ValueChanged(RatingControl sender, object args)
        {
            int value = (int)sender.Value;

            if (sender == DutyRating)
            {
                DutyText.Text = value > 0 ? $"{value}/5" : "Not rated";
            }
            else if (sender == FlightRating)
            {
                FlightText.Text = value > 0 ? $"{value}/5" : "Not rated";
            }
            else if (sender == StaffRating)
            {
                StaffText.Text = value > 0 ? $"{value}/5" : "Not rated";
            }
            else if (sender == CleanRating)
            {
                CleanText.Text = value > 0 ? $"{value}/5" : "Not rated";
            }

        }

        private void ReviewBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReviewBox != null && CharCount != null)
            {
                CharCount.Text = $"{ReviewBox.Text.Length} characters";
            }

        }


    }
}

