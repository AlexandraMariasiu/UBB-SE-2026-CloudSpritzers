using AutoMapper;
using CloudSpritzers1.src;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.dto.mappingProfiles;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.mappingProfiles;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.service;
using CloudSpritzers1.src.viewmodel;
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

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            DotNetEnv.Env.Load(System.IO.Path.Combine(AppContext.BaseDirectory, ".env"));

            var services = new ServiceCollection();
            services.AddAutoMapper(
                typeof(UserMappingProfile).Assembly,
                typeof(EmployeeMappingProfile).Assembly,
                typeof(BotMessageMappingProfile).Assembly,
                typeof(FAQEntryMappingProfile).Assembly,
                typeof(ReviewMappingProfile).Assembly
            );

            services.AddSingleton<ReviewRepository>();
            services.AddSingleton<ReviewService>();

            services.AddTransient<LandingViewModel>();

            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Content = new CloudSpritzers1.src.view.general.LandingPage();
            _window.Activate();
        }
    }
}
