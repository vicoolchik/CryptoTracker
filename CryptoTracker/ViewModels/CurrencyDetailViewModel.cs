using CryptoTracker.Models;
using CryptoTracker.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace CryptoTracker.ViewModels
{
    public class CurrencyDetailViewModel : BindableBase, INavigationAware
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRegionManager _regionManager;
        private CurrencyDetail _currencyDetail;

        public CurrencyDetail CurrencyDetail
        {
            get => _currencyDetail;
            set => SetProperty(ref _currencyDetail, value);
        }

        public DelegateCommand GoBackCommand => new DelegateCommand(GoBack);

        public CurrencyDetailViewModel(ICurrencyService currencyService, IRegionManager regionManager)
        {
            _currencyService = currencyService;
            _regionManager = regionManager;
        }

        public async void LoadCurrencyDetail(string currencyId)
        {
            CurrencyDetail = await _currencyService.GetCurrencyDetailAsync(currencyId);
        }

        private void GoBack()
        {
            _regionManager.RequestNavigate("ContentRegion", "MainView");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string currencyId = navigationContext.Parameters.GetValue<string>("currencyId");
            LoadCurrencyDetail(currencyId);
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
