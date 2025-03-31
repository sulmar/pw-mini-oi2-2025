namespace TestApp.TDD;

public class DecoratorDiscountFactory : IDiscountFactory
{
    private readonly IDiscountFactory _decoratedDiscountFactory;

    private readonly IDictionary<string, decimal> _onesCouponCodes = new Dictionary<string, decimal>();
    

    public DecoratorDiscountFactory(IDiscountFactory decoratedDiscountFactory)
    {
        _decoratedDiscountFactory = decoratedDiscountFactory;
        
        _onesCouponCodes.Add("XYZ", 0.5m);
    }

    public decimal Create(string couponCode)
    {
        if (_onesCouponCodes.ContainsKey(couponCode))
        {
            var discount = _onesCouponCodes[couponCode];
            _onesCouponCodes.Remove(couponCode);
            
            return discount;
        }
        else
            return _decoratedDiscountFactory.Create(couponCode); 
    }
}