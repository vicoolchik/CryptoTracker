using System.Collections.Generic;

namespace CryptoTracker.Models
{
    public class CurrencyDetail : Currency
    {
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal PriceChange { get; set; }
        public List<Market> Markets { get; set; }
    }
}
