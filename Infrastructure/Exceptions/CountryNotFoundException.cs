namespace Infrastructure.Exceptions;

public class CountryNotFoundException : Exception
{
    public override string Message => "Esse pais não foi encontrado";
}
