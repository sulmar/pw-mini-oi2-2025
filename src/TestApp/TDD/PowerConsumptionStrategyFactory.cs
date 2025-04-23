namespace TestApp.TDD;

// Concrete Fabric: Fabryka strategii
public class PowerConsumptionStrategyFactory
{
    public IPowerConsumptionStrategy GetStrategy(string machineType, bool isEnergySaving = false)
    {
        IPowerConsumptionStrategy strategy;

        switch (machineType)
        {
            case "MillingMachine": strategy = new LinearPowerConsumptionStrategy(5m); break;
            case "Press": strategy = new LinearPowerConsumptionStrategy(7.2m); break;
            case "Lathe": strategy = new LogPowerConsumptionStrategy(3.5m); break;
            default: throw new ArgumentException("Machine type not supported");
        }
        
        if (isEnergySaving)
            strategy = new EnergySavingDecorator(strategy);

        return strategy;
        
    }
}