namespace TestApp.UnitTests;

public class RentTests
{
    // Method_Scenario_ExpectedBehavior
    
    [Fact]
    public void CanReturn_WhenUserIsAdmin_ShouldReturnsTrue()
    {
        // Arrange
        Rent rent = new Rent(); 
        
        // Act
        var result = rent.CanReturn(new User { IsAdmin = true });
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanReturn_WhenUserIsRentee_ShouldReturnsTrue()
    {
        // Arrange
        Rent rent = new Rent();
        User rentee = new User();
        rent.Rentee = rentee;
        
        // Act
        var result = rent.CanReturn(rentee);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanReturn_WhenUserIsNotRenteeAndNotAdmin_ShouldReturnsFalse()
    {
        // Arrange
        Rent rent = new Rent(); 
        User rentee = new User();
        rent.Rentee = rentee;
        
        // Act
        var result = rent.CanReturn(new User());
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanReturn_WhenUserIsEmpty_ShouldThrowsArgumentNullException()
    {
        // Arrange
        Rent rent = new Rent();
        
        // Act
        Action act = () => rent.CanReturn(null);
        
        // Assert
        Assert.Throws<ArgumentNullException>(act);
        
    }
}
