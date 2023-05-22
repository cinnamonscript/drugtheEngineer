using EngineerTest;
using Xunit;

namespace EngineerTestTest;

public class PharmacyTest
{
    [Fact]
    public void TestShouldDecreaseBenefitAndExpiresIn()
    {
        Assert.Equal(new Drug[] { new Drug("test", 1, 2)}, 
            new Pharmacy(new Drug[] { new Drug("test", 2, 3)}).UpdateBenefitValue());
    }
}