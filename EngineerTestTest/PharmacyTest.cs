using EngineerTest;
using Xunit;

namespace EngineerTestTest;

public class PharmacyTest
{
    [Fact]
    public void TestShouldDecreaseBenefitAndExpiresIn()
    {
        Assert.Equal(new Drug[] { new Drug("test", 1, 2) },
            new Pharmacy(new Drug[] { new Drug("test", 2, 3) }).UpdateBenefitValue());
    }

    [Fact]
    public void TestBenefitDoesNotIncreasePast50()
    {
        var drugs = new[]
        {
                new Drug("Herbal Tea", 0, 50),
                new Drug("Herbal Tea", 0, 49),
                new Drug("Fervex", 5, 50),
                new Drug("Fervex", 5, 49),
                new Drug("Fervex", 5, 48),
                new Drug("Fervex", 5, 47)
            };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            if (drug.Name == "Herbal Tea")
            {
                Assert.True(drug.Benefit < 51);
            }

            if (drug.Name == "Fervex")
            {
                Assert.True(drug.Benefit < 51);
            }
        }
    }

    [Fact]
    public void TestBenefitIsNotNegative()
    {
        var drugs = new[]
        {
                new Drug("Doliprane", 5, 0),
                new Drug("Dafalgan", 5, 0),
                new Drug("Doliprane", 0, 0),
                new Drug("Dafalgan", 0, 0)
            };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            Assert.True(drug.Benefit >= 0);
        }
    }

    [Fact]
    public void TestBenefitDecreasesTwiceAsFastAfterExpiration()
    {
        var drugs = new[]
        {
                new Drug("Doliprane", 5, 5),
                new Drug("Doliprane", 0, 5),
            };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            if (drug.ExpiresIn == 4)
            {
                Assert.Equal(4, drug.Benefit);
            }
            if (drug.ExpiresIn == -1)
            {
                Assert.Equal(3, drug.Benefit);
            }
        }
    }

    [Fact]
    public void TestMagicPillNeverExpiresOrDecreasesInBenefit()
    {
        var drugs = new[]
            {
                new Drug("Magic Pill", 1, 50),
            };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            Assert.Equal(50, drug.Benefit);
            Assert.Equal(1, drug.ExpiresIn);
        }
    }


    [Fact]
    public void TestHerbalTeaIncreasesBenefitFasterAfterExpiration()
    {
        var drugs = new[]
        {
        new Drug("Herbal Tea", 1, 20),
        new Drug("Herbal Tea", -2, 30)
        };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            if (drug.ExpiresIn == 0)
            {
                Assert.Equal(21, drug.Benefit);
            }
            if (drug.ExpiresIn == -3)
            {
                Assert.Equal(32, drug.Benefit);
            }
        }
    }

    [Fact]
    public void TestFervexIncreasesBenefitBasedOnExpiration()
    {
        var drugs = new[]
        {
                new Drug("Fervex", 15, 45),
                new Drug("Fervex", 8, 45),
                new Drug("Fervex", 3, 45),
                new Drug("Fervex", 0, 45)
            };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            if (drug.ExpiresIn == 14)
            {
                Assert.Equal(46, drug.Benefit); // 40 + 1 (15 days remaining)
            }
            if (drug.ExpiresIn == 7)
            {
                Assert.Equal(47, drug.Benefit); // 45 + 2 (8 days remaining)
            }
            if (drug.ExpiresIn == 2)
            {
                Assert.Equal(48, drug.Benefit); // 45 + 3 (3 days remaining)
            }
            if (drug.ExpiresIn == -1)
            {
                Assert.Equal(0, drug.Benefit); // 0 (expired)
            }
        }
    }

    [Fact]
    public void TestDafalganDegradesTwiceAsFastAsNormalDrugs()
    {
        var drugs = new[]
        {
                new Drug("Dafalgan", 5, 30),
                new Drug("Dafalgan", 0, 30)
        };
        var pharmacy = new Pharmacy(drugs);

        var updatedDrugs = pharmacy.UpdateBenefitValue();

        foreach (var drug in updatedDrugs)
        {
            if (drug.ExpiresIn == 4)
            {
                Assert.Equal(28, drug.Benefit);
            }
            if (drug.ExpiresIn == -1)
            {
                Assert.Equal(26, drug.Benefit);
            }
        }
    }
}