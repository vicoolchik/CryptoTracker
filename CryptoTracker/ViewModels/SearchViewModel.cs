using CryptoTracker.Models;
using CryptoTracker.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    public class SearchViewModel : BindableBase, INavigationAware
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Currency> _searchResults;
        private string _searchTerm;

        public ObservableCollection<Currency> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set => SetProperty(ref _searchTerm, value);
        }

        public DelegateCommand<Currency> NavigateToCurrencyDetailCommand => new DelegateCommand<Currency>(NavigateToCurrencyDetail);

        public SearchViewModel(ICurrencyService currencyService, IRegionManager regionManager)
        {
            _currencyService = currencyService;
            _regionManager = regionManager;
        }

        private async void SearchCurrencies()
        {
            var results = await _currencyService.SearchCurrenciesAsync(SearchTerm);
            SearchResults = new ObservableCollection<Currency>(results);
        }

        public DelegateCommand GoBackCommand => new DelegateCommand(() =>
        {
            _regionManager.RequestNavigate("ContentRegion", "MainView");
        });

        private void NavigateToCurrencyDetail(Currency currency)
        {
            if (currency == null)
            {
                return;
            }

            var parameters = new NavigationParameters();
            parameters.Add("currencyId", currency.Id);
            _regionManager.RequestNavigate("ContentRegion", "CurrencyDetailView", parameters);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue("searchTerm", out string searchTerm))
            {
                SearchTerm = searchTerm;
                SearchCurrencies();
            }
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
