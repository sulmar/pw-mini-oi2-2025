namespace TestApp.TDD;

public class DiscountPriceCalculator
{
    public decimal CalculateTotalPrice(decimal originalPrice, string couponCode)
    {
        if (originalPrice < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (couponCode == "SAVE10NOW")
            return originalPrice - originalPrice * 0.1M; // 10%
        
        if (couponCode == "DISCOUNT20OFF")
            return originalPrice - originalPrice * 0.2M; // 20%
        
        return originalPrice;

        throw new NotImplementedException();
    }
}