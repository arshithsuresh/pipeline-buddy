using Microsoft.Extensions.DependencyInjection;
using PipelineBuddy.Services;
using Implementation.Services;
using System;
using System.Windows;
using PipelineBuddyView.Config;
using PipelineBuddy.Models;
using PipelineBuddyView.ViewModel;
using PipelineBuddyView.Store;
using PipelineBuddyView.Views;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;
using PipelineBuddy.ExceptionHandler;
using ExceptionHandler;

namespace PipelineBuddy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            BuildHost();            
        }

        private void BuildHost()
        {
            var _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())            
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).Build();

            AppHost = Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostContext, configuration) =>
                {
                    configuration.AddConfiguration(_configuration);
                })
                .ConfigureServices((hostContext, services) => {
                    AddServices(services);                     
                })
                .Build();

            AppDomain.CurrentDomain.UnhandledException += 
                new UnhandledExceptionEventHandler(AppHost.Services.GetService<IExceptionHandler>()!.HandleException);
        }

        public static T GetService<T>(){
            
            var service = AppHost!.Services.GetService<T>();
            if(service == null) 
            {                
                throw new Exception("No Service found "+ nameof(T));
            }
            return service;
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AddNewJobWindow>();

            services.AddSingleton<ConfigModel>(DefaultConfig.getDefaultConfig());

            // Stores
            services.AddSingleton<SelectedJobDataStore>();
            services.AddSingleton<AllJobDataStore>();

            services.AddSingleton<IConfigService, ConfigService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IJobDataService, JobDataService>();

            services.AddSingleton<IBackgroundJobRefresher, BackgroundJobRefresher>();

            services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();
            
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<IPRLogger, FileLogger>();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var logger = GetService<IPRLogger>();
            var mainWindow = GetService<MainWindow>();
            logger.Info("Initializing PR Buddy...");

            var location = new Point(5, 5);
            var width = System.Windows.SystemParameters.WorkArea.Width;
            mainWindow.Left = width - mainWindow.Width - location.X;
            mainWindow.Top = location.Y ;
            
            if (mainWindow != null) 
                mainWindow.Show();

            logger.Info("Starting PR Buddy...");

            base.OnStartup(e);
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            var logger = GetService<IPRLogger>();
            logger.Info("PR Buddy Existing...");
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

    }

}
