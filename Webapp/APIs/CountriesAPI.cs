using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class CountriesAPI : IAPI
{
    public CountriesAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public List<Country> GetAll() 
    {
        var response = Get("/Countries").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        List<Country>? result = JsonConvert.DeserializeObject<List<Country>>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result; 
    }

    public Country GetById(int id)
    {
        var response = Get($"/Countries/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        Country? result = JsonConvert.DeserializeObject<Country>(jsonResult);

        if (result == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result;
    }

    public void DeleteById(int id)
    {
        var response = Delete($"/Countries/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void Create(Country country)
    {
        var response = Post("/Countries", country).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}
