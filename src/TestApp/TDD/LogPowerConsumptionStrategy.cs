namespace TestApp.TDD;

// Concrete Strategy: Strategia logarytmiczna
public class LogPowerConsumptionStrategy : IPowerConsumptionStrategy
{
    private readonly decimal _basePower;

    public LogPowerConsumptionStrategy(decimal basePower)
    {
        _basePower = basePower;
    }

    public decimal GetPowerConsumption(int duration)
    {
        return _basePower * (decimal)  Math.Log10(duration + 1);
    }
}