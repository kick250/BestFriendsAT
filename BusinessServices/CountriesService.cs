using Entities;
using Repository;

namespace BusinessServices;

public class CountriesService
{
    private CountriesRepository CountriesRepository { get; set; }

    public CountriesService(CountriesRepository countriesRepository)
    {
        CountriesRepository = countriesRepository;
    }

    public List<Country> GetAll()
    {
        return CountriesRepository.GetAll();
    }
}