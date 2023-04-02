using Entities;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repository;

public class FriendsRepository : IRepository
{
    private static string StringConnection { get; set; } = "";

    public FriendsRepository(IConfiguration configuration)
        : base(configuration) { }

    public List<Friend> GetAll()
    {
        List<Friend> peoples = new List<Friend>();

        using (var command = CreateCommand("GetAllPeoples;"))
        {
            var data = command.ExecuteReader();

            peoples = ParsePeoplesFromCollection(data);
        }

        return peoples;
    }

    public Friend GetById(int id)
    {
        Friend? people = null;

        using (var command = CreateCommand("GetPeopleById @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            var data = command.ExecuteReader();

            people = ParsePeople(data);
        }

        if (people == null) throw new FriendsNotFoundException();

        return people;
    }

    public void DeleteById(int id)
    {
        using (var command = CreateCommand("DeletePeople @Id;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

            command.ExecuteNonQuery();
        }
    }

    public void Create(Friend people)
    {
        using (var command = CreateCommand(@"CreatePeople @Name, @LastName, @Email, @Phone, @Birthdate;"))
        {
            command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, people.Name));
            command.Parameters.Add(CreateParameter("@LastName", SqlDbType.VarChar, people.LastName));
            command.Parameters.Add(CreateParameter("@Email", SqlDbType.VarChar, people.Email));
            command.Parameters.Add(CreateParameter("@Phone", SqlDbType.VarChar, people.Phone));
            command.Parameters.Add(CreateParameter("@Birthdate", SqlDbType.Date, people.Birthdate));

            command.ExecuteNonQuery();
        }
    }

    public void Update(int id, Friend people)
    {
        using (var command = CreateCommand(@"UpdatePeople @Id, @Name, @LastName, @Email, @Phone, @Birthdate;"))
        {
            command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));
            command.Parameters.Add(CreateParameter("@Name", SqlDbType.VarChar, people.Name));
            command.Parameters.Add(CreateParameter("@LastName", SqlDbType.VarChar, people.LastName));
            command.Parameters.Add(CreateParameter("@Email", SqlDbType.VarChar, people.Email));
            command.Parameters.Add(CreateParameter("@Phone", SqlDbType.VarChar, people.Phone));
            command.Parameters.Add(CreateParameter("@Birthdate", SqlDbType.Date, people.Birthdate));

            command.ExecuteNonQuery();
        }
    }

    // private

    private List<Friend> ParsePeoplesFromCollection(SqlDataReader peoplesData)
    {
        List<Friend> peoples = new List<Friend>();

        if (peoplesData.HasRows)
        {
            Friend? people;
            while ((people = ParsePeople(peoplesData)) != null)
            {
                peoples.Add(people);
            }
        }

        return peoples;
    }

    private Friend? ParsePeople(SqlDataReader peopleData)
    {
        if (!peopleData.Read()) return null;

        Dictionary<String, String?> data = new Dictionary<string, string?>();

        data["Id"] = peopleData["Id"].ToString();
        data["Name"] = peopleData["Name"].ToString();
        data["LastName"] = peopleData["LastName"].ToString();
        data["Email"] = peopleData["Email"].ToString();
        data["Phone"] = peopleData["Phone"].ToString();
        data["Birthdate"] = peopleData["Birthdate"].ToString();

        //Friend people = Friend.BuildFromPeopleData(data);

        return new Friend();
    }
}