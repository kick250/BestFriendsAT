using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class FriendsAPI : IAPI
{
    public FriendsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public List<Friend> GetAll()
    {
        var response = Get("/Friends").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        List<Friend>? result = JsonConvert.DeserializeObject<List<Friend>>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result;
    }

    public Friend GetById(string id) 
    {
        var response = Get($"/Friends/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        Friend? result = JsonConvert.DeserializeObject<Friend>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result;
    }
}
