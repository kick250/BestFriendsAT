using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using System.Net.WebSockets;

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

    public Friend GetById(int id) 
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

    public void Create(Friend friend)
    {
        var response = Post("/Friends", friend).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void Update(Friend friend)
    {
        var response = Put($"/Friends/{friend.Id}", friend).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void DeleteById(int id)
    {
        var response = Delete($"/Friends/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}
