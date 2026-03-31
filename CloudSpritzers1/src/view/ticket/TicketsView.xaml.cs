using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using CloudSpritzers1.src.model.ticket;
using CloudSpritzers1.src.model;
using System.Collections.ObjectModel;

namespace CloudSpritzers1.src.view
{
    public sealed partial class TicketsView : Page
    {
        public ObservableCollection<Ticket> Tickets { get; } = new ObservableCollection<Ticket>();
        public TicketsView()
        {
            this.InitializeComponent();

            // Sample user and categories
            var sampleUser = new User(1, "John Doe", "john@example.com");
            var sampleCategory = new TicketCategory(1, "Baggage", UrgencyLevelEnum.LOW);
            var sampleSubcategory = new TicketSubcategory(1, "Lost Items", 3, sampleCategory);

            // Create ticket
            var ticket = new Ticket(
                ticketId: 1,
                user: sampleUser,
                status: StatusEnum.OPEN,
                category: sampleCategory,
                subcategory: sampleSubcategory,
                subject: "Lost baggage",
                description: "My suitcase did not arrive on time.",
                createdAt: DateTime.Now
            );

            // Map status to color
            SolidColorBrush statusColor = ticket.Status switch
            {
                StatusEnum.OPEN => new SolidColorBrush(Colors.Green),
                StatusEnum.IN_PROGRESS => new SolidColorBrush(Colors.Orange),
                StatusEnum.RESOLVED => new SolidColorBrush(Colors.Gray),
                _ => new SolidColorBrush(Colors.Black)
            };

            // Create UI element manually
            var border = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 229, 231, 235)),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 0, 12)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var stack = new StackPanel { Spacing = 4 };
            stack.Children.Add(new TextBlock
            {
                Text = $"Ticket #{ticket.TicketId}",
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.Black)
            });
            stack.Children.Add(new TextBlock
            {
                Text = ticket.Subject,
                FontSize = 14,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 107, 114, 128))
            });
            stack.Children.Add(new TextBlock
            {
                Text = ticket.Status.ToString(),
                FontSize = 12,
                Foreground = statusColor
            });

            grid.Children.Add(stack);

            var viewBtn = new Button
            {
                Content = "View",
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 43, 184, 192)),
                Foreground = new SolidColorBrush(Colors.White),
                Width = 80,
                Height = 32,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(viewBtn, 1);
            grid.Children.Add(viewBtn);

            border.Child = grid;

            // Add to your StackPanel container
            Tickets.Add(ticket);
            TicketList.ItemsSource = Tickets;
        }

        private async void CreateTicketButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = null;

            // Main content panel
            var stackPanel = new StackPanel
            {
                Spacing = 12,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            // Header
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Submit a New Ticket",
                FontSize = 24,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Colors.Black)
            });

            stackPanel.Children.Add(new TextBlock
            {
                Text = "Please provide detailed information about your issue. All fields marked with * are required.",
                FontSize = 14,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 107, 114, 128)),
                TextWrapping = TextWrapping.Wrap
            });

            // Title
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Title of the issue*",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.Black)
            });

            stackPanel.Children.Add(new TextBox
            {
                PlaceholderText = "e.g., Lost baggage, damaged suitcase, service complaint",
                Width = 400,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 243, 243, 245)),
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(8),
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderForeground = new SolidColorBrush(Colors.DarkGray)
            });
            // Category
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Category*",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.Black)
            });

            var categoryCombo = new ComboBox
            {
                PlaceholderText = "Select a category",
                Width = 400,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 243, 243, 245)),
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderForeground = new SolidColorBrush(Colors.DarkGray)
            };
            categoryCombo.Items.Add("Baggage");
            categoryCombo.Items.Add("Service");
            categoryCombo.Items.Add("Technical");
            stackPanel.Children.Add(categoryCombo);

            // Subcategory
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Subcategory*",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.Black)
            });

            var subcategoryCombo = new ComboBox
            {
                PlaceholderText = "Select a subcategory",
                Width = 400,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 243, 243, 245)),
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderForeground = new SolidColorBrush(Colors.DarkGray)
            };
            subcategoryCombo.Items.Add("Lost Item");
            subcategoryCombo.Items.Add("Damage");
            subcategoryCombo.Items.Add("Complaint");
            stackPanel.Children.Add(subcategoryCombo);

            // Description
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Description*",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.Black)
            });

            stackPanel.Children.Add(new TextBox
            {
                PlaceholderText = "Please describe the issue in detail",
                Width = 400,
                Height = 120,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 243, 243, 245)),
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(8),
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderForeground = new SolidColorBrush(Colors.DarkGray)
            });

            // Email
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Email Address*",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.Black)
            });

            stackPanel.Children.Add(new TextBox
            {
                PlaceholderText = "your.email@example.com",
                Width = 400,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 243, 243, 245)),
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(8),
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderForeground = new SolidColorBrush(Colors.DarkGray)
            });


            // Notification text
            stackPanel.Children.Add(new TextBlock
            {
                Text = "You will receive email notifications when your ticket status changes.",
                FontSize = 12,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 107, 114, 128)),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 8, 0, 0)
            });

            // Buttons panel aligned to left, Send first
            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left,
                Spacing = 12,
                Margin = new Thickness(0, 12, 0, 0)
            };

            // Send button first
            var sendBtn = new Button
            {
                Content = "Send",
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 43, 184, 192)),
                Foreground = new SolidColorBrush(Colors.White),
                Padding = new Thickness(12, 6, 12, 6)
            };
            sendBtn.Click += (s, args) =>
            {
                // TODO: Add send logic here
                dialog?.Hide();
            };

            // Cancel button second
            var cancelBtn = new Button
            {
                Content = "Cancel",
                Background = new SolidColorBrush(Colors.White),
                Foreground = new SolidColorBrush(Colors.Black),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(12, 6, 12, 6),
            };
            cancelBtn.PointerEntered += (s, args) =>
            {
                cancelBtn.Background = new SolidColorBrush(Colors.White);
                cancelBtn.Foreground = new SolidColorBrush(Colors.Black);
            };
            cancelBtn.PointerExited += (s, args) =>
            {
                cancelBtn.Background = new SolidColorBrush(Colors.White);
                cancelBtn.Foreground = new SolidColorBrush(Colors.Black);
            };
            cancelBtn.Click += (s, args) => dialog?.Hide();

            buttonPanel.Children.Add(sendBtn);
            buttonPanel.Children.Add(cancelBtn);
            stackPanel.Children.Add(buttonPanel);

            // Create the dialog
            dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Background = new SolidColorBrush(Colors.White),
                Content = new ScrollViewer
                {
                    MaxHeight = 500,
                    Content = stackPanel
                },
                PrimaryButtonText = null,
                CloseButtonText = null
            };

            await dialog.ShowAsync();
        }
    }
}