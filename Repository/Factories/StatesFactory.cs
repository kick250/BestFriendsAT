using Entities;

namespace Repository.Factories;

public class StatesFactory
{
    public StatesFactory() { }

    public State BuildFromProperties(string? id, string? name, string? flagUrl, string? countryId, Country? country)
    {
        string idValue = id ?? "";
        string countryIdValue = countryId ?? "";

        return new State()
        {
            Id = int.Parse(idValue),
            Name = name,
            FlagUrl = flagUrl,
            CountryId = int.Parse(countryIdValue),
            Country = country
        };
    }
}
