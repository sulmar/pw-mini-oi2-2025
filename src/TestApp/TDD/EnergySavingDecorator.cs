namespace TestApp.TDD;

// Concrete Decorator: Dekorator strategii
public class EnergySavingDecorator : IPowerConsumptionStrategy
{
    private readonly IPowerConsumptionStrategy _decoratedStrategy;
    
    public EnergySavingDecorator(IPowerConsumptionStrategy decoratedStrategy)
    {
        _decoratedStrategy = decoratedStrategy;
    }
    public decimal GetPowerConsumption(int duration)
    {
        return _decoratedStrategy.GetPowerConsumption(duration) * 0.8m;
    }
}