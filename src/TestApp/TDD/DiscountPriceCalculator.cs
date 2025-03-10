namespace TestApp.TDD;

public class DiscountPriceCalculator
{
    private IDictionary<string, decimal> _couponCodes;

    public DiscountPriceCalculator()
    {
        _couponCodes = new Dictionary<string, decimal>();
        _couponCodes.Add("SAVE10NOW", 0.1M);        // 10%
        _couponCodes.Add("DISCOUNT20OFF", 0.2M);    // 20%
    }
    
    public decimal CalculateTotalPrice(decimal originalPrice, string couponCode)
    {
        if (originalPrice < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(couponCode))
            return originalPrice;
              
        return originalPrice - originalPrice * _couponCodes[couponCode]; 
        
    }
}