namespace EngineerTest;

public class Drug : IDrug
{
    public string Name { get; }
    public int ExpiresIn { get; set; }
    public int Benefit { get; set; }

    public Drug(string name, int expiresIn, int benefit)
    {
        Name = name;
        ExpiresIn = expiresIn;
        Benefit = benefit;
    }

    protected bool Equals(Drug other)
    {
        return Name == other.Name && ExpiresIn == other.ExpiresIn && Benefit == other.Benefit;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Drug)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, ExpiresIn, Benefit);
    }
}