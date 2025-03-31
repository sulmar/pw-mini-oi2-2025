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
        var discountPriceCalculator = new DiscountPriceCalculator( new DiscountFactory(new FakeCouponCodeRepository()));
        
        // Act
        var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, string.Empty);

        // Assert
        Assert.Equal(OriginalPrice, result);

    }

    // 2. Dodaj rabat 10%, który będzie naliczany po podaniu kodu kuponu SAVE10NOW.
    [Fact]
    public void CalculateTotalPrice_WhenCouponCodeIsSAVE10NOW_ShouldReturnDiscountedOriginalPriceBy10Percent()
    {
        ICouponCodeRepository repository = new FakeCouponCodeRepository();
        repository.Add("SAVE10NOW", 0.1m);
        
        var discountPriceCalculator = new DiscountPriceCalculator(new DiscountFactory(repository));
        
        var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "SAVE10NOW");
        
        Assert.Equal(90, result);
        
    }
    
    // 3. Dodaj rabat 20%, który będzie naliczany po podaniu kodu kuponu DISCOUNT20OFF
    
    [Fact]
    public void CalculateTotalPrice_WhenCouponCodeIsDISCOUNT20OFF_ShouldReturnDiscountedOriginalPriceBy20Percent()
    {
        ICouponCodeRepository repository = new FakeCouponCodeRepository();
        repository.Add("DISCOUNT20OFF", 0.2m);
        var discountPriceCalculator = new DiscountPriceCalculator(new DiscountFactory(repository));
        
        var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "DISCOUNT20OFF");
        
        Assert.Equal(80, result);
        
    }
    
    // 4. Wywołanie metody CalculateDiscount z ujemną ceną powinno rzucić wyjątkiem ArgumentException z komunikatem "Negatives not allowed".

    [Fact]
    public void CalculateTotalPrice_WhenOriginalPriceIsNegative_ShouldThrowArgumentExceptionWithMessage()
    {
        var discountPriceCalculator = new DiscountPriceCalculator(new DiscountFactory(new FakeCouponCodeRepository()));
        
        Action act = () => discountPriceCalculator.CalculateTotalPrice(-1m, string.Empty);
        
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Negatives not allowed", exception.Message);
        
        
    }
    
   // 5. W przypadku błędnego kodu powinien być zwracany wyjątek ArgumentException z komunikatem "Invalid coupon code" 

   [Fact]
   public void CalculateTotalPrice_WhenCouponCodeIsInvalid_ShouldThrowArgumentExceptionWithMessage()
   {
       ICouponCodeRepository repository = new FakeCouponCodeRepository();
       repository.Add("b", 0.1m);
       var discountPriceCalculator = new DiscountPriceCalculator(new DiscountFactory(repository));
       
       Action act = () => discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "a");
       var exception = Assert.Throws<ArgumentException>(act);
       
       Assert.Equal("Invalid coupon code", exception.Message);
   }
   
   // 6. Dodaj rabat 50%, który jest naliczany jednorazowo na podstawie kodu z puli kodów.

   [Fact]
   public void CalculateTotalPrice_WhenCouponCodeIsFirstUsaged_ShouldReturnOriginalDiscountedPriceBy50Percent()
   {
       ICouponCodeRepository repository = new FakeCouponCodeRepository();
       
       // Arrange
       var discountPriceCalculator = new DiscountPriceCalculator(new DecoratorDiscountFactory(new DiscountFactory(repository)));
       
       // Act
       var result = discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "XYZ");
       
       // Assert
       Assert.Equal(50, result);
   }
   
   
   [Fact]
   public void CalculateTotalPrice_WhenCouponCodeIsSecondUsaged_ShouldThrowArgumentExceptionWithMessage()
   {
       // Arrange
       ICouponCodeRepository repository = new FakeCouponCodeRepository();
       var discountPriceCalculator = new DiscountPriceCalculator(new DecoratorDiscountFactory(new DiscountFactory(repository)));
       discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "XYZ");
       
       // Act
       Action act = () => discountPriceCalculator.CalculateTotalPrice(OriginalPrice, "XYZ");
       
       // Assert
       var exception = Assert.Throws<ArgumentException>(act);
       Assert.Equal("Invalid coupon code", exception.Message);
   }
   
   
    
}