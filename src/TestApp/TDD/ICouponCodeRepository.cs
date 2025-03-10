namespace TestApp.TDD;

public interface ICouponCodeRepository
{
    IDictionary<string, decimal> GetAll();
}