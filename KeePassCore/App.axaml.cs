using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KeePassCore.Services;
using KeePassCore.ViewModels;
using KeePassCore.Views;
using Microsoft.Extensions.DependencyInjection;

namespace KeePassCore
{
    public class App : Application
    {
        private readonly ServiceCollection serviceCollection;
        private ServiceProvider serviceProvider;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public App()
        {
            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
        }
    
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<OpenDatabaseService>();
            services.AddSingleton<DialogService>();
            services.AddSingleton<LoggerService>();
            foreach (var viewModelType in Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.BaseType == typeof(ViewModelBase)))
            {
                services.AddTransient(viewModelType);
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                serviceCollection.AddSingleton(desktop);
                serviceProvider = serviceCollection.BuildServiceProvider();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
                };
            }
            else
            {
                throw new NotImplementedException($"{ApplicationLifetime}");
            }
            
            base.OnFrameworkInitializationCompleted();
        }
    }
}