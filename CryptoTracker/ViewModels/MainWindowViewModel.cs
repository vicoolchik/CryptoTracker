using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace CryptoTracker.ViewModels
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set => SetProperty(ref _searchTerm, value);
        }

        public DelegateCommand NavigateToSearchViewCommand => new DelegateCommand(() =>
        {
            var parameters = new NavigationParameters();
            parameters.Add("searchTerm", SearchTerm);
            _regionManager.RequestNavigate("ContentRegion", "SearchResultsView", parameters);
        });

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
