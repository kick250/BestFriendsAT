using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Country
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "É necessário um nome para o pais.")]
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    public List<State>? States { get; set; }
}
