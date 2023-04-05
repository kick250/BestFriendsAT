using Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Repository;

public class FriendsRepository : IRepository
{
    public FriendsRepository(IConfiguration configuration)
        : base(configuration) { }

    public List<Friend> GetAll()
    {
        List<Friend> friends = new List<Friend>();

        using (var command = CreateCommand("GetAllFriends;"))
        {
            var data = command.ExecuteReader();

            friends = ParseFriendsFromCollection(data);
        }

        return friends;
    }

    //public Friend GetById(int id)
    //{
    //    Friend? people = null;

    //    using (var command = CreateCommand("GetPeopleById @Id;"))
    //    {
    //        command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

    //        var data = command.ExecuteReader();

    //        people = ParsePeople(data);
    //    }

    //    if (people == null) throw new FriendsNotFoundException();

    //    return people;
    //}

    //public void DeleteById(int id)
    //{
    //    using (var command = CreateCommand("DeletePeople @Id;"))
    //    {
    //        command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

    //        command.ExecuteNonQuery();
    //    }
    //}

    //public void Create(Friend people)
    //{
    //    using (var command = CreateCommand(@"CreatePeople @Name, @LastName, @Email, @Phone, @Birthdate;"))
    //    {
    //        command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, people.Name));
    //        command.Parameters.Add(CreateParameter("@LastName", SqlDbType.VarChar, people.LastName));
    //        command.Parameters.Add(CreateParameter("@Email", SqlDbType.VarChar, people.Email));
    //        command.Parameters.Add(CreateParameter("@Phone", SqlDbType.VarChar, people.Phone));
    //        command.Parameters.Add(CreateParameter("@Birthdate", SqlDbType.Date, people.Birthdate));

    //        command.ExecuteNonQuery();
    //    }
    //}

    //public void Update(int id, Friend people)
    //{
    //    using (var command = CreateCommand(@"UpdatePeople @Id, @Name, @LastName, @Email, @Phone, @Birthdate;"))
    //    {
    //        command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));
    //        command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, people.Name));
    //        command.Parameters.Add(CreateParameter("@LastName", SqlDbType.VarChar, people.LastName));
    //        command.Parameters.Add(CreateParameter("@Email", SqlDbType.VarChar, people.Email));
    //        command.Parameters.Add(CreateParameter("@Phone", SqlDbType.VarChar, people.Phone));
    //        command.Parameters.Add(CreateParameter("@Birthdate", SqlDbType.Date, people.Birthdate));

    //        command.ExecuteNonQuery();
    //    }
    //}

    // private

    private List<Friend> ParseFriendsFromCollection(SqlDataReader friendsData)
    {
        List<Friend> friends = new List<Friend>();

        if (friendsData.HasRows)
        {
            Friend? friend;
            while ((friend = ParseFriend(friendsData)) != null)
            {
                friends.Add(friend);
            }
        }

        return friends;
    }

    private Friend? ParseFriend(SqlDataReader friendData)
    {
        if (!friendData.Read()) return null;

        Dictionary<String, String?> data = new Dictionary<string, string?>();

        data["Id"] = friendData["Id"].ToString();
        data["Name"] = friendData["Name"].ToString();
        data["LastName"] = friendData["LastName"].ToString();
        data["Email"] = friendData["Email"].ToString();
        data["Phone"] = friendData["Phone"].ToString();
        data["Birthdate"] = friendData["Birthdate"].ToString();
        data["PhotoUrl"] = friendData["PhotoUrl"].ToString();
        data["StateId"] = friendData["StateId"].ToString();

        Friend friend = Friend.BuildFromFriendData(data, null, null, null);

        return friend;
    }
}