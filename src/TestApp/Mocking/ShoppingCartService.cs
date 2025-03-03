using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

#region Model

public class CartItem
{
    public string Name { get; }
    public decimal Price { get; } // Cena w PLN

    public CartItem(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
} 

#endregion

#region Infrastructure

public class NbpExchangeRateService
{
    private readonly HttpClient _httpClient;

    public NbpExchangeRateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string currencyCode)
    {
        var url = $"https://api.nbp.pl/api/exchangerates/rates/A/{currencyCode}/?format=json";
        var response = await _httpClient.GetStringAsync(url);
        var data = JsonSerializer.Deserialize<NbpApiResponse>(response);
        
        return data.Rates?[0]?.Mid ?? throw new Exception("Brak kursu waluty");
    }
}

public class NbpApiResponse
{
    public List<NbpRate> Rates { get; set; }
}

public class NbpRate
{
    public decimal Mid { get; set; }
}

#endregion


public class ShoppingCartService
{
    private readonly List<CartItem> _items = new();
   
    public void AddItem(CartItem item) => _items.Add(item);

    public async Task<decimal> CalculateTotalAsync(string currencyCode)
    {
        var  _exchangeRateService = new NbpExchangeRateService(new HttpClient());

        if (!_items.Any()) return 0m;

        decimal totalInPln = _items.Sum(i => i.Price);
        decimal exchangeRate = await _exchangeRateService.GetExchangeRateAsync(currencyCode);

        return totalInPln / exchangeRate;
    }
}