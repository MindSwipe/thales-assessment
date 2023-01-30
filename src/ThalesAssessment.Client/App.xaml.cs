using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Windows;
using ThalesAssessment.Client.Services;
using ThalesAssessment.Entities.Settings;

namespace ThalesAssessment.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ApiService>();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddOptions<AppSettings>().BindConfiguration(nameof(AppSettings));
            serviceCollection.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            Instance = this;
        }
    }
}
