using Repository;
using Entities;
using Infrastructure.Exceptions;

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

    public void Create(Friend friend)
    {
        if (IsEmailInUse(friend.Email ?? ""))
            throw new Exception("Esse email já está em uso.");

        FriendsRepository.Create(friend);
    }

    public void AddFriendship(int userId, int friendId)
    {
        FriendsRepository.AddFriendship(userId, friendId);
    }

    public void Update(Friend friend)
    {
        FriendsRepository.Update(friend);
    }

    public void DeleteById(int id)
    {
        FriendsRepository.DeleteById(id);
    }

    #region private 

    private bool IsEmailInUse(string email)
    {
        try
        {
            return FriendsRepository.GetByEmail(email) != null;
        } catch (FriendNotFoundException)
        {
            return false;
        }
    }
    #endregion
}
