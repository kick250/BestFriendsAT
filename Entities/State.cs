namespace Entities;

public class State
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    public Country? Country { get; set; }

    public static State BuildFromStateData(Dictionary<string, string?> data, Country? country)
    {
        string? id = data["Id"] ?? "";

        return new State()
        {
            Id = int.Parse(id),
            Name = data["Name"],
            FlagUrl = data["FlagUrl"],
            Country = country
        };
    }

    public string GetCountryName()
    {
        if (Country == null)
            return "";

        return Country.Name ?? "";
    }
}