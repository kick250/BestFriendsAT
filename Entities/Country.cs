namespace Entities;
public class Country
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    public List<State>? States { get; set; }

    public static Country BuildFromCountryData(Dictionary<String, String?> data)
    {
        string? id = data["Id"] ?? "";

        List<State>? states = null; //data["State]

        return new Country()
        {
            Id = int.Parse(id),
            Name = data["Name"],
            FlagUrl = data["FlagUrl"],
            States = states
        };
    }
}
