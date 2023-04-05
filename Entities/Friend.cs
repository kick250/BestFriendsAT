namespace Entities;

public class Friend
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
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
