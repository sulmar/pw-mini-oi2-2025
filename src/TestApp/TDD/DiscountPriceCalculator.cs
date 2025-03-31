namespace TestApp.TDD;

public class DiscountPriceCalculator(IDiscountFactory discountFactory)
{
    public decimal CalculateTotalPrice(decimal originalPrice, string couponCode)
    {
        if (originalPrice < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(couponCode))
            return originalPrice;
        
        var discount = discountFactory.Create(couponCode);
        
        return originalPrice - originalPrice * discount; 
        
    }
}

public interface IDiscountFactory
{
    decimal Create(string couponCode);
}

public class DiscountFactory(ICouponCodeRepository _couponCodeRepository) : IDiscountFactory
{
    public decimal Create(string couponCode)
    {
        var couponCodes = _couponCodeRepository.GetAll();
        
        if (!couponCodes.ContainsKey(couponCode))
            throw new ArgumentException("Invalid coupon code");

        return couponCodes[couponCode];

    }
}