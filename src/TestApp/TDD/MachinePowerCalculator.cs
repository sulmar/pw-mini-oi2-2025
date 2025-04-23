namespace TestApp.TDD;

public class MachinePowerCalculator
{
    private readonly PowerConsumptionStrategyFactory _strategyFactory;

    public MachinePowerCalculator(PowerConsumptionStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public decimal GetPowerConsumption(string machineType, int duration, bool isEnergySaving)
    {
        if (string.IsNullOrEmpty(machineType))
            throw new ArgumentException("Machine type cannot be empty");
        
        if (duration <= 0)
            throw new ArgumentException("Duration must be grater than zero");
        
        var strategy = _strategyFactory.GetStrategy(machineType, isEnergySaving);
        
        return strategy.GetPowerConsumption(duration);
            
    }
}