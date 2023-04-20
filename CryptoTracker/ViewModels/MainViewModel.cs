using CryptoTracker.Models;
using CryptoTracker.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Currency> _currencies;

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }

        public DelegateCommand LoadTopNCurrenciesCommand => new DelegateCommand(LoadTopNCurrencies);
        public DelegateCommand<Currency> NavigateToCurrencyDetailCommand => new DelegateCommand<Currency>(NavigateToCurrencyDetail);

        public MainViewModel(ICurrencyService currencyService, IRegionManager regionManager)
        {
            _currencyService = currencyService;
            _regionManager = regionManager;
            LoadTopNCurrencies();
        }

        private async void LoadTopNCurrencies()
        {
            var currencies = await _currencyService.GetTopNCurrenciesAsync(10);
            Currencies = new ObservableCollection<Currency>(currencies);
        }

        private void NavigateToCurrencyDetail(Currency currency)
        {
            var parameters = new NavigationParameters();
            parameters.Add("currencyId", currency.Id);
            _regionManager.RequestNavigate("ContentRegion", "CurrencyDetailView", parameters); 
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
