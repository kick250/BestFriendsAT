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

    public string GetCountryName()
    {
        if (Country == null)
            return "";

        return Country.Name ?? "";
    }

    public string ToOption()
    {
        return $"{GetCountryName()} - {Name} ";
    }
}