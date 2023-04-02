namespace Entities;

public class Friend
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set;}
    public DateOnly? Birthdate { get; set; }
    public string? PhotoUrl { get; set; }
    public List<Friend> Friends { get; set; } = new List<Friend>();
    public Country? Country { get; set; }
    public State? State { get; set; }

}
