namespace Ayllu.Domain.Exceptions.Common;

public class EntityNotFoundException(string message, Exception? inner = null!) : Exception(message, inner)
{
    
}
