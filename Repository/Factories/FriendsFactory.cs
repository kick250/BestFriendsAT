using Entities;
using System.Numerics;
using System.Xml.Linq;

namespace Repository.Factories;

public class FriendsFactory
{
    public FriendsFactory() { }

    public Friend BuildFromProperties(string? id, string? name, string? lastName, string? email, string? phone,
                                      string? birthdate, string? photoUrl, string? stateId, List<Friend>? friends,
                                      Country? country, State? state)
    {
        string idValue = id ?? "";
        string stateIdValue = stateId ?? "";
        string birthdateValue = birthdate ?? "";

        return new Friend()
        {
            Id = int.Parse(idValue),
            Name = name,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Birthdate = DateTime.Parse(birthdateValue),
            PhotoUrl = photoUrl,
            StateId = int.Parse(stateIdValue),
            Friends = friends,
            Country = country,
            State = state,
        };
    }
}
