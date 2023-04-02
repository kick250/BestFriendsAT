namespace Infrastructure.Exceptions;

public class FriendsNotFoundException : Exception
{
    public string GetMessage()
    {
        return "Esse amigo não foi encontrado.";
    }
}
