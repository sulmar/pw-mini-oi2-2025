namespace TestApp.TDD;

public interface ICouponCodeRepository
{
    IDictionary<string, decimal> GetAll();
    void Add(string couponCode, decimal discount);
}