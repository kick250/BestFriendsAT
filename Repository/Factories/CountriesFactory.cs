using Entities;

namespace Repository.Factories;

public class CountriesFactory
{
    public CountriesFactory() { }

    public Country BuildFromProperties(string? id, string? name, string? FlagUrl, List<State>? states)
    {
        string idValue = id ?? "";

        return new Country()
        {
            Id = int.Parse(idValue),
            Name = name,
            FlagUrl = FlagUrl,
            States = states
        };
    }
}
