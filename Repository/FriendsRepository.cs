using Entities;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using Repository.Factories;
using System.Data;
using System.Data.SqlClient;

namespace Repository;

public class FriendsRepository : IRepository
{
    private const int FRIENDS_COLUMNS_QUANTITY = 8;

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

    public Friend GetById(int id)
    {
        Friend? friend = null;

        using (var command = CreateCommand("GetFriendById @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            var data = command.ExecuteReader();

            friend = ParseFriend(data);
        }

        if (friend == null) throw new FriendNotFoundException();

        return friend;
    }

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

    #region private 
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

        var friendsFactory = new FriendsFactory();

        Country? country = ParseCountry(friendData);
        State? state = ParseState(friendData, country);

        Friend friend = friendsFactory.BuildFromProperties(
            friendData["Id"].ToString(),
            friendData["Name"].ToString(),
            friendData["LastName"].ToString(),
            friendData["Email"].ToString(),
            friendData["Phone"].ToString(),
            friendData["Birthdate"].ToString(),
            friendData["PhotoUrl"].ToString(),
            friendData["StateId"].ToString(),
            null,
            country,
            state
        );

        return friend;
    }

    private Country? ParseCountry(SqlDataReader countryData)
    {
        if (countryData.FieldCount <= FRIENDS_COLUMNS_QUANTITY) return null;

        const int idIndex = 12;
        const int nameIndex = 13;
        const int flagUrlIndex = 14;

        var factory = new CountriesFactory();

        return factory.BuildFromProperties(
            countryData[idIndex].ToString(),
            countryData[nameIndex].ToString(),
            countryData[flagUrlIndex].ToString(),
            null
        );
    }

    private State? ParseState(SqlDataReader stateData, Country? country)
    {
        if (stateData.FieldCount <= FRIENDS_COLUMNS_QUANTITY) return null;

        const int idIndex = 8;
        const int nameIndex = 9;
        const int flagUrlIndex = 10;
        const int countryIdIndex = 11;

        var factory = new StatesFactory();

        return factory.BuildFromProperties(
            stateData[idIndex].ToString(),
            stateData[nameIndex].ToString(),
            stateData[flagUrlIndex].ToString(),
            stateData[countryIdIndex].ToString(),
            country
        );
    }

    #endregion
}