namespace Infrastructure.Exceptions;

public class StringConnectionEmptyException : Exception
{
    public override string Message => "A string de conexão não foi encontrada.";
}
