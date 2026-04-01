using AutoMapper;
using CloudSpritzers1.src;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.dto.mappingProfiles;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.chat;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.service;
using CloudSpritzers1.src.service.bot;
using CloudSpritzers1.src.service.bot.strategy;
using CloudSpritzers1.src.viewmodel;
using CloudSpritzers1.src.viewModel.chat;
using CloudSpritzers1.src.viewModel.review;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace CloudSpritzers1
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        private Window? _window;
        public User User { get; private set; }

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        public void SetUser(int userId)
        {
            if (User != null)
                return;
            User = Services.GetService<UserService>().GetById(userId);
        }

        private static IServiceProvider ConfigureServices()
        {
            DotNetEnv.Env.Load(System.IO.Path.Combine(AppContext.BaseDirectory, ".env"));

            var services = new ServiceCollection();
            services.AddAutoMapper(
                typeof(UserMappingProfile).Assembly,
                typeof(EmployeeMappingProfile).Assembly,
                typeof(MessageMappingProfile).Assembly,
                typeof(FAQEntryMappingProfile).Assembly,
                typeof(ReviewMappingProfile).Assembly,
                typeof(TicketMappingProfile).Assembly
            );

            services.AddSingleton<DecisionTreeRepository>();
            services.AddTransient<IBotStrategy, DecisionTreeStrategy>(); // I am not sure this is the way to do it :(
            services.AddTransient<BotEngine>();

            services.AddSingleton<MessageDBRepository>();
            services.AddSingleton<MessageService>();

            services.AddSingleton<ChatDBRepository>();
            services.AddSingleton<ChatService>();

            services.AddSingleton<ReviewRepository>();
            services.AddSingleton<ReviewService>();

            services.AddSingleton<UserRepository>();
            services.AddSingleton<UserService>();

            services.AddTransient<LandingViewModel>();
            services.AddTransient<AllReviewsViewModel>();
            services.AddTransient<AddReviewViewModel>();
            services.AddTransient<ChatViewModel>();
            

            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            Frame rootFrame = new Frame();
            _window.Content = rootFrame;

            rootFrame.Navigate(typeof(CloudSpritzers1.src.view.general.EnterYourId));
            //_window.Content = new CloudSpritzers1.src.view.general.EnterYourId();
            _window.Activate();
        }
    }
}
