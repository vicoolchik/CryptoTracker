using CryptoTracker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CryptoTracker
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<CurrencyDetailView>();
            containerRegistry.RegisterForNavigation<SearchResultsView>();
        }
    }
}
