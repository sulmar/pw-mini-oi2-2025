using TestApp.TDD;

namespace TestApp.UnitTests;

public class DecoratorDiscountFactoryTests
{
    [Fact]
    public void GetStrategy_WhenMachineTypeIsMillingMachine_ShouldReturnCorrectStrategy()
    {
        var sut = new PowerConsumptionStrategyFactory();
        
        var result = sut.GetStrategy("MillingMachine");
        
        Assert.IsType<LinearPowerConsumptionStrategy>(result);
    }
    
    [Fact]
    public void GetStrategy_WhenMachineTypeIsPress_ShouldReturnCorrectStrategy()
    {
        var sut = new PowerConsumptionStrategyFactory();
        
        var result = sut.GetStrategy("Press");
        
        Assert.IsType<LinearPowerConsumptionStrategy>(result);
    }
    
    [Fact]
    public void GetStrategy_WhenMachineTypeIsLathe_ShouldReturnCorrectStrategy()
    {
        var sut = new PowerConsumptionStrategyFactory();
        
        var result = sut.GetStrategy("Lathe");
        
        Assert.IsType<LogPowerConsumptionStrategy>(result);
    }
}