namespace TestApp.TDD;

public class DiscountPriceCalculator(ICouponCodeRepository _couponCodeRepository)
{
    public decimal CalculateTotalPrice(decimal originalPrice, string couponCode)
    {
        if (originalPrice < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(couponCode))
            return originalPrice;
  
        var couponCodes = _couponCodeRepository.GetAll();
        
        if (!couponCodes.ContainsKey(couponCode))
            throw new ArgumentException("Invalid coupon code");
        
        return originalPrice - originalPrice * couponCodes[couponCode]; 
        
    }
}