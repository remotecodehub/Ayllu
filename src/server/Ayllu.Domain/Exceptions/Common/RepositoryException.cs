namespace Ayllu.Domain.Exceptions.Common;

public class RepositoryException(string message, Exception? inner = null!) : Exception(message, inner)
{
}
