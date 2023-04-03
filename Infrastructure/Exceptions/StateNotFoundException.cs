namespace Infrastructure.Exceptions;

public class StateNotFoundException : Exception
{
    public override string Message => "Esse estado não foi encontrado";
}
