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

}
