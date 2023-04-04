using Microsoft.Extensions.Configuration;
using Entities;
using System.Data.SqlClient;
using Infrastructure.Exceptions;
using System.Data;
using System.Diagnostics.Metrics;

namespace Repository;

public class StatesRepository : IRepository
{
    public StatesRepository(IConfiguration configuration)
        : base(configuration) { }

    public List<State> GetAll()
    {
        List<State> states = new List<State>();

        using (var command = CreateCommand("GetAllStates;"))
        {
            var data = command.ExecuteReader();

            states = ParseStatesFromCollection(data);
        }

        return states;
    }

    public State GetById(int id)
    {
        State? state = null;

        using (var command = CreateCommand("GetStateById @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            var data = command.ExecuteReader();

            state = ParseState(data);
        }

        if (state == null) throw new StateNotFoundException();

        return state;
    }

    public void Create(State state)
    {
        using (var command = CreateCommand(@"CreateState @Name, @FlagUrl, @CountryId;"))
        {
            command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, state.Name));
            command.Parameters.Add(CreateParameter("@FlagUrl", SqlDbType.VarChar, state.FlagUrl));
            command.Parameters.Add(CreateParameter("@CountryId", SqlDbType.Int, state.CountryId));

            command.ExecuteNonQuery();
        }
    }

    public void Update(State state)
    {
        using (var command = CreateCommand(@"UpdateState @Id, @Name, @FlagUrl, @CountryId"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, state.Id));
            command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, state.Name));
            command.Parameters.Add(CreateParameter("@FlagUrl", SqlDbType.VarChar, state.FlagUrl));
            command.Parameters.Add(CreateParameter("@CountryId", SqlDbType.Int, state.CountryId));

            command.ExecuteNonQuery();
        }
    }

    public void DeleteById(int id)
    {
        using (var command = CreateCommand("DeleteStateById @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            command.ExecuteNonQuery();
        }
    }

    #region
    private List<State> ParseStatesFromCollection(SqlDataReader statesData)
    {
        List<State> states = new List<State>();

        if (statesData.HasRows)
        {
            State? state;
            while ((state = ParseState(statesData)) != null)
            {
                states.Add(state);
            }
        }

        return states;
    }

    private State? ParseState(SqlDataReader stateData)
    {
        if (!stateData.Read()) return null;

        Dictionary<String, String?> data = new Dictionary<string, string?>();

        data["Id"] = stateData["Id"].ToString();    
        data["Name"] = stateData["Name"].ToString();
        data["FlagUrl"] = stateData["FlagUrl"].ToString();
        data["CountryId"] = stateData["CountryId"].ToString();

        State state;

        if (stateData.FieldCount > 4)
        {
            state = State.BuildFromStateData(data, ParseCountry(stateData));
        }
        else
            state = State.BuildFromStateData(data, null);

        return state;
    }

    private Country ParseCountry(SqlDataReader countryData)
    {
        const int idIndex = 4;
        const int nameIndex = 5;
        const int flagUrlIndex = 6;

        Dictionary<String, String?> data = new Dictionary<string, string?>();

        data["Id"] = countryData[idIndex].ToString();
        data["Name"] = countryData[nameIndex].ToString();
        data["FlagUrl"] = countryData[flagUrlIndex].ToString();

        Country country = Country.BuildFromCountryData(data, null);

        return country;
    }
    #endregion
}
