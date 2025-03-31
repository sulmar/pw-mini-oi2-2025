namespace TestApp.TDD;

public class FakeCouponCodeRepository : ICouponCodeRepository
{
    private IDictionary<string, decimal> _couponCodes;

    public FakeCouponCodeRepository()
    {
        _couponCodes = new Dictionary<string, decimal>();
     
    }

    public void Add(string couponCode, decimal discountPrice)
    {
        _couponCodes.Add(couponCode, discountPrice);
    }
    
    public IDictionary<string, decimal> GetAll()
    {
        return _couponCodes;
    }
}