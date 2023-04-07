using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Friend
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um nome.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um sobrenome.")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um email.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um telefone.")]
    public string? Phone { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de uma data de nascimento.")]
    public DateTime? Birthdate { get; set; }
    public string? PhotoUrl { get; set; }
    public int? StateId { get; set; }
    public List<Friend>? Friends { get; set; }
    public Country? Country { get; set; }
    public State? State { get; set; }

    public string GetCountryName()
    {
        if (Country == null) return "";

        return Country.Name ?? "";
    }

    public string GetStateName()
    {
        if (State == null) return "";

        return State.Name ?? "";
    }
}
