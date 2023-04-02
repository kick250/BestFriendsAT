using Microsoft.Extensions.Configuration;
using Entities;
using System.Data.SqlClient;

namespace Repository;
public class CountriesRepository : IRepository
{
    public CountriesRepository(IConfiguration configuration)
        : base(configuration) { }

    public List<Country> GetAll()
    {
        List<Country> countries = new List<Country>();

        using (var command = CreateCommand("GetAllCountries;"))
        {
            var data = command.ExecuteReader();

            countries = ParseCountriesFromCollection(data);
        }

        return countries;
    }

    #region private 
    private List<Country> ParseCountriesFromCollection(SqlDataReader countriesData)
    {
        List<Country> countries = new List<Country>();

        if (countriesData.HasRows)
        {
            Country? country;
            while ((country = ParseCountry(countriesData)) != null)
            {
                countries.Add(country);
            }
        }

        return countries;
    }

    private Country? ParseCountry(SqlDataReader countryData)
    {
        if (!countryData.Read()) return null;

        Dictionary<String, String?> data = new Dictionary<string, string?>();

        data["Id"] = countryData["Id"].ToString();
        data["Name"] = countryData["Name"].ToString();
        data["FlagUrl"] = countryData["FlagUrl"].ToString();
        //data["States"] = countryData["States"].ToString();

        Country country = Country.BuildFromCountryData(data);

        return country;
    }
    #endregion
}
