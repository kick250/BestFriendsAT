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

    public State GetById(int id)
    {
        return StatesRepository.GetById(id);
    }

    public void Create(State state)
    {
        StatesRepository.Create(state);
    }

    public void DeleteById(int id)
    {
        StatesRepository.DeleteById(id);
    }
}
