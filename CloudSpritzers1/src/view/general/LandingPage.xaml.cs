using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CloudSpritzers1.src.view.general
{
    public sealed partial class LandingPage : Page
    {
        private DispatcherTimer _carouselTimer = new DispatcherTimer();

        public LandingPage()
        {
            InitializeComponent();
            StartCarousel();
        }

        private void StartCarousel()
        {
            _carouselTimer = new DispatcherTimer();
            _carouselTimer.Interval = TimeSpan.FromSeconds(2);
            _carouselTimer.Tick += CarouselTimer_Tick;
            _carouselTimer.Start();
        }

        private void CarouselTimer_Tick(object? sender, object e)
        {
            if (SupportCarousel.Items.Count == 0)
                return;

            int nextIndex = SupportCarousel.SelectedIndex + 1;

            if (nextIndex >= SupportCarousel.Items.Count)
            {
                nextIndex = 0;
            }

            SupportCarousel.SelectedIndex = nextIndex;
        }
    }
}