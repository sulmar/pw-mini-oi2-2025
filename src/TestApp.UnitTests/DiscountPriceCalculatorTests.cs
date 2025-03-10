using TestApp.TDD;

namespace TestApp.UnitTests;

public class DiscountPriceCalculatorTests
{
    private const decimal OriginalPrice = 100m; 
    
    // 1. W przypadku podania pustego kodu rabat nie będzie udzielany.
    [Fact]
    public void CalculateTotalPrice_WhenCouponCodeIsEmpty_ShouldReturnOriginalPrice()
    {
        // Arrange
        var discountPriceCalculator = new DiscountPriceCalculator();
        
        // Act
        var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, string.Empty);

        // Assert
        Assert.Equal(OriginalPrice, result);

    }

    // 2. Dodaj rabat 10%, który będzie naliczany po podaniu kodu kuponu SAVE10NOW.
    [Fact]
    public void CalculateTotalPrice_WhenCouponCodeIsSAVE10NOW_ShouldReturnDiscountedOriginalPriceBy10Percent()
    {
        var discountPriceCalculator = new DiscountPriceCalculator();
        
        var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "SAVE10NOW");
        
        Assert.Equal(90, result);
        
    }
    
    // 3. Dodaj rabat 20%, który będzie naliczany po podaniu kodu kuponu DISCOUNT20OFF
    
    [Fact]
    public void CalculateTotalPrice_WhenCouponCodeIsDISCOUNT20OFF_ShouldReturnDiscountedOriginalPriceBy20Percent()
    {
        var discountPriceCalculator = new DiscountPriceCalculator();
        
        var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "DISCOUNT20OFF");
        
        Assert.Equal(80, result);
        
    }
    
    // 4. Wywołanie metody CalculateDiscount z ujemną ceną powinno rzucić wyjątkiem ArgumentException z komunikatem "Negatives not allowed".

    [Fact]
    public void CalculateTotalPrice_WhenOriginalPriceIsNegative_ShouldThrowArgumentExceptionWithMessage()
    {
        var discountPriceCalculator = new DiscountPriceCalculator();
        
        Action act = () => discountPriceCalculator.CalculateTotalPrice(-1m, string.Empty);
        
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Negatives not allowed", exception.Message);
        
        
    }
    
}