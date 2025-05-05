namespace Fsm.UnitTests;

public class AutomatTests
{
    [Fact]
    public void Constructor_WhenCalled_ShouldStateIsIdle()
    {
        // Arrange
        
        // Act
        var sut = new Automat();

        // Assert
        Assert.Equal(State.Idle, sut.CurrentState);
    }
    
    [Fact]
    public void Select_ValidProductId_ShouldStateIsSelected()
    {
        // Arrange
        var sut = new Automat();
        
        // Act
        sut.Select(1);
        
        // Assert
        Assert.Equal(State.Selected, sut.CurrentState);
    }

    [Fact]
    public void InsertCoin_OverOrEqualTotalAmount_ShouldStateIsProcessing()
    {
        // Arrange
        var sut = new Automat();
        sut.Select(1);
        
        sut.InsertCoin(2);
        sut.InsertCoin(2);

        // Act
        sut.InsertCoin(1);
        
        // Assert
        Assert.Equal(State.Processing, sut.CurrentState);
    }
    
    [Fact]
    public void InsertCoin_OverOrEqualTotalAmount_ShouldStateIsSelected()
    {
        // Arrange
        var sut = new Automat();
        sut.Select(1);
        
        sut.InsertCoin(2);

        // Act
        sut.InsertCoin(1);
        
        // Assert
        Assert.Equal(State.Selected, sut.CurrentState);
    }

    [Fact]
    public async void InsertCoin_WhenTimeoutElapsed_ShouldStateIsIdle()
    {
        // Arrange
        var sut = new Automat();
        sut.Select(1);
        
        // Act
        await Task.Delay(Automat.ElapsedTime + TimeSpan.FromSeconds(1));
        
        // Assert
        Assert.Equal(State.Idle, sut.CurrentState);
        
    }
    
    [Fact]
    public async void InsertCoin_WhenTimeoutNotElapsed_ShouldStateIsSelected()
    {
        // Arrange
        var sut = new Automat();
        sut.Select(1);
        
        // Act
        await Task.Delay(Automat.ElapsedTime - TimeSpan.FromSeconds(1));
        
        // Assert
        Assert.Equal(State.Selected, sut.CurrentState);
        
    }
    
    [Fact]
    public void Graph_WhenCalled_ShouldReturnGraph()
    {
        // Arrange
        var sut = new Automat();
        
        // Act
        var result = sut.Graph;
        
        // Assert
        Assert.NotNull(result);
        
        
    }
}