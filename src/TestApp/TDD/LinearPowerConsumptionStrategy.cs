namespace TestApp.TDD;

// Concrete Strategy: Strategia liniowa
public class LinearPowerConsumptionStrategy : IPowerConsumptionStrategy
{
    private readonly decimal _basePower;

    public LinearPowerConsumptionStrategy(decimal basePower)
    {
        _basePower = basePower;
    }

    public decimal GetPowerConsumption(int duration)
    {
        return duration * _basePower;
    }
}