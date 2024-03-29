﻿using AutoMapper;
using ElasticView.ApiDomain;
using ElasticView.ApiRepository;
using ElasticView.AppService;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Models.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using System.Windows;

namespace ElasticView.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }


        //private ServiceProvider serviceProvider;

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .UseSerilog((context, config) =>
                {
                    config.WriteTo.File($"log/log-.log",
                        rollingInterval: RollingInterval.Hour)
                    .MinimumLevel.Information()
                    ;
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>();

                    //services.AddHttpClient();
                    var httpClient = new HttpClient();
                    services.AddSingleton(httpClient);

                    //var assemblies = GetAssemblies();
                    //services.AddAutoMapper(assemblies);

                    services.AddAutoMapper(x =>
                    {
                        x.AddProfile(new ViewModelProfiles());
                    });



                    services.AddSingleton<IApiContext, ApiContext>();
                    services.AddSingleton<IElasticAppService, ElasticAppService>();
                    services.AddSingleton<IElasticIndexAppService, ElasticIndexAppService>();
                    services.AddSingleton<IElasticCatAppService, ElasticCatAppService>();
                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var window = AppHost.Services.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var logger = AppHost.Services.GetRequiredService<ILogger>();
            logger.Error(e.Exception.Message, e.Exception);

            HandyControl.Controls.MessageBox.Show("出错啦!,请查看日志或联系管理员.");

            e.Handled = true;
        }

        private Assembly[] GetAssemblies()
        {
            return DependencyContext.Default.CompileLibraries
                .Where(x => !x.Serviceable && x.Type != "package")
                .Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name))).ToArray();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppHost?.StopAsync();
            base.OnExit(e);
        }
    }
}
