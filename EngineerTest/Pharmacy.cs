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
    // "Herbal Tea" increases in Benefit, 2x faster after expiration date.
    // "Magic Pill" never expires nor decreases in Benefit.
    // "Fervex" increases in Benefit, x2 when <11d, x3 when <6d, 0 after exp.
    // "Dafalgan" degrades in Benefit twice as fast as normal drugs.

    public IEnumerable<IDrug> UpdateBenefitValue()
    {
        for (var i = 0; i < _drugs.Length; i++)
        {
            // Benefit if not "Herbal Tea", "Fervex", "Magic Pill", "Dafalgan"
            if (
                _drugs[i].Name != "Herbal Tea" &&
                _drugs[i].Name != "Fervex" &&
                _drugs[i].Name != "Magic Pill" &&
                _drugs[i].Name != "Dafalgan"
            )
            {
                // If Benefit does not go negative
                if (_drugs[i].Benefit > 0)
                {
                // Benefit decreases by 1
                _drugs[i].Benefit = _drugs[i].Benefit - 1;
                }
            }

            // Benefit if "Herbal Tea" or "Fervex"
            else
            {
                // Less than 50
                if (_drugs[i].Benefit < 50)
                {
                    // Benefit +1
                    _drugs[i].Benefit = _drugs[i].Benefit + 1;
                    
                    // "Fervex"
                    if (_drugs[i].Name == "Fervex")
                    {
                        if (_drugs[i].ExpiresIn < 11 && _drugs[i].ExpiresIn > 5)
                        {
                        _drugs[i].Benefit = _drugs[i].Benefit + 1;
                        }

                        if (_drugs[i].ExpiresIn < 6)
                        {
                        _drugs[i].Benefit = _drugs[i].Benefit + 1;
                        }

                        if (_drugs[i].ExpiresIn < 0)
                        {
                        _drugs[i].Benefit == 0;
                        }
                    }

                    // "Herbal Tea"
                    if (_drugs[i].Name == "Herbal Tea")
                    {
                        if (_drugs[i].ExpiresIn < 0)
                        {
                        _drugs[i].Benefit = _drugs[i].Benefit + 2;
                        }
                    }

                    // "Dafalgan"
                    if (_drugs[i].Name == "Dafalgan")
                    {
                        if (_drugs[i].Benefit > 0)
                        {
                        _drugs[i].Benefit = _drugs[i].Benefit - 2;
                        }
                    }
                }
            }

            // Expiring if not "Magic Pill"
            if (_drugs[i].Name != "Magic Pill")
            {
                _drugs[i].ExpiresIn = _drugs[i].ExpiresIn - 1;
            }
        }
        return _drugs;
    }
}