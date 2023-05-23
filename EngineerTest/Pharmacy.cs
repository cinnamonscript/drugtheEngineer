namespace EngineerTest;

public class Pharmacy : IPharmacy
{
    private readonly IDrug[] _drugs;

    public Pharmacy(IEnumerable<IDrug> drugs)
    {
        _drugs = drugs.ToArray();
    }

    // The Benefit of an item is never more than 50.
    // The Benefit of an item is never negative.
    // Once expiration date, Benefit degrades x2.
    // "Magic Pill" never expires nor decreases in Benefit.
    // "Herbal Tea" increases in Benefit, 2x faster after expiration date.
    // "Fervex" increases in Benefit, x2 when <11d, x3 when <6d, 0 after exp.
    // "Dafalgan" degrades in Benefit twice as fast as normal drugs.

    public IEnumerable<IDrug> UpdateBenefitValue()
    {
        for (var i = 0; i < _drugs.Length; i++)
        {
            // Benefit of Benefit-Decreasing Drugs = exclude "Herbal Tea", "Fervex", "Magic Pill"
            if (
                _drugs[i].Name != "Herbal Tea" &&
                _drugs[i].Name != "Fervex" &&
                _drugs[i].Name != "Magic Pill" &&
                _drugs[i].Benefit > 0 // Benefit does not go negative
            )
            {
                // Dafalgan Decreases Benefit by 2
                if (_drugs[i].Name == "Dafalgan" && _drugs[i].Benefit > 1) // Benefit does not go negative
                {
                    _drugs[i].Benefit -= 2;
                }

                // All other Drugs Decrease Benefit by 1, also assume if Dafalgan Benefit = 1 that it will degrade to 0
                else
                {
                    _drugs[i].Benefit--;
                }
            }

            // Benefit of Benefit-Increasing Drugs = exclude "Magic Pill", "Dafalgan"
            else
            {
                // Less than 50
                if (_drugs[i].Benefit < 50 &&
                _drugs[i].Name != "Magic Pill" &&
                _drugs[i].Name != "Dafalgan")
                {
                    // Benefit +1
                    _drugs[i].Benefit++;

                    // "Fervex" increases in Benefit, x2 when <11d, x3 when <6d.
                    if (_drugs[i].Name == "Fervex")
                    {
                        if (_drugs[i].ExpiresIn < 11)
                        {
                            _drugs[i].Benefit++;
                        }

                        if (_drugs[i].ExpiresIn < 6)
                        {
                            _drugs[i].Benefit++;
                        }

                    }

                    // "Herbal Tea" increases in Benefit, 2x faster after expiration date.
                    if (_drugs[i].Name == "Herbal Tea" && _drugs[i].Benefit < 49)
                    {
                        if (_drugs[i].ExpiresIn <= 0)
                        {
                            _drugs[i].Benefit++;
                        }
                    }
                }

                // "Fervex" Benefit = 0 after exp.
                if (_drugs[i].Name == "Fervex" && _drugs[i].ExpiresIn < 1)
                {
                    _drugs[i].Benefit = 0;
                }
            }

            // All drugs expire bar "Magic Pill"
            if (_drugs[i].Name != "Magic Pill")
            {
                _drugs[i].ExpiresIn--;
            }
        }
        return _drugs;
    }
}