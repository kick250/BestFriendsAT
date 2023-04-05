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

    
    public static Friend BuildFromFriendData(Dictionary<String, String?> data, List<Friend>? Friends, Country? Country, State? State)
    {
        string id = data["Id"] ?? "";
        string stateId = data["StateId"] ?? "";
        string birthdate = data["Birthdate"] ?? "";

        return new Friend()
        {
            Id = int.Parse(id),
            Name = data["Name"],
            LastName = data["LastName"],
            Email = data["Email"],
            Phone = data["Phone"],
            Birthdate = DateTime.Parse(birthdate),
            PhotoUrl = data["PhotoUrl"],
            StateId = int.Parse(stateId),
            Friends = Friends,
            Country = Country,
            State = State
        };
    }
}
