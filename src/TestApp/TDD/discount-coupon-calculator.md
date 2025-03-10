## Kalkulator kuponów rabatów

## Wprowadzenie
Sklep internetowy przygotowuje się do uruchomienia kuponów rabatowych.
Każdy kupujący podczas procesu zamawiania będzie mógł wprowadzić **kod kuponu** `CouponCode` i skorzystać ze znizki procentowej.
System powinien automatycznie przeliczyć cenę na podstawie udzielonego rabatu.

### Implementacja CheckoutController

```cs
[ApiController]
[Route("api/checkout")]
public class CheckoutController : ControllerBase
{
    private readonly DiscountPriceCalculator _priceCalculator;

    public CheckoutController(DiscountPriceCalculator priceCalculator)
    {
        _priceCalculator = priceCalculator;
    }

    [HttpPost]
    public IActionResult ProcessCheckout([FromBody] CheckoutRequest request)
    {
        decimal totalPrice = _priceCalculator.CalculateTotalPrice(request.TotalPrice, request.CouponCode);
        return Ok(new { TotalPrice = totalPrice });
    }
}

public record CheckoutRequest(decimal TotalPrice, string? CouponCode);

```

### Przykładowe wywołania API
#### Brak kuponu
##### Żądanie
```json
{
  "totalprice": 100.0,
  "couponCode": ""
}
```

##### Odpowiedź
```json
{
  "TotalPrice": 300.0
}
```

#### Kupon "SAVE10NOW" (10% rabatu)

##### Żądanie
```json
{
  "price": 100.0,
  "couponCode": "SAVE10NOW"
}
```

##### Odpowiedź
```json
{
  "TotalPrice": 270.0
}
```


## Zadanie

Utwórz kalkulator _DiscountPriceCalculator_ z metodą _CalculateTotalPrice(decimal totalPrice, string couponCode)_ do obliczania ceny na podstawie kodu kuponu według poniższych wymagań.

## Wymagania

1. W przypadku podania pustego kodu rabat nie będzie udzielany.
2. Dodaj rabat 10%, który będzie naliczany po podaniu kodu kuponu `SAVE10NOW`.
3. Dodaj rabat 20%, który będzie naliczany po podaniu kodu kuponu `DISCOUNT20OFF`.
4. Wywołanie metody _CalculateDiscount_ z ujemną ceną powinno rzucić wyjątkiem _ArgumentException_ z komunikatem _"Negatives not allowed"_.
5. W przypadku błędnego kodu powinien być zwracany wyjątek _ArgumentException_ z komunikatem _"Invalid discount code"_
(6. Dodaj rabat 50%, który jest naliczany jednorazowo na podstawie kodu z puli kodów.)




