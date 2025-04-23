using TestApp.TDD;

namespace TestApp.UnitTests;

public class MachinePowerCalculatorTests
{
    [Fact]
    public void GetPowerConsumption_WhenMachineTypeIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var sut = new MachinePowerCalculator(new PowerConsumptionStrategyFactory());

        // Act
        Action act = () => sut.GetPowerConsumption(null, 1, false);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Machine type cannot be empty", exception.Message);
    }
    
    [Fact]
    public void GetPowerConsumption_WhenDurationIsZero_ShouldThrowArgumentException()
    {
        // Arrange
        var sut = new MachinePowerCalculator(new PowerConsumptionStrategyFactory());
        
        // Act
        Action act = () => sut.GetPowerConsumption("a",  0, false);
        
        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Duration must be grater than zero", exception.Message);
    }

    [Theory]
    [InlineData("MillingMachine", 1, false, 5)]
    [InlineData("MillingMachine", 2, false, 10)]
    [InlineData("Press", 1, false, 7.2)]
    [InlineData("Press", 2, false, 14.4)]
    [InlineData("Lathe", 1, false, 1.05)]
    [InlineData("Lathe", 2, false, 1.67)]
    [InlineData("Lathe", 10, false, 3.64)]
    [InlineData("Lathe", 100, false, 7.02)]
    [InlineData("MillingMachine", 1, true, 4)]
    public void GetPowerConsumption_WhenMachineTypeIsValid_ShouldReturn5000W(string machineType, int duration, bool isEnergySaving, decimal expected)
    {
        // Arrange
        var sut = new MachinePowerCalculator(new PowerConsumptionStrategyFactory());
        
        // Act
        var result = sut.GetPowerConsumption(machineType, duration, isEnergySaving);
        
        // Assert
        Assert.Equal(expected, result, precision: 2);
    }
}