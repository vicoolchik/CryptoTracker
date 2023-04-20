using CryptoTracker.Services;
using CryptoTracker.ViewModels;
using CryptoTracker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Windows;

namespace CryptoTracker
{
    public partial class App : PrismApplication
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register your services and view models here
            containerRegistry.RegisterSingleton<ICurrencyService, CurrencyService>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<SearchResultsView, SearchViewModel>();
            containerRegistry.RegisterForNavigation<CurrencyDetailView, CurrencyDetailViewModel>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();


        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
        }

    }
}
