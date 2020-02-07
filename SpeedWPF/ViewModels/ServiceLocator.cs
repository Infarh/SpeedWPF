using System;
using Microsoft.Extensions.DependencyInjection;
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

            _Services = services.BuildServiceProvider();
        }

        public MainWindowViewModel MainWindowModel => _Services.GetRequiredService<MainWindowViewModel>();
    }
}
