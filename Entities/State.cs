using System.ComponentModel.DataAnnotations;

namespace Entities;

public class State
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Um estado precisa de um nome.")]
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    [Required(ErrorMessage = "Um estado precisa de um pais.")]
    public int? CountryId { get; set; }
    public Country? Country { get; set; }

    public static State BuildFromStateData(Dictionary<string, string?> data, Country? country)
    {
        string id = data["Id"] ?? "";
        string countryId = data["CountryId"] ?? "";

        return new State()
        {
            Id = int.Parse(id),
            Name = data["Name"],
            FlagUrl = data["FlagUrl"],
            CountryId = int.Parse(countryId),
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