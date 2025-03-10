namespace TestApp.TDD;

public class DiscountPriceCalculator
{
    private readonly ICouponCodeRepository _couponCodeRepository;

    public DiscountPriceCalculator(ICouponCodeRepository couponCodeRepository)
    {
        _couponCodeRepository = couponCodeRepository;
    }
    
    public decimal CalculateTotalPrice(decimal originalPrice, string couponCode)
    {
        if (originalPrice < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(couponCode))
            return originalPrice;
  
        var couponCodes = _couponCodeRepository.GetAll();
        
        return originalPrice - originalPrice * couponCodes[couponCode]; 
        
    }
}