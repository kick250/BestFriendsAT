namespace Infrastructure.Exceptions;

public class StringConnectionEmptyException : Exception
{
    public string GetMessage()
    {
        return "A string de conexão não foi encontrada.";
    }
}
