namespace Infrastructure.Exceptions;

public class FriendNotFoundException : Exception
{
     public override string Message => "Esse amigo não foi encontrado.";
}
