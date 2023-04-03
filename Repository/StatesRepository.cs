using Microsoft.Extensions.Configuration;
using Entities;
using System.Data.SqlClient;

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

        State state = State.BuildFromStateData(data, null);

        return state;
    }
    #endregion
}
