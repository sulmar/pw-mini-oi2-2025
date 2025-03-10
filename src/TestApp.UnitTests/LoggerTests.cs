namespace TestApp.UnitTests;

public class LoggerTests
{
    Logger sut;  // SUT = System Under Test
    
    public LoggerTests()
    {
        sut = new Logger();
    }
    
    [Fact]
    public void Log_WhenMessageIsEmpty_ShouldThrowArgumentException()
    {
        // Act
        Action act = () => sut.Log(string.Empty);
        
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Log_WhenMessageIsNotEmpty_ShouldSetLastMessage()
    {
        // Act
        sut.Log("a");
        
        // Assert
        Assert.Equal("a", sut.LastMessage);
    }
}