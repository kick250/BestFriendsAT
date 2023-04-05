using Microsoft.Extensions.Configuration;
using Entities;
using System.Data.SqlClient;
using Infrastructure.Exceptions;
using System.Data;
using Repository.Factories;

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

    public Country GetById(int id) 
    {
        Country? country = null;

        using (var command = CreateCommand("GetCountryById @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            var data = command.ExecuteReader();

            country = ParseCountry(data);
        }

        if (country == null) throw new CountryNotFoundException();

        return country;
    }

    public void DeleteById(int id)
    {
        using (var command = CreateCommand("DeleteCountry @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            command.ExecuteNonQuery();
        }
    }

    public void Create(Country country)
    {
        using (var command = CreateCommand(@"CreateCountry @Name, @FlagUrl;"))
        {
            command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, country.Name));
            command.Parameters.Add(CreateParameter("@FlagUrl", SqlDbType.VarChar, country.FlagUrl));

            command.ExecuteNonQuery();
        }
    }

    public void Update(Country country)
    {
        using (var command = CreateCommand(@"UpdateCountry @Id, @Name, @FlagUrl"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, country.Id));
            command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, country.Name));
            command.Parameters.Add(CreateParameter("@FlagUrl", SqlDbType.VarChar, country.FlagUrl));

            command.ExecuteNonQuery();
        }
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

        var countryFactory = new CountriesFactory();

        Country country = countryFactory.BuildFromProperties(
            countryData["Id"].ToString(),
            countryData["Name"].ToString(),
            countryData["FlagUrl"].ToString(),
            null
        );

        return country;
    }
    #endregion
}
