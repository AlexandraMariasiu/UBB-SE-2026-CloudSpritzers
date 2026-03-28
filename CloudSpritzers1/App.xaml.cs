using AutoMapper;
using CloudSpritzers.src;
using CloudSpritzers.src.dto;
using CloudSpritzers.src.model.mappingProfiles;
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
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddAutoMapper(typeof(EmployeeMappingProfile).Assembly);

            // MESSAGE FOR ALL: here we will add ViewModels and Services
            // services.AddTransient<MainViewModel>();

            return services.BuildServiceProvider();

            //a way of using this: 
            //    public class MainViewModel
            //{
            //    private readonly IMapper _mapper;

            //    public MainViewModel(IMapper mapper)
            //    {
            //        _mapper = mapper;
            //    }

            //    public void LoadUserData()
            //    {
            //        var internalUser = new User(11, "Abel", "abel@gmail.com");
            //        UserDTO uiUser = _mapper.Map<UserDTO>(internalUser);
            //        Debug.WriteLine("Verification: User mapping successful.");
            //        Debug.WriteLine($"User Name: {uiUser.Name}");
            //    }
            //}
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
