using Repository;
using Entities;

namespace BusinessServices;

public class StatesService
{
    private StatesRepository StatesRepository { get; set; }

    public StatesService(StatesRepository statesRepository)
    {
        StatesRepository = statesRepository;
    }

    public List<State> GetAll()
    {
        return StatesRepository.GetAll();
    }
}
