using System;
using Microsoft.Extensions.DependencyInjection;
using SpeedWPF.Services;
using SpeedWPF.Services.Interfaces;

// ReSharper disable ConvertToAutoPropertyWhenPossible
// ReSharper disable UnusedMember.Global

namespace SpeedWPF.ViewModels
{
    public class ServiceLocator
    {
        private readonly IServiceProvider _Services;

        public IServiceProvider Services => _Services;

        public ServiceLocator()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();

            services.AddTransient<IDataService, DataService>();

            _Services = services.BuildServiceProvider();
        }

        public MainWindowViewModel MainWindowModel => _Services.GetRequiredService<MainWindowViewModel>();
    }
}
