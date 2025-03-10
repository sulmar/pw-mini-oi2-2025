namespace TestApp.TDD;

public class FakeCouponCodeRepository : ICouponCodeRepository
{
    private IDictionary<string, decimal> _couponCodes;

    public FakeCouponCodeRepository()
    {
        _couponCodes = new Dictionary<string, decimal>();
        _couponCodes.Add("SAVE10NOW", 0.1M);        // 10%
        _couponCodes.Add("DISCOUNT20OFF", 0.2M);    // 20%
    }
    
    public IDictionary<string, decimal> GetAll()
    {
        return _couponCodes;
    }
}