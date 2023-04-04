using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class StatesAPI : IAPI
{
    public StatesAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public List<State> GetAll()
    {
        var response = Get("/States").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        List<State>? result = JsonConvert.DeserializeObject<List<State>>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result;
    }

    public State GetById(int id)
    {
        var response = Get($"/States/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        State? result = JsonConvert.DeserializeObject<State>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result;
    }

    public void Create(State state) 
    {
        var response = Post($"/States", state).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void Update(State state)
    {
        var response = Put($"/States/{state.Id}", state).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void DeleteById(int id)
    {
        var response = Delete($"/States/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}
