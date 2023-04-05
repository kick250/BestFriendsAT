using Repository;
using Entities;

namespace BusinessServices;

public class FriendsService
{
    private FriendsRepository FriendsRepository { get; set; }

    public FriendsService(FriendsRepository friendsRepository) 
    {
        FriendsRepository = friendsRepository;
    }

    public List<Friend> GetAll()
    {
        return FriendsRepository.GetAll();
    }

    public Friend GetById(int id)
    {
        return FriendsRepository.GetById(id);
    }
}
