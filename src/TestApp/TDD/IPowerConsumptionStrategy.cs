namespace TestApp.TDD;

// Abstract Strategy
public interface IPowerConsumptionStrategy
{
    decimal GetPowerConsumption(int duration);
}