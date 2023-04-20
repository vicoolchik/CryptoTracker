using CryptoTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetTopNCurrenciesAsync(int n);
        Task<CurrencyDetail> GetCurrencyDetailAsync(string id);
        Task<List<Currency>> SearchCurrenciesAsync(string searchTerm);
    }
}
