using System.Text.Json;
using EngineerTest;



try
{
    var drugs = new[] {
        new Drug("Doliprane", 20, 30),
        new Drug("Herbal Tea", 10, 5),
        new Drug("Fervex", 12, 35),
        new Drug("Magic Pill", 15, 40)
    };
    IPharmacy pharmacy = new Pharmacy(drugs);

    var log = new List<Drug[]?>();

    for (var elapsedDays = 0; elapsedDays < 30; elapsedDays++) {
        log.Add(JsonSerializer.Deserialize<Drug[]>(JsonSerializer.Serialize(pharmacy.UpdateBenefitValue())));
    }

    File.WriteAllText(
        "output.json",
        JsonSerializer.Serialize(new { Result= log }, options: new JsonSerializerOptions { WriteIndented = true})
    );    
    Console.WriteLine("Success");

}
catch
{
    Console.WriteLine("Error");
}
