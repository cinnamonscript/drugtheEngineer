namespace EngineerTest;

public interface IDrug
{
    string Name { get; }
    int ExpiresIn { get; set; }
    int Benefit { get; set; }
}