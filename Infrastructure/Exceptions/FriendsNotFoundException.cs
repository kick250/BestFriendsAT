namespace Infrastructure.Exceptions;

public class FriendsNotFoundException : Exception
{
     public override string Message => "Esse amigo não foi encontrado.";
}
