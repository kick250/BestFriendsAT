using Microsoft.Extensions.Configuration;
using Entities;
using System.Data.SqlClient;
using Infrastructure.Exceptions;
using System.Data;
using Repository.Factories;

namespace Repository;

public class StatesRepository : IRepository
{
    private const int STATE_COLUMNS_QUANTITY = 4;

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

        var StatesFactory = new StatesFactory();

        State state = StatesFactory.BuildFromProperties(
            stateData["Id"].ToString(),
            stateData["Name"].ToString(),
            stateData["FlagUrl"].ToString(),
            stateData["CountryId"].ToString(),
            ParseCountry(stateData)
        );

        return state;
    }

    private Country? ParseCountry(SqlDataReader countryData)
    {
        if (countryData.FieldCount <= STATE_COLUMNS_QUANTITY) return null;

        const int idIndex = 4;
        const int nameIndex = 5;
        const int flagUrlIndex = 6;

        var factory = new CountriesFactory();

        return factory.BuildFromProperties(
            countryData[idIndex].ToString(),
            countryData[nameIndex].ToString(),
            countryData[flagUrlIndex].ToString(), 
            null
        );
    }
    #endregion
}
