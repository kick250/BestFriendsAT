using Microsoft.Extensions.Configuration;
using System.Data;

namespace Repository;

public class FriendshipsRepository : IRepository
{
    public FriendshipsRepository(IConfiguration configuration)
        : base(configuration) { }

    public void AddFriendship(int userId, int friendId)
    {
        using (var command = CreateCommand(@"CreateFriendship @UserId, @FriendId;"))
        {
            command.Parameters.Add(CreateParameter("@UserId", SqlDbType.Int, userId));
            command.Parameters.Add(CreateParameter("@FriendId", SqlDbType.Int, friendId));

            command.ExecuteNonQuery();
        }
    }
}
