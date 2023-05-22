namespace EngineerTest;

public class Pharmacy : IPharmacy
{
    private readonly IDrug[] _drugs;

    public Pharmacy(IEnumerable<IDrug> drugs)
    {
        _drugs = drugs.ToArray();
    }

    public IEnumerable<IDrug> UpdateBenefitValue()
    {
        for (var i = 0; i < _drugs.Length; i++)
        {
            if (
                _drugs[i].Name != "Herbal Tea" &&
                _drugs[i].Name != "Fervex"
            )
            {
                if (_drugs[i].Benefit > 0)
                {
                    if (_drugs[i].Name != "Magic Pill")
                    {
                        _drugs[i].Benefit = _drugs[i].Benefit - 1;
                    }
                }
            }
            else
            {
                if (_drugs[i].Benefit < 50)
                {
                    _drugs[i].Benefit = _drugs[i].Benefit + 1;
                    if (_drugs[i].Name == "Fervex")
                    {
                        if (_drugs[i].ExpiresIn < 11)
                        {
                            if (_drugs[i].Benefit < 50)
                            {
                                _drugs[i].Benefit = _drugs[i].Benefit + 1;
                            }
                        }

                        if (_drugs[i].ExpiresIn < 6)
                        {
                            if (_drugs[i].Benefit < 50)
                            {
                                _drugs[i].Benefit = _drugs[i].Benefit + 1;
                            }
                        }
                    }
                }
            }

            if (_drugs[i].Name != "Magic Pill")
            {
                _drugs[i].ExpiresIn = _drugs[i].ExpiresIn - 1;
            }

            if (_drugs[i].ExpiresIn < 0)
            {
                if (_drugs[i].Name != "Herbal Tea")
                {
                    if (_drugs[i].Name != "Fervex")
                    {
                        if (_drugs[i].Benefit > 0)
                        {
                            if (_drugs[i].Name != "Magic Pill")
                            {
                                _drugs[i].Benefit = _drugs[i].Benefit - 1;
                            }
                        }
                    }
                    else
                    {
                        _drugs[i].Benefit =
                            _drugs[i].Benefit - _drugs[i].Benefit;
                    }
                }
                else
                {
                    if (_drugs[i].Benefit < 50)
                    {
                        _drugs[i].Benefit = _drugs[i].Benefit + 1;
                    }
                }
            }
        }

        return _drugs;
    }
}